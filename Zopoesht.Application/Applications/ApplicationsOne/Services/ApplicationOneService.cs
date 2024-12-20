using AutoMapper;
using AutoMapper.QueryableExtensions;
using DeepL.Model;
using DeepL;
using Microsoft.EntityFrameworkCore;
using Zopoesht.Application.Applications.ApplicationsOne.Dtos;
using Zopoesht.Application.Applications.ApplicationsOne.Interfaces;
using Zopoesht.Application.Applications.ApplicationsTwo.Dtos;
using Zopoesht.Application.Applications.Common.Dtos;
using Zopoesht.Application.Applications.Common.Services;
using Zopoesht.Application.Common;
using Zopoesht.Application.Common.Dtos;
using Zopoesht.Application.Common.Extensions;
using Zopoesht.Application.Infrastructure.Services;
using Zopoesht.Application.Reports.Dtos;
using Zopoesht.Application.Reports.Interfaces;
using Zopoesht.Application.Users.Interfaces;
using Zopoesht.Data.Applications.ApplicationOne;
using Zopoesht.Data.Applications.ApplicationOne.Enums;
using Zopoesht.Data.Applications.ApplicationTwo;
using Zopoesht.Data.Applications.Common.Enums;
using Zopoesht.Data.Applications.Common.Models;
using Zopoesht.Data.Common.Enums;
using Zopoesht.Data.Logs.Enums;
using Zopoesht.Data.Users.Constants;
using Zopoesht.Infrastructure.DomainValidation;
using Zopoesht.Infrastructure.DomainValidation.Enums;
using Zopoesht.Infrastructure.DomainValidation.Models;
using Zopoesht.Infrastructure.Emails;
using Zopoesht.Infrastructure.Interfaces.Contexts;
using Zopoesht.Infrastructure.Interfaces.Users;
using Zopoesht.Infrastructure.Logs;
using Zopoesht.Infrastructure.AppSettings.DeepLTranslateConfiguration;
using Zopoesht.Infrastructure.AppSettings;

namespace Zopoesht.Application.Applications.ApplicationsOne.Services
{
    public class ApplicationOneService : ApplicationService<ApplicationOne, ApplicationOneDto>, IApplicationOneService
    {

        private readonly IAppDbContext context;
        private readonly DomainValidationService validation;
        private readonly RoleService roleService;
        private readonly IMapper mapper;
        private readonly IUserContext userContext;
        private readonly ILoggingService loggingService;
        private readonly IReportService reportService;
        private readonly DeepLTranslateConfigurationSettings deepLTranslateConfiguration;

        public ApplicationOneService(
            DomainValidationService validation,
            RoleService roleService,
            IAppDbContext context,
            IMapper mapper,
            IUserContext userContext,
            IEmailService emailService,
            IUserService userService,
            ILoggingService loggingService,
            IReportService reportService
            )
            : base(context, userContext, mapper, validation, roleService, emailService, userService, loggingService)
        {
            this.context = context;
            this.validation = validation;
            this.roleService = roleService;
            this.mapper = mapper;
            this.userContext = userContext;
            this.loggingService = loggingService;
            this.reportService = reportService;
            this.deepLTranslateConfiguration = AppSettingsConfiguration.DeepLTranslateConfiguration;
        }

        public override async Task Create(ApplicationOneDto dto)
        {
            dto.ValidateProperties(this.validation);

            if (dto.Applicant.ApplicantType == ApplicantType.Authority)
            {
                ValidateAuthority(dto);
            }

            using (var transaction = await context.BeginTransactionAsync())
            {
                try
                {
                    await this.TranslateFields(dto);

                    var applicationOne = MapApplicationOneFromDto(dto);
                    applicationOne.RegisterNumber = GenerateRegisterNumber();

                    await SaveApplicationAsync(applicationOne);

                    var appHistory = new ApplicationHistoryDto()
                    {
                        ApplicationId = applicationOne.Id,
                        RootId = applicationOne.RootId,
                        UserFullName = this.userContext.Username,
                        Type = ApplicationHistoryType.Create
                    };

                    await this.CreateHistory(appHistory, ApplicationType.ApplicationOne);

                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();

                    throw new Exception(ex.Message);
                }
            }
        }

