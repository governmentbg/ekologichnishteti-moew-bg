using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Zopoesht.Application.Applications.ApplicationsTwo.Dtos;
using Zopoesht.Application.Applications.ApplicationsTwo.Interfaces;
using Zopoesht.Application.Applications.Common.Dtos;
using Zopoesht.Application.Applications.Common.Services;
using Zopoesht.Application.Infrastructure.Services;
using Zopoesht.Application.Users.Interfaces;
using Zopoesht.Data.Applications.ApplicationOne;
using Zopoesht.Data.Applications.ApplicationTwo;
using Zopoesht.Data.Applications.ApplicationTwo.Enums;
using Zopoesht.Data.Applications.Common.Enums;
using Zopoesht.Data.Applications.Common.Models;
using Zopoesht.Data.Common.Enums;
using Zopoesht.Data.Logs.Enums;
using Zopoesht.Infrastructure.DomainValidation;
using Zopoesht.Infrastructure.DomainValidation.Enums;
using Zopoesht.Infrastructure.DomainValidation.Models;
using Zopoesht.Infrastructure.Emails;
using Zopoesht.Infrastructure.Interfaces.Contexts;
using Zopoesht.Infrastructure.Interfaces.Users;
using Zopoesht.Infrastructure.Logs;
using DeepL;
using Zopoesht.Infrastructure.AppSettings.DeepLTranslateConfiguration;
using Zopoesht.Infrastructure.AppSettings;
using DeepL.Model;

namespace Zopoesht.Application.Applications.ApplicationsTwo.Services
{
    public class ApplicationTwoService : ApplicationService<ApplicationTwo, ApplicationTwoDto>, IApplicationTwoService
    {

        private readonly IAppDbContext context;
        private readonly DomainValidationService validation;
        private readonly RoleService roleService;
        private readonly IMapper mapper;
        private readonly IUserContext userContext;
        private readonly ILoggingService loggingService;
        private readonly DeepLTranslateConfigurationSettings deepLTranslateConfiguration;

        public ApplicationTwoService(
            DomainValidationService validation,
            RoleService roleService,
            IAppDbContext context,
            IMapper mapper,
            IUserContext userContext,
            IEmailService emailService,
            IUserService userService,
            ILoggingService loggingService
            )
            : base(context, userContext, mapper, validation, roleService, emailService, userService, loggingService)
        {
            this.context = context;
            this.validation = validation;
            this.roleService = roleService;
            this.mapper = mapper;
            this.userContext = userContext;
            this.loggingService = loggingService;
            this.deepLTranslateConfiguration = AppSettingsConfiguration.DeepLTranslateConfiguration;
        }

        public override async Task Create(ApplicationTwoDto dto)
        {
            dto.ValidateProperties(this.validation);

            using (var transaction = await context.BeginTransactionAsync())
            {
                try
                {
                    await TranslateFields(dto);

                    var applicationTwo = MapApplicationTwoFromDto(dto);

                    await SaveApplicationAsync(applicationTwo);

                    var appHistory = new ApplicationHistoryDto()
                    {
                        ApplicationId = applicationTwo.Id,
                        RootId = applicationTwo.RootId,
                        UserFullName = this.userContext.Username,
                        Type = ApplicationHistoryType.Create
                    };

                    await this.CreateHistory(appHistory, ApplicationType.ApplicationTwo);

                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();

                    throw new Exception(ex.Message);
                }
            }
        }

        public override async Task<ApplicationTwoDto> SaveModification(ApplicationTwoDto dto)
        {
            this.ValidatePermissionDtoException(dto.ApplicationTwoAuthorities, dto.Applicant.Authority?.Id);
            dto.ValidateProperties(this.validation);

            dto.ApplicationTwoFiles.ForEach(f => f.ZopoeshtAttachedFileId = 0);

            return await base.SaveModification(dto);
        }

