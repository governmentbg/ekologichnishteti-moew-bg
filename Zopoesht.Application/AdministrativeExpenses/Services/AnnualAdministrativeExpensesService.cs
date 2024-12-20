using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Zopoesht.Application.AdministrativeExpenses.Dtos;
using Zopoesht.Application.AdministrativeExpenses.Interfaces;
using Zopoesht.Application.Infrastructure.Services;
using Zopoesht.Data.AdministrativeExpenses;
using Zopoesht.Data.AdministrativeExpenses.Enums;
using Zopoesht.Data.Logs.Enums;
using Zopoesht.Data.Nomenclatures;
using Zopoesht.Data.Users;
using Zopoesht.Data.Users.Constants;
using Zopoesht.Infrastructure.DomainValidation;
using Zopoesht.Infrastructure.DomainValidation.Enums;
using Zopoesht.Infrastructure.Interfaces.Contexts;
using Zopoesht.Infrastructure.Interfaces.Users;
using Zopoesht.Infrastructure.Logs;

namespace Zopoesht.Application.AdministrativeExpenses.Services
{
    public class AnnualAdministrativeExpensesService : IAnnualAdministrativeExpensesService
    {
        private readonly IAppDbContext context;
        private readonly IMapper mapper;
        private readonly IUserContext userContext;
        private readonly ILoggingService loggingService;
        private readonly RoleService roleService;
        private readonly DomainValidationService validation;

        public AnnualAdministrativeExpensesService(
            IAppDbContext context,
            IMapper mapper,
            IUserContext userContext,
            ILoggingService loggingService,
            RoleService roleService,
            DomainValidationService validation)
        {
            this.context = context;
            this.mapper = mapper;
            this.userContext = userContext;
            this.loggingService = loggingService;
            this.roleService = roleService;
            this.validation = validation;
        }

        public async Task<AnnualAdministrativeExpensesHistorySearchResultDto> GetHistory(int rootId, CancellationToken cancellationToken)
        {
            var histories = await context.Set<AnnualAdministrativeExpensesHistory>()
                .Include(a => a.User)
                    .ThenInclude(u => u.Authority)
                .Include(a => a.AnnualAdministrativeExpense)
                    .ThenInclude(a => a.Authority)
                 .Include(a => a.AnnualAdministrativeExpense)
                    .ThenInclude(a => a.Period)
                .Where(a => a.RootId == rootId)
                .OrderByDescending(a => a.Id)
                .ToListAsync();

            if (!histories.Any())
            {
                validation.ThrowErrorMessage(AdministrativeExpensesErrorCode.AdministrativeExpenses_AnnualAdministrativeExpenseDoesNotExist);
            }

            return new AnnualAdministrativeExpensesHistorySearchResultDto
            {
                AuthorityName = histories.First().AnnualAdministrativeExpense.Authority.Name,
                PeriodName = histories.First().AnnualAdministrativeExpense.Period.Name,
                Histories = histories
                    .Select(h => mapper.Map<AnnualAdministrativeExpensesHistoryDto>(h))
                    .ToList()
            };
        }

        public async Task<AnnualAdministrativeExpensesDto> Add(AnnualAdministrativeExpensesDto model, CancellationToken cancellationToken)
        {
            model.ValidateProperties(validation);
            ValidateAuthority(model);

            var period = await context.Set<Period>()
                .Include(p => p.AnnualAdministrativeExpenses)
                .SingleOrDefaultAsync(p => p.Id == model.PeriodId);

            if (period == null)
            {
                validation.ThrowErrorMessage(AdministrativeExpensesErrorCode.AdministrativeExpenses_PeriodDoesNotExist);
            }

            if (period.AnnualAdministrativeExpenses.Any(a => a.AuthorityId == model.AuthorityId))
            {
                validation.ThrowErrorMessage(AdministrativeExpensesErrorCode.AdministrativeExpenses_AlreadyExistingExpenseWithAuthority);
            }

            var annualAdministrativeExpense = mapper.Map<AnnualAdministrativeExpensesDto, AnnualAdministrativeExpenses>(model);
            annualAdministrativeExpense.State = AnnualAdministrativeExpenseState.Active;
            
            using (var transaction = await context.BeginTransactionAsync())
            {
                try
                {
                    await SaveAnnualAdministrativeExpense(annualAdministrativeExpense);

                    await CreateHistory(annualAdministrativeExpense);

                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();

                    await loggingService.LogExceptionAsync(ex, LogType.ExceptionLog);

                    validation.ThrowErrorMessage(SystemErrorCode.System_InternalProblem);
                }
            }

            return mapper.Map<AnnualAdministrativeExpenses, AnnualAdministrativeExpensesDto>(annualAdministrativeExpense);
        }