        public override async Task<ApplicationOneDto> SaveModification(ApplicationOneDto dto)
        {
            this.ValidatePermissionDtoException(dto.ApplicationOneAuthorities, dto.Applicant.Authority?.Id);
            dto.ValidateProperties(this.validation);

            if (dto.Applicant.ApplicantType == ApplicantType.Authority)
            {
                ValidateAuthority(dto);
            }

            dto.ApplicationOneFiles.ForEach(f => f.ZopoeshtAttachedFileId = 0);

            return await base.SaveModification(dto);
        }

        public override async Task<ApplicationOneDto> BeginModification(ApplicationOneDto dto, CommitState commitState, ApplicationType applicationType)
        {
            this.ValidatePermissionDtoException(dto.ApplicationOneAuthorities, dto.Applicant.Authority?.Id);

            if (dto.Applicant.ApplicantType == ApplicantType.Authority)
            {
                ValidateAuthority(dto);
            }

            dto.ApplicationOneFiles.ForEach(f =>
            {
                f.ZopoeshtAttachedFileId = 0;
                f.ZopoeshtAttachedFile.Id = 0;
            });

            return await base.BeginModification(dto, commitState, applicationType);
        }

        public override async Task<ApplicationOneDto> FinishModification(ApplicationOneDto dto, CommitState state, ApplicationType applicationType)
        {
            this.ValidatePermissionDtoException(dto.ApplicationOneAuthorities, dto.Applicant.Authority?.Id);
            dto.ValidateProperties(this.validation);
            
            if (dto.Applicant.ApplicantType == ApplicantType.Authority)
            {
                ValidateAuthority(dto);
            }

            await this.TranslateFields(dto);

            dto.ApplicationOneFiles.ForEach(f => f.ZopoeshtAttachedFileId = 0);

            return await base.FinishModification(dto, state, applicationType);
        }