        public override async Task<ApplicationTwoDto> BeginModification(ApplicationTwoDto dto, CommitState state, ApplicationType applicationType)
        {
            this.ValidatePermissionDtoException(dto.ApplicationTwoAuthorities, dto.Applicant.Authority?.Id);

            dto.ApplicationTwoFiles.ForEach(f =>
            {
                f.ZopoeshtAttachedFileId = 0; 
                f.ZopoeshtAttachedFile.Id = 0;
            });

            return await base.BeginModification(dto, state, applicationType);
        }

        public override async Task<ApplicationTwoDto> FinishModification(ApplicationTwoDto dto, CommitState state, ApplicationType applicationType)
        {
            this.ValidatePermissionDtoException(dto.ApplicationTwoAuthorities, dto.Applicant.Authority?.Id);
            dto.ValidateProperties(this.validation);

            dto.ApplicationTwoFiles.ForEach(f => f.ZopoeshtAttachedFileId = 0);

            await TranslateFields(dto);

            return await base.FinishModification(dto, state, applicationType);
        }

        public override async Task ChangeApplicationStateAndCreateHistory(int applicationId, CommitState state, ApplicationType applicationType)
        {
            var application = await this.context.Set<ApplicationTwo>()
                .Where(s => s.Id == applicationId)
                .Include(s => s.Applicant)
                .Include(s => s.ApplicationTwoAuthorities)
                .Include(s => s.ApplicationTwoActivityOfferings)
                .SingleOrDefaultAsync();

            ValidateCaseStatus(application);
            ValidateOperator(application);
            ValidateActivityOfferings(application);

            this.ValidatePermissionModelException(application.ApplicationTwoAuthorities, application.Applicant.AuthorityId);

            await base.ChangeApplicationStateAndCreateHistory(applicationId, state, applicationType);
        }

        public override async Task<ApplicationTwoDto> GetById(int id)
        {
            var item = await base.GetById(id);

            var locks = await this.context.Set<ApplicationLock>()
                .AsNoTracking()
                .Where(s => s.ApplicationType == ApplicationType.ApplicationTwo && s.ApplicationId == id && s.IsLocked)
                .OrderBy(s => s.LockedDate)
                .ProjectTo<ApplicationLockDto>(this.mapper.ConfigurationProvider)
                .ToListAsync();

            item.HasPermissionToEdit = locks.Any(currentLock => currentLock.UserId == this.userContext.UserId);

            item.HasPermissionToControl = this.ValidatePermissionDto(item.ApplicationTwoAuthorities, item.Applicant.Authority?.Id);

            item.Locks = locks;

            var applicationOneType = await this.context.Set<ApplicationOne>()
                .AsNoTracking()
                .Where(a => a.Id == item.ApplicationOneId)
                .Select(t => t.ApplicationOneType)
                .SingleOrDefaultAsync();

            item.ApplicationOneType = applicationOneType;

            return item;
        }

