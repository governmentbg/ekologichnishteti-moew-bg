using Microsoft.AspNetCore.Mvc;
using Zopoesht.Application.AdministrativeExpenses.Interfaces;
using Zopoesht.Application.AdministrativeExpenses.Dtos;
using Zopoesht.Application.Infrastructure.Services;
using Zopoesht.Data.Users.Constants;

namespace Zopoesht.Hosting.Controllers.AdministrativeExpenses
{
    [ApiController]
    [Route("api/[controller]")]
    public class PeriodController : ControllerBase
    {
        private readonly IPeriodService periodService;
        private readonly RoleService roleService;

        public PeriodController(IPeriodService periodService, RoleService roleService)
        {
            this.periodService = periodService;
            this.roleService = roleService;
        }

        [HttpGet]
        public async Task<List<PeriodDto>> GetAll(CancellationToken cancellationToken)
        {
            this.roleService.ValidateRolesException(UserRoleAliases.ADMINISTRATOR, UserRoleAliases.MOSV, UserRoleAliases.AUTHORIZED_ENTITY_ACTIVE);

            return await periodService.GetAll(cancellationToken);
        }

        [HttpPost]
        public async Task<int> Add([FromBody] PeriodDto model, CancellationToken cancellationToken)
        {
            this.roleService.ValidateRolesException(UserRoleAliases.ADMINISTRATOR, UserRoleAliases.MOSV);

            return await periodService.Add(model, cancellationToken);
        }

        [HttpPut]
        public async Task Update([FromBody] PeriodDto model, CancellationToken cancellationToken)
        {
            this.roleService.ValidateRolesException(UserRoleAliases.ADMINISTRATOR, UserRoleAliases.MOSV);

            await periodService.Update(model, cancellationToken);
        }
    }
}