        public override async Task CancelModification(ApplicationOneDto dto, ApplicationType applicationType)
        {
            using (var transaction = await this.context.BeginTransactionAsync())
            {
                try
                {
                    var application = await this.context.Set<ApplicationOne>()
                        .Include(a => a.Applicant)
                        .Include(a => a.ApplicationOneAuthorities)
                        .Include(a => a.ApplicationOneFiles)
                        .SingleOrDefaultAsync(a => a.Id == dto.Id);

                    if (application == null)
                    {
                        validation.ThrowErrorMessage(ApplicationErrorCode.Application_DoesNotExist);
                    }

                    this.ValidatePermissionModelException(application.ApplicationOneAuthorities, application.Applicant.AuthorityId);

                    var applicationOneAuthorities = await this.context.Set<ApplicationOneAuthority>()
                        .Where(a => a.ApplicationOneId == dto.Id)
                        .ToListAsync();

                    var applicationOneFiles = await this.context.Set<ApplicationOneFile>()
                        .Where(a => a.ApplicationOneId == dto.Id)
                        .ToListAsync();

                    var applicationOneTeritorrialRanges = await this.context.Set<ApplicationOneTerritorialRange>()
                        .Where(a => a.ApplicationOneBasicId == dto.ApplicationOneBasic.Id)
                        .ToListAsync();

                    var applicationOneAffectedCountries = await this.context.Set<ApplicationOneAffectedCountry>()
                        .Where(a => a.ApplicationOneBasicId == dto.ApplicationOneBasic.Id)
                        .ToListAsync();

                    var applicationOneActivityOfferings = await this.context.Set<ApplicationOneActivityOffering>()
                        .Where(a => a.ApplicationOneBasicId == dto.ApplicationOneBasic.Id)
                        .ToListAsync();

                    var basic = await this.context.Set<ApplicationOneBasic>()
                        .Where(s => s.Id == dto.ApplicationOneBasic.Id)
                        .SingleOrDefaultAsync();

                    context.Set<ApplicationOneAuthority>().RemoveRange(applicationOneAuthorities);
                    context.Set<ApplicationOneFile>().RemoveRange(applicationOneFiles);
                    context.Set<ApplicationOneTerritorialRange>().RemoveRange(applicationOneTeritorrialRanges);
                    context.Set<ApplicationOneAffectedCountry>().RemoveRange(applicationOneAffectedCountries);
                    context.Set<ApplicationOneActivityOffering>().RemoveRange(applicationOneActivityOfferings);
                    context.Set<ApplicationOneBasic>().Remove(basic);
                    context.Set<ApplicationOne>().Remove(application);

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

        public override async Task ChangeApplicationStateAndCreateHistory(int applicationId, CommitState state, ApplicationType applicationType)
        {
            var application = await this.context.Set<ApplicationOne>()
                .Where(s => s.Id == applicationId)
                .Include(s => s.ApplicationOneBasic)
                .Include(s => s.Applicant)
                .Include(s => s.ApplicationOneAuthorities)
                .SingleOrDefaultAsync();

            if (state == CommitState.Entered && !application.ApplicationOneBasic.OperatorId.HasValue)
            {
                validation.ThrowErrorMessage(ValidationErrorCode.Validation_OperatorRequired);
            }

            this.ValidatePermissionModelException(application.ApplicationOneAuthorities, application.Applicant.AuthorityId);

            await base.ChangeApplicationStateAndCreateHistory(applicationId, state, applicationType);
        }

        public async Task<SearchResultItemDto<ApplicationOneSearchResultDto>> GetFiltered(ApplicationOneSearchFilterDto model, CancellationToken cancellationToken)
        {
            model.ValidateProperties(this.validation);

            var applications = this.context.Set<ApplicationOne>()
                .Where(s => s.CommitState != CommitState.Archived &&
                    s.CommitState != CommitState.Deleted)
                .Include(a => a.ApplicationOneBasic)
                .Include(a => a.ApplicationOneAuthorities)
                    .ThenInclude(s => s.Authority)
                .Include(a => a.Applicant)
                .Include(a => a.Applicant.Authority)
                .Include(a => a.Applicant.Individual)
                .Include(a => a.Applicant.LegalEntity)
                .Include(a => a.Applicant.Operator)
                .Include(a => a.ApplicationTwos)
                    .ThenInclude(a => a.ApplicationTwoAuthorities)
                .AsQueryable();
                
            var filteredApplications = model
                .WhereBuilder(applications)
                .OrderByDescending(a => a.RootId)
                .ProjectTo<ApplicationOneSearchResultDto>(this.mapper.ConfigurationProvider)
                .AsQueryable();

            return new SearchResultItemDto<ApplicationOneSearchResultDto>
            {
                TotalCount = filteredApplications.Count(),
                Items = await filteredApplications
                    .ApplyPagination(model.Offset, model.Limit)
                    .ToListAsync()
            };
        }

        public override async Task<List<ApplicationOneSearchResultDto>> GetByState<ApplicationOneSearchResultDto>(CommitState commitState)
        {
            var applications = this.context.Set<ApplicationOne>()
                .Where(s => s.CommitState == commitState)
                .Include(a => a.ApplicationOneBasic)
                .Include(a => a.ApplicationOneAuthorities)
                .Include(a => a.Applicant)
                .Include(a => a.Applicant.Authority)
                .Include(a => a.Applicant.Individual)
                .Include(a => a.Applicant.LegalEntity)
                .Include(a => a.Applicant.Operator)
                .Include(a => a.ApplicationTwos)
                .AsQueryable();

            return await applications
                .OrderByDescending(e => e.Id)
                .ProjectTo<ApplicationOneSearchResultDto>(this.mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public override async Task<ApplicationOneDto> GetById(int id)
        {
            var item = await base.GetById(id);

            var appTwo = await this.context.Set<ApplicationTwo>()
                .Where(s => s.ApplicationOneId == id)
                .OrderByDescending(s => s.CreateDate)
                .FirstOrDefaultAsync();

            var locks = await this.context.Set<ApplicationLock>()
                .AsNoTracking()
                .Where(s => s.ApplicationType == ApplicationType.ApplicationOne && s.ApplicationId == id && s.IsLocked)
                .OrderBy(s => s.LockedDate)
                .ProjectTo<ApplicationLockDto>(this.mapper.ConfigurationProvider)
                .ToListAsync();

            if (appTwo != null)
            {
                item.HasApplicationTwo = true;
                item.ApplicationTwoRootId = appTwo.Id;
            }

            item.HasPermissionToEdit = locks.Any(currentLock => currentLock.UserId == this.userContext.UserId);

            item.HasPermissionToControl = this.ValidatePermissionDto(item.ApplicationOneAuthorities, item.Applicant.Authority?.Id);

            item.Locks = locks;

            return item;
        }

        public async Task DeleteApplication(int id, CancellationToken cancellationToken)
        {
            using (var transaction = await this.context.BeginTransactionAsync())
            {
                try
                {
                    var application = await this.context.Set<ApplicationOne>()
                        .Where(a => a.Id == id)
                        .Include(a => a.Applicant)
                        .Include(a => a.ApplicationOneAuthorities)
                        .SingleOrDefaultAsync();

                    if (application == null)
                    {
                        validation.ThrowErrorMessage(ApplicationErrorCode.Application_DoesNotExist);
                    }

                    this.ValidatePermissionModelException(application.ApplicationOneAuthorities, application.Applicant.AuthorityId);

                    if (application.CommitState != CommitState.Pending && application.CommitState != CommitState.Completed)
                    {
                        validation.ThrowErrorMessage(ApplicationErrorCode.Application_CanNotDelete);
                    }

                    var appTwo = await context.Set<ApplicationTwo>()
                        .Where(s => s.ApplicationOneId == id)
                        .OrderByDescending(s => s.CreateDate)
                        .FirstOrDefaultAsync();

                    if (appTwo != null)
                    {
                        validation.ThrowErrorMessage(ApplicationErrorCode.Application_CanNotDelete);
                    }

                    application.CommitState = CommitState.Deleted;
                    await context.SaveChangesAsync();

                    var appHistory = new ApplicationHistoryDto()
                    {
                        ApplicationId = application.Id,
                        RootId = application.RootId,
                        UserFullName = this.userContext.Username,
                        Type = ApplicationHistoryType.Delete
                    };

                    await this.CreateHistory(appHistory, ApplicationType.ApplicationOne);

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

        public async Task<ApplicationOneDto> RestoreApplication(int id, CancellationToken cancellationToken)
        {
            using (var transaction = await this.context.BeginTransactionAsync())
            {
                try
                {
                    var application = await this.context.Set<ApplicationOne>()
                        .Where(a => a.Id == id)
                        .Include(a => a.Applicant)
                        .Include(a => a.ApplicationOneAuthorities)
                        .SingleOrDefaultAsync();

                    if (application == null)
                    {
                        validation.ThrowErrorMessage(ApplicationErrorCode.Application_DoesNotExist);
                    }

                    this.ValidatePermissionModelException(application.ApplicationOneAuthorities, application.Applicant.AuthorityId);

                    if (application.CommitState != CommitState.Deleted)
                    {
                        validation.ThrowErrorMessage(ApplicationErrorCode.Application_CanNotRestore);
                    }

                    application.CommitState = CommitState.Pending;
                    await context.SaveChangesAsync();

                    var appHistory = new ApplicationHistoryDto()
                    {
                        ApplicationId = application.Id,
                        RootId = application.RootId,
                        UserFullName = this.userContext.Username,
                        Type = ApplicationHistoryType.Restore
                    };

                    await this.CreateHistory(appHistory, ApplicationType.ApplicationOne);

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

            return await this.GetById(id);
        }

        public async Task<int> GetIdByAppTwoId(int appTwoId, CancellationToken cancellationToken)
        {
            var appTwo = await this.context.Set<ApplicationTwo>()
                .SingleOrDefaultAsync(a => a.Id == appTwoId);

            if (appTwo == null)
            {
                validation.ThrowErrorMessage(ApplicationErrorCode.Application_DoesNotExist);
            }

            return appTwo.ApplicationOneId;
        }

        public async Task<byte[]> ExportExcelFile(ReportSearchFilterDto filter, CancellationToken cancellationToken)
        {
            var result = await reportService.GetFiltered(filter, cancellationToken);

            return ExcelService.CreateExcelDoc(result.Items, filter);
        }

        protected override async Task EditApplication(ApplicationOneDto dto)
        {
            dto.ValidateProperties(this.validation);

            var files = await this.context.Set<ApplicationOneFile>()
                .Where(s => s.ApplicationOneId == dto.Id)
                .ToListAsync();

            var authorities = await this.context.Set<ApplicationOneAuthority>()
                .Where(s => s.ApplicationOneId == dto.Id)
                .ToListAsync();

            var territorialRanges = await this.context.Set<ApplicationOneTerritorialRange>()
                .Where(s => s.ApplicationOneBasicId == dto.ApplicationOneBasic.Id)
                .ToListAsync();

            var affectedCountries = await this.context.Set<ApplicationOneAffectedCountry>()
                .Where(s => s.ApplicationOneBasicId == dto.ApplicationOneBasic.Id)
                .ToListAsync();

            var activityOfferings = await this.context.Set<ApplicationOneActivityOffering>()
                .Where(s => s.ApplicationOneBasicId == dto.ApplicationOneBasic.Id)
                .ToListAsync();

            this.context.Set<ApplicationOneFile>().RemoveRange(files);

            this.context.Set<ApplicationOneAuthority>().RemoveRange(authorities);

            this.context.Set<ApplicationOneTerritorialRange>().RemoveRange(territorialRanges);

            this.context.Set<ApplicationOneAffectedCountry>().RemoveRange(affectedCountries);

            this.context.Set<ApplicationOneActivityOffering>().RemoveRange(activityOfferings);

            var application = await this.context.Set<ApplicationOne>()
                .Where(s => s.Id == dto.Id)
                .Include(s => s.ApplicationOneBasic)
                .SingleOrDefaultAsync();

            application = this.mapper.Map<ApplicationOneDto, ApplicationOne>(dto, application);

            await context.SaveChangesAsync();

        }

        private async Task TranslateFields(ApplicationOneDto dto)
        {
            var translator = new Translator(deepLTranslateConfiguration.ApiKey);

            TextResult descriptionTranslate = new TextResult("", LanguageCode.Bulgarian, 0, null);


            if (!String.IsNullOrEmpty(dto.ApplicationOneBasic.Name))
            {
                descriptionTranslate = await translator.TranslateTextAsync(dto.ApplicationOneBasic.Name, LanguageCode.Bulgarian, LanguageCode.EnglishBritish);
            }

            dto.ApplicationOneBasic.NameAlt = descriptionTranslate.Text;
        }

        private ApplicationOne MapApplicationOneFromDto(ApplicationOneDto dto)
        {
            var applicationOne = mapper.Map<ApplicationOneDto, ApplicationOne>(dto);

            if (dto.Applicant.ApplicantType == ApplicantType.LegalEntity
                || dto.Applicant.ApplicantType == ApplicantType.NonGovernmentalOrganization
                || dto.Applicant.ApplicantType == ApplicantType.Individual)
            {
                applicationOne.ApplicationOneType = ApplicationOneType.ReportedDamage;
            }

            applicationOne.CommitState = CommitState.Pending;

            return applicationOne;
        }

        private string GenerateRegisterNumber()
        {
            var random = new Random();

            string registerNumber = "";

            var usedRegisterNumbers = this.context.Set<ApplicationOne>()
                .AsNoTracking()
                .Select(s => s.RegisterNumber)
                .ToHashSet();

            string alphabetChars = "ABCDEFGHKLMNPQRSTUVWXYZ";
            string numberChars = "0123456789";

            while (string.IsNullOrWhiteSpace(registerNumber) || usedRegisterNumbers.Contains(registerNumber))
            {
                string alphabets = new string(Enumerable.Repeat(alphabetChars, 1).Select(s => s[random.Next(s.Length)]).ToArray());
                string numbers = new string(Enumerable.Repeat(numberChars, 4).Select(s => s[random.Next(s.Length)]).ToArray());
                registerNumber = alphabets + numbers;
            }


            return registerNumber;
        }

        private void ValidateAuthority(ApplicationOneDto dto)
        {
            if (userContext.Role != UserRoleAliases.ADMINISTRATOR && userContext.Role != UserRoleAliases.MOSV
                && dto.Applicant.Authority.Id != userContext.AuthorityId)
            {
                validation.ThrowErrorMessage(ApplicationErrorCode.Application_NotEnoughPermissions);
            }
        }
    }
}
