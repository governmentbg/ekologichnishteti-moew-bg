using Microsoft.AspNetCore.Mvc;
using Zopoesht.Application.AdministrativeExpenses.Dtos;
using Zopoesht.Application.AdministrativeExpenses.Interfaces;
using Zopoesht.Application.Infrastructure.Services;
using Zopoesht.Data.Users.Constants;

namespace Zopoesht.Hosting.Controllers.AdministrativeExpenses
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnnualAdministrativeExpensesController : ControllerBase
    {
        private readonly IAnnualAdministrativeExpensesService annualAdministrativeExpensesService;
        private readonly RoleService roleService;

        public AnnualAdministrativeExpensesController(
            IAnnualAdministrativeExpensesService annualAdministrativeExpensesService,
            RoleService roleService
            )
        {
            this.annualAdministrativeExpensesService = annualAdministrativeExpensesService;
            this.roleService = roleService;
        }

        [HttpGet("{rootId:int}")]
        public async Task<AnnualAdministrativeExpensesHistorySearchResultDto> GetAll([FromRoute] int rootId, CancellationToken cancellationToken)
        {
            this.roleService.ValidateRolesException(UserRoleAliases.ADMINISTRATOR);

            return await annualAdministrativeExpensesService.GetHistory(rootId, cancellationToken);
        }

        [HttpPost]
        public async Task<AnnualAdministrativeExpensesDto> Add([FromBody] AnnualAdministrativeExpensesDto model, CancellationToken cancellationToken)
        {
            this.roleService.ValidateRolesException(UserRoleAliases.ADMINISTRATOR, UserRoleAliases.MOSV, UserRoleAliases.AUTHORIZED_ENTITY_ACTIVE);

            return await annualAdministrativeExpensesService.Add(model, cancellationToken);
        }

        [HttpPut]
        public async Task Update([FromBody] AnnualAdministrativeExpensesDto model, CancellationToken cancellationToken)
        {
            this.roleService.ValidateRolesException(UserRoleAliases.ADMINISTRATOR, UserRoleAliases.MOSV, UserRoleAliases.AUTHORIZED_ENTITY_ACTIVE);

            await annualAdministrativeExpensesService.Update(model, cancellationToken);
        }
    }
}