        public override async Task CancelModification(ApplicationTwoDto dto, ApplicationType applicationType)
        {
            using (var transaction = await this.context.BeginTransactionAsync())
            {
                try
                {
                    var application = await this.context.Set<ApplicationTwo>()
                        .Include(a => a.Applicant)
                        .Include(a => a.ApplicationTwoAuthorities)
                        .Include(a => a.Codes)
                        .Include(a => a.ApplicationTwoFiles)
                        .SingleOrDefaultAsync(a => a.Id == dto.Id);

                    if (application == null)
                    {
                        validation.ThrowErrorMessage(ApplicationErrorCode.Application_DoesNotExist);
                    }

                    this.ValidatePermissionModelException(application.ApplicationTwoAuthorities, application.Applicant.AuthorityId);

                    var applicationTwoAuthorities = await this.context.Set<ApplicationTwoAuthority>()
                        .Where(a => a.ApplicationTwoId == dto.Id)
                        .ToListAsync();

                    var activityOfferings = await this.context.Set<ApplicationTwoActivityOffering>()
                        .Where(a => a.ApplicationTwoId == dto.Id)
                        .ToListAsync();

                    var applicationTwoCodes = await this.context.Set<ApplicationTwoCode>()
                        .Where(a => a.ApplicationTwoId == dto.Id)
                        .ToListAsync();

                    var applicationTwoFiles = await this.context.Set<ApplicationTwoFile>()
                        .Where(a => a.ApplicationTwoId == dto.Id)
                        .ToListAsync();

                    var courts = await this.context.Set<ApplicationTwoAdministrativeCourt>()
                       .Where(s => s.ApplicationTwoId == dto.Id)
                       .ToListAsync();

                    var prosecutors = await this.context.Set<ApplicationTwoProsecutor>()
                        .Where(s => s.ApplicationTwoId == dto.Id)
                        .ToListAsync();

                    var ministriesOfInterior = await this.context.Set<ApplicationTwoMinistryOfInterior>()
                        .Where(s => s.ApplicationTwoId == dto.Id)
                        .ToListAsync();

                    this.context.Set<ApplicationTwoAuthority>().RemoveRange(applicationTwoAuthorities);
                    this.context.Set<ApplicationTwoActivityOffering>().RemoveRange(activityOfferings);
                    this.context.Set<ApplicationTwoCode>().RemoveRange(applicationTwoCodes);
                    this.context.Set<ApplicationTwoFile>().RemoveRange(applicationTwoFiles);
                    this.context.Set<ApplicationTwoAdministrativeCourt>().RemoveRange(courts);
                    this.context.Set<ApplicationTwoProsecutor>().RemoveRange(prosecutors);
                    this.context.Set<ApplicationTwoMinistryOfInterior>().RemoveRange(ministriesOfInterior);

                    this.context.Set<ApplicationTwo>().Remove(application);

                    await context.SaveChangesAsync();

                    await base.CancelModification(dto, applicationType);

                    await transaction.CommitAsync();
                }
                catch (DomainErrorException)
                {
                    await transaction.RollbackAsync();

                    throw;
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();

                    await this.loggingService.LogExceptionAsync(ex, LogType.ExceptionLog);

                    this.validation.ThrowErrorMessage(SystemErrorCode.System_InternalProblem);
                }
            }
        }

        protected override async Task EditApplication(ApplicationTwoDto dto)
        {
            dto.ValidateProperties(this.validation);
            MapFinancialAssurances(dto);

            var files = await this.context.Set<ApplicationTwoFile>()
                .Where(s => s.ApplicationTwoId == dto.Id)
                .ToListAsync();

            var codes = await this.context.Set<ApplicationTwoCode>()
                .Where(s => s.ApplicationTwoId == dto.Id)
                .ToListAsync();

            var activityOfferings = await this.context.Set<ApplicationTwoActivityOffering>()
                .Where(s => s.ApplicationTwoId == dto.Id)
                .ToListAsync();

            var authorities = await this.context.Set<ApplicationTwoAuthority>()
                .Where(s => s.ApplicationTwoId == dto.Id)
                .ToListAsync();

            var courts = await this.context.Set<ApplicationTwoAdministrativeCourt>()
                .Where(s => s.ApplicationTwoId == dto.Id)
                .ToListAsync(); 

            var prosecutors = await this.context.Set<ApplicationTwoProsecutor>()
                .Where(s => s.ApplicationTwoId == dto.Id)
                .ToListAsync(); 

            var ministriesOfInterior = await this.context.Set<ApplicationTwoMinistryOfInterior>()
                .Where(s => s.ApplicationTwoId == dto.Id)
                .ToListAsync();

            this.context.Set<ApplicationTwoFile>().RemoveRange(files);
            this.context.Set<ApplicationTwoActivityOffering>().RemoveRange(activityOfferings);
            this.context.Set<ApplicationTwoCode>().RemoveRange(codes);
            this.context.Set<ApplicationTwoAuthority>().RemoveRange(authorities);
            this.context.Set<ApplicationTwoAdministrativeCourt>().RemoveRange(courts);
            this.context.Set<ApplicationTwoProsecutor>().RemoveRange(prosecutors);
            this.context.Set<ApplicationTwoMinistryOfInterior>().RemoveRange(ministriesOfInterior);

            await base.EditApplication(dto);
        }

        private ApplicationTwo MapApplicationTwoFromDto(ApplicationTwoDto dto)
        {
            var applicationTwo = mapper.Map<ApplicationTwoDto, ApplicationTwo>(dto);

            applicationTwo.CommitState = CommitState.Pending;

            return applicationTwo;
        }

