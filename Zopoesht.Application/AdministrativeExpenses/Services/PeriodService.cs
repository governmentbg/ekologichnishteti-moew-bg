using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Zopoesht.Application.AdministrativeExpenses.Dtos;
using Zopoesht.Application.AdministrativeExpenses.Interfaces;
using Zopoesht.Application.Infrastructure.Services;
using Zopoesht.Data.AdministrativeExpenses.Enums;
using Zopoesht.Data.Nomenclatures;
using Zopoesht.Data.Users.Constants;
using Zopoesht.Infrastructure.DomainValidation;
using Zopoesht.Infrastructure.DomainValidation.Enums;
using Zopoesht.Infrastructure.Interfaces.Contexts;
using Zopoesht.Infrastructure.Interfaces.Users;

namespace Zopoesht.Application.AdministrativeExpenses.Services
{
    public class PeriodService : IPeriodService
    {
        private readonly IAppDbContext context;
        private readonly IMapper mapper;
        private readonly IUserContext userContext;
        private readonly RoleService roleService;
        private readonly DomainValidationService validation;

        public PeriodService(
            IAppDbContext context,
            IMapper mapper,
            IUserContext userContext,
            RoleService roleService,
            DomainValidationService validation)
        {
            this.context = context;
            this.mapper = mapper;
            this.userContext = userContext;
            this.roleService = roleService;
            this.validation = validation;
        }

        public async Task<List<PeriodDto>> GetAll(CancellationToken cancellationToken)
        {
            List<PeriodDto> periods = await context.Set<Period>()
                .ProjectTo<PeriodDto>(mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            periods.ForEach(p => p.AnnualAdministrativeExpenses = p.AnnualAdministrativeExpenses
                .Where(e => e.State == AnnualAdministrativeExpenseState.Active &&
                                  (userContext.Role == UserRoleAliases.ADMINISTRATOR || e.AuthorityId == userContext.AuthorityId))
                .ToList());

            return periods;
        }


        public async Task<int> Add(PeriodDto model, CancellationToken cancellationToken)
        {
            model.ValidateProperties(validation);

            var period = mapper.Map<PeriodDto, Period>(model);

            await context.Set<Period>().AddAsync(period);
            await context.SaveChangesAsync();

            return period.Id;
        }

        public async Task Update(PeriodDto model, CancellationToken cancellationToken)
        {
            model.ValidateProperties(validation);

            var period = await context.Set<Period>()
                .Include(p => p.AnnualAdministrativeExpenses)
                .SingleOrDefaultAsync(p => p.Id == model.Id);

            if (period == null)
            {
                validation.ThrowErrorMessage(AdministrativeExpensesErrorCode.AdministrativeExpenses_PeriodDoesNotExist);
            }

            if (period.AnnualAdministrativeExpenses.Any())
            {
                validation.ThrowErrorMessage(AdministrativeExpensesErrorCode.AdministrativeExpenses_PeriodCannotUpdate);
            }

            period = mapper.Map<PeriodDto, Period>(model, period);

            await context.SaveChangesAsync();
        }
    }
}