        public async Task Update(AnnualAdministrativeExpensesDto model, CancellationToken cancellationToken)
        {
            model.ValidateProperties(validation);
            ValidateAuthority(model);

            var annualAdministrativeExpenses = await context.Set<AnnualAdministrativeExpenses>()
                .SingleOrDefaultAsync(a => a.Id == model.Id);

            if (annualAdministrativeExpenses == null)
            {
                validation.ThrowErrorMessage(AdministrativeExpensesErrorCode.AdministrativeExpenses_AnnualAdministrativeExpenseDoesNotExist);
            }

            var period = await context.Set<Period>()
                .Include(p => p.AnnualAdministrativeExpenses)
                .SingleOrDefaultAsync(p => p.Id == model.PeriodId);

            if (period == null)
            {
                validation.ThrowErrorMessage(AdministrativeExpensesErrorCode.AdministrativeExpenses_PeriodDoesNotExist);
            }

            using (var transaction = await context.BeginTransactionAsync())
            {
                try
                {
                    var annualAdministrativeExpense = await CopyAnnualAdministrativeExpense(model);

                    await CreateHistory(annualAdministrativeExpense);

                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();

                    await loggingService.LogExceptionAsync(ex, LogType.ExceptionLog);

                    validation.ThrowErrorMessage(SystemErrorCode.System_InternalProblem);
                }
            }
        }

        private async Task SaveAnnualAdministrativeExpense(AnnualAdministrativeExpenses annualAdministrativeExpense)
        {
            await context.Set<AnnualAdministrativeExpenses>().AddAsync(annualAdministrativeExpense);
            await context.SaveChangesAsync();

            annualAdministrativeExpense.RootId = annualAdministrativeExpense.Id;
            await context.SaveChangesAsync();
        }

        private async Task CreateHistory(AnnualAdministrativeExpenses annualAdministrativeExpense)
        {
            var history = new AnnualAdministrativeExpensesHistory
            {
                UserId = userContext.UserId,
                UserFullName = await context.Set<User>().Where(e => e.Id == userContext.UserId).Select(e => e.FirstName + " " + e.LastName).SingleAsync(),
                ModificationDate = DateTime.Now,
                AnnualAdministrativeExpenseId = annualAdministrativeExpense.Id,
                RootId = annualAdministrativeExpense.RootId
            };

            await context.Set<AnnualAdministrativeExpensesHistory>().AddAsync(history);
            await context.SaveChangesAsync();
        }

        private async Task<AnnualAdministrativeExpenses> CopyAnnualAdministrativeExpense(AnnualAdministrativeExpensesDto dto)
        {
            var annualAdministrativeExpense = mapper.Map<AnnualAdministrativeExpensesDto, AnnualAdministrativeExpenses>(dto);
            annualAdministrativeExpense.State = AnnualAdministrativeExpenseState.Active;

            await context.Set<AnnualAdministrativeExpenses>().AddAsync(annualAdministrativeExpense);
            await context.SaveChangesAsync();

            context.Set<AnnualAdministrativeExpenses>()
                .Where(a => a.Id == dto.Id)
                .ExecuteUpdate(x => x.SetProperty(a => a.State, AnnualAdministrativeExpenseState.Archived));

            return annualAdministrativeExpense;
        }

        private void ValidateAuthority(AnnualAdministrativeExpensesDto dto)
        {
            if (userContext.Role != UserRoleAliases.ADMINISTRATOR &&
                (userContext.Role == UserRoleAliases.AUTHORIZED_ENTITY_ACTIVE && dto.AuthorityId != userContext.AuthorityId))
            {
                validation.ThrowErrorMessage(AdministrativeExpensesErrorCode.AdministrativeExpenses_NotEnoughPermissions);
            }
        }
    }
}
