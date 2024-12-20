using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Zopoesht.Application.Applications.Common.Dtos;
using Zopoesht.Application.Common;
using Zopoesht.Application.Infrastructure.Services;
using Zopoesht.Application.Users.Interfaces;
using Zopoesht.Data.Applications.Common.Enums;
using Zopoesht.Data.Applications.Common.Models;
using Zopoesht.Data.Common.Enums;
using Zopoesht.Data.Logs.Enums;
using Zopoesht.Data.Users;
using Zopoesht.Data.Users.Constants;
using Zopoesht.Infrastructure.AppSettings;
using Zopoesht.Infrastructure.AppSettings.AuthConfiguration;
using Zopoesht.Infrastructure.DomainValidation;
using Zopoesht.Infrastructure.DomainValidation.Enums;
using Zopoesht.Infrastructure.Emails;
using Zopoesht.Infrastructure.Interfaces.Contexts;
using Zopoesht.Infrastructure.Interfaces.Users;
using Zopoesht.Infrastructure.Logs;

namespace Zopoesht.Application.Applications.Common.Services
{
    public abstract class ApplicationService<T, TDto>
        where T : ApplicationCommit
        where TDto : ApplicationCommitDto
    {
        private readonly IAppDbContext context;
        private readonly IUserContext userContext;
        private readonly IMapper mapper;
        private readonly DomainValidationService domainValidation;
        private readonly AuthConfigurationSettings authConfiguration;
        private readonly RoleService roleService;
        private readonly IEmailService emailService;
        private readonly IUserService userService;
        private readonly ILoggingService loggingService;


        public ApplicationService(
            IAppDbContext context,
            IUserContext userContext,
            IMapper mapper,
            DomainValidationService domainValidation,
            RoleService roleService,
            IEmailService emailService,
            IUserService userService,
            ILoggingService loggingService
            )
        {
            this.context = context;
            this.userContext = userContext;
            this.mapper = mapper;
            this.domainValidation = domainValidation;
            this.roleService = roleService;
            this.authConfiguration = AppSettingsConfiguration.AuthConfiguration;
            this.emailService = emailService;
            this.userService = userService;
            this.loggingService = loggingService;
        }

        public virtual async Task<TDto> GetById(int id)
        {
            var application = await this.context.Set<T>()
                .AsNoTracking()
                .Where(a => a.Id == id)
                .ProjectTo<TDto>(this.mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();

            if (application == null)
            {
                this.domainValidation.ThrowErrorMessage(ApplicationErrorCode.Application_DoesNotExist);
            }

            var applicationsCount = await context.Set<T>()
                .AsNoTracking()
                .CountAsync(e => e.RootId == application.RootId);

            application.HasHistory = applicationsCount > 0;

            return application;
        }

        public virtual async Task<List<TReturnDto>> GetByState<TReturnDto>(CommitState commitState)
        {
            var applications = this.context.Set<T>()
                .AsNoTracking()
                .Where(e => e.CommitState == commitState)
                .AsQueryable();

            return await applications
                .OrderByDescending(e => e.Id)
                .ProjectTo<TReturnDto>(this.mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<ApplicationHistoryResultDto> GetHistory(int rootId, ApplicationType type)
        {
            var histories = await this.context.Set<ApplicationHistory>()
                .Where(e => e.RootId == rootId && e.ApplicationType == type)
                .OrderByDescending(e => e.Id)
                .ProjectTo<ApplicationHistoryDto>(this.mapper.ConfigurationProvider)
                .ToListAsync();

            var recent = await this.context.Set<T>()
                .AsNoTracking()
                .Where(a => a.RootId == rootId)
                .OrderByDescending(a => a.Id)
                .FirstOrDefaultAsync();

            return new ApplicationHistoryResultDto()
            {
                RecentApplicationId = recent.Id,
                ApplicationHistoryDtos = histories
            };
        }

        public virtual async Task Create(TDto dto)
        {
            dto.ValidateProperties(this.domainValidation);

            var entity = this.mapper.Map<TDto, T>(dto);
            entity.CommitState = CommitState.Entered;

            await this.context.Set<T>().AddAsync(entity);
            await this.context.SaveChangesAsync();

            entity.RootId = entity.Id;
            await this.context.SaveChangesAsync();
        }

        public async Task ChangeApplicationState(int applicationId, CommitState commitState, string changeStateDescription, ApplicationType type)
        {
            var application = await context.Set<T>()
               .Where(s => s.Id == applicationId)
               .SingleOrDefaultAsync();

            application.CommitState = commitState;

            application.ChangeStateDescription = changeStateDescription;

            await context.SaveChangesAsync();
        }

        public virtual async Task<TDto> BeginModification(TDto dto, CommitState state, ApplicationType applicationType)
        {
            int newId = 0;

            using (var transaction = await this.context.BeginTransactionAsync())
            {
                try
                {
                    newId = await this.CopyApplication(dto);

                    await this.AddLock(newId, userContext.UserId, applicationType);

                    var appHistory = new ApplicationHistoryDto()
                    {
                        ApplicationId = dto.Id,
                        RootId = dto.RootId,
                        UserFullName = this.userContext.Username,
                        Type = ApplicationHistoryType.Modification
                    };

                    await this.CreateHistory(appHistory, applicationType);

                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();

                    await this.loggingService.LogExceptionAsync(ex, LogType.ExceptionLog);

                    this.domainValidation.ThrowErrorMessage(SystemErrorCode.System_InternalProblem);
                }
            }

            if (newId == 0)
            {
                await this.loggingService.LogExceptionAsync(new Exception("Internal error"), LogType.ExceptionLog);

                this.domainValidation.ThrowErrorMessage(SystemErrorCode.System_InternalProblem);
            }

            return await this.GetById(newId);
        }

        public virtual async Task<TDto> FinishModification(TDto dto, CommitState state, ApplicationType applicationType)
        {
            using (var transaction = await this.context.BeginTransactionAsync())
            {
                try
                {
                    var appHistory = new ApplicationHistoryDto()
                    {
                        ApplicationId = dto.Id,
                        RootId = dto.RootId,
                        UserFullName = this.userContext.Username,
                        Type = ApplicationHistoryType.FinishModification
                    };

                    await this.CreateHistory(appHistory, applicationType);

                    await this.EditApplication(dto);

                    await this.ChangeApplicationState(dto.Id, state, null, applicationType);

                    await this.RemoveLock(dto.Id, applicationType);

                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();

                    await this.loggingService.LogExceptionAsync(ex, LogType.ExceptionLog);

                    this.domainValidation.ThrowErrorMessage(SystemErrorCode.System_InternalProblem);
                }
            }

            return await GetById(dto.Id);
        }

        public virtual async Task ChangeApplicationStateAndCreateHistory(int applicationId, CommitState state, ApplicationType applicationType)
        {
            using (var transaction = await this.context.BeginTransactionAsync())
            {
                try
                {
                    var application = await context.Set<T>()
                        .Where(s => s.Id == applicationId)
                        .SingleOrDefaultAsync();

                    var appHistory = new ApplicationHistoryDto()
                    {
                        ApplicationId = application.Id,
                        RootId = application.RootId,
                        UserFullName = this.userContext.Username,
                        Type = ApplicationHistoryType.Complete
                    };

                    await this.CreateHistory(appHistory, applicationType);

                    await this.ChangeApplicationState(applicationId, state, null, applicationType);

                    await transaction.CommitAsync();
                }
                catch(Exception ex)
                {
                    await transaction.RollbackAsync();

                    await this.loggingService.LogExceptionAsync(ex, LogType.ExceptionLog);

                    this.domainValidation.ThrowErrorMessage(SystemErrorCode.System_InternalProblem);
                }
            }
        }

        public virtual async Task<TDto> SaveModification(TDto dto)
        {
            using (var transaction = await this.context.BeginTransactionAsync())
            {
                try
                {
                    await this.EditApplication(dto);

                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();

                    await this.loggingService.LogExceptionAsync(ex, LogType.ExceptionLog);

                    this.domainValidation.ThrowErrorMessage(SystemErrorCode.System_InternalProblem);
                }
            }

            return await GetById(dto.Id);
        }

        public virtual async Task CancelModification(TDto dto, ApplicationType applicationType)
        {
            var lastArchivedApplication = await context.Set<T>()
                .Where(a => a.RootId == dto.RootId)
                .OrderByDescending(a => a.Id)
                .FirstOrDefaultAsync();

            if (lastArchivedApplication == null)
            {
                domainValidation.ThrowErrorMessage(ApplicationErrorCode.Application_DoesNotExist);
            }

            lastArchivedApplication.CommitState = CommitState.Pending;

            var appHistory = new ApplicationHistoryDto()
            {
                ApplicationId = lastArchivedApplication.Id,
                RootId = lastArchivedApplication.RootId,
                UserFullName = this.userContext.Username,
                Type = ApplicationHistoryType.CancelModification
            };

            await this.CreateHistory(appHistory, applicationType);
        }

        protected virtual async Task EditApplication(TDto dto)
        {
            var application = await this.context.Set<T>()
                .Where(s => s.Id == dto.Id)
                .SingleOrDefaultAsync();

            application = this.mapper.Map<TDto, T>(dto, application);

            await context.SaveChangesAsync();
        }

        protected async Task<int> CopyApplication(TDto dto)
        {
            var application = this.mapper.Map<TDto, T>(dto);
            
            application.CommitState = CommitState.Modification;

            EntityService.ClearSkipProperties(application);
            this.context.Set<T>().Add(application);

            await this.context.SaveChangesAsync();

            this.context.Set<T>()
                .Where(e => e.Id == dto.Id)
                .ExecuteUpdate(x => x.SetProperty(a => a.CommitState, CommitState.Archived));

            return application.Id;
        }

        protected async Task CreateHistory(ApplicationHistoryDto dto, ApplicationType type)
        {
            var history = new ApplicationHistory
            {
                ApplicationId = dto.ApplicationId.Value,
                RootId = dto.RootId.Value,
                ModificationDate = DateTime.Now,
                Type = dto.Type,
                UserId = userContext.UserId,
                UserFullName = await this.context.Set<User>().Where(e => e.Id == userContext.UserId).Select(e => e.FirstName + " " + e.LastName).SingleAsync(),
                ApplicationType = type
            };

            this.context.Set<ApplicationHistory>().Add(history);

            await this.context.SaveChangesAsync();
        }

        protected async Task SaveApplicationAsync(T application)
        {
            context.Set<T>().Add(application);

            await context.SaveChangesAsync();

            application.RootId = application.Id;

            await context.SaveChangesAsync();
        }

        public async Task AddLock(int applicationId, int userId, ApplicationType applicationType)
        {
            try
            {
                var applicationLock = new ApplicationLock(applicationId, userId, applicationType);
                applicationLock.Lock();

                this.context.Set<ApplicationLock>().Add(applicationLock);

                await this.context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                await this.loggingService.LogExceptionAsync(ex, LogType.ExceptionLog);

                this.domainValidation.ThrowErrorMessage(SystemErrorCode.System_InternalProblem);
            }
        }

        public async Task RemoveLock(int applicationId, ApplicationType applicationType)
        {
            try
            {
                var locks = this.context.Set<ApplicationLock>()
                    .Where(s => s.ApplicationId == applicationId && s.ApplicationType == applicationType)
                    .AsQueryable();

                await locks.ForEachAsync(s => s.Unlock());

                await this.context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                await this.loggingService.LogExceptionAsync(ex, LogType.ExceptionLog);

                this.domainValidation.ThrowErrorMessage(SystemErrorCode.System_InternalProblem);
            }
        }

        // In model
        protected void ValidatePermissionModelException(IEnumerable<ApplicationAuthority> applicationAuthorities, int? mainAuthorityId)
        {
            if (!ValidatePermissionModel(applicationAuthorities, mainAuthorityId))
            {
                this.domainValidation.ThrowErrorMessage(ApplicationErrorCode.Application_NotEnoughPermissions);
            }
        }

        // In Dto
        protected void ValidatePermissionDtoException(IEnumerable<ApplicationAuthorityDto> applicationAuthorities, int? mainAuthorityId)
        {
            if (!ValidatePermissionDto(applicationAuthorities, mainAuthorityId))
            {
                this.domainValidation.ThrowErrorMessage(ApplicationErrorCode.Application_NotEnoughPermissions);
            }
        }

        protected bool ValidatePermissionDto(IEnumerable<ApplicationAuthorityDto> applicationAuthorities, int? mainAuthorityId)
        {
            if (this.roleService.ValidateRole(UserRoleAliases.ADMINISTRATOR) || this.roleService.ValidateRole(UserRoleAliases.MOSV))
            {
                return true;
            }

            var existsInApplicationAuthorities = applicationAuthorities.Any(a => a.AuthorityId == this.userContext.AuthorityId);

            if (mainAuthorityId.HasValue)
            {
                return existsInApplicationAuthorities || mainAuthorityId.Value == this.userContext.AuthorityId;
            }

            return existsInApplicationAuthorities;
        }

        protected bool ValidatePermissionModel(IEnumerable<ApplicationAuthority> applicationAuthorities, int? mainAuthorityId)
        {
            if (this.roleService.ValidateRole(UserRoleAliases.ADMINISTRATOR) || this.roleService.ValidateRole(UserRoleAliases.MOSV))
            {
                return true;
            }

            var existsInApplicationAuthorities = applicationAuthorities.Any(a => a.AuthorityId == this.userContext.AuthorityId);

            if (mainAuthorityId.HasValue)
            {
                return existsInApplicationAuthorities || mainAuthorityId.Value == this.userContext.AuthorityId;
            }

            return existsInApplicationAuthorities;
        }
    }
}