        private void ValidateCaseStatus(ApplicationTwo application)
        {
            if (application.CaseStatus == CaseStatus.Active)
            {
                validation.ThrowErrorMessage(ApplicationErrorCode.Application_CaseStatusNotCompleted);
            }
        }

        private void ValidateOperator(ApplicationTwo application)
        {
            if (application.OperatorId == null || application.OperatorId <= 0)
            {
                validation.ThrowErrorMessage(ValidationErrorCode.Validation_OperatorRequired);
            }
        }

        private void ValidateActivityOfferings(ApplicationTwo application)
        {
            if (application.ActivityOfferingType == null)
            {
                validation.ThrowErrorMessage(ApplicationErrorCode.Application_ActivityOfferingsNotCompleted);
            }

            if (application.ActivityOfferingType == ActivityOfferingType.Listed && !application.ApplicationTwoActivityOfferings.Any())
            {
                validation.ThrowErrorMessage(ApplicationErrorCode.Application_ActivityOfferingsNotCompleted);
            }

            if (application.ActivityOfferingType == ActivityOfferingType.NotListed && String.IsNullOrWhiteSpace(application.NotListedDescription))
            {
                validation.ThrowErrorMessage(ApplicationErrorCode.Application_ActivityOfferingsNotCompleted);
            }

            if (application.ActivityOfferingType == ActivityOfferingType.DiffuseNature && String.IsNullOrWhiteSpace(application.DiffuseNatureDescription))
            {
                validation.ThrowErrorMessage(ApplicationErrorCode.Application_ActivityOfferingsNotCompleted);
            }

        }

        private void MapFinancialAssurances(ApplicationTwoDto dto)
        {
            if (dto.HasNoCollateral)
            {
                dto.InsurancePolicyId = null;
                dto.BankGuaranteeId = null;
                dto.MortgageId = null;
                dto.StakeId = null;

                return;
            }

            if (!dto.HasInsurancePolicy)
            {
                dto.InsurancePolicyId = null;
            }

            if (!dto.HasBankGuarantee)
            {
                dto.BankGuaranteeId = null;
            }

            if (!dto.HasMortgage)
            {
                dto.MortgageId = null;
            }

            if (!dto.HasStake)
            {
                dto.StakeId = null;
            }
        }

        private async Task TranslateFields(ApplicationTwoDto dto)
        {
            var translator = new Translator(deepLTranslateConfiguration.ApiKey);

            TextResult actionsTranslate = new TextResult("", LanguageCode.Bulgarian, 0, null);
            TextResult occurrenceDateDescriptionTranslate = new TextResult("", LanguageCode.Bulgarian, 0, null);
            TextResult procedureInitiatedDateDescriptionTranslate = new TextResult("", LanguageCode.Bulgarian, 0, null);

            if (!String.IsNullOrEmpty(dto.ActionsTaken))
            {
                actionsTranslate = await translator.TranslateTextAsync(dto.ActionsTaken, LanguageCode.Bulgarian, LanguageCode.EnglishBritish);
            }

            if (!String.IsNullOrEmpty(dto.OccurrenceDateDescription))
            {
                occurrenceDateDescriptionTranslate = await translator.TranslateTextAsync(dto.OccurrenceDateDescription, LanguageCode.Bulgarian, LanguageCode.EnglishBritish);
            }

            if (!String.IsNullOrEmpty(dto.ProcedureInitiatedDateDescription))
            {
                procedureInitiatedDateDescriptionTranslate = await translator.TranslateTextAsync(dto.ProcedureInitiatedDateDescription, LanguageCode.Bulgarian, LanguageCode.EnglishBritish);
            }

            dto.ActionsTakenAlt = actionsTranslate.Text;
            dto.OccurrenceDateDescriptionAlt = occurrenceDateDescriptionTranslate.Text;
            dto.ProcedureInitiatedDateDescriptionAlt = procedureInitiatedDateDescriptionTranslate.Text;
        }
    }
}
