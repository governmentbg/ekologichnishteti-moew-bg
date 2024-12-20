using Microsoft.AspNetCore.Mvc;
using Zopoesht.Application.Applications.ApplicationsTwo.Dtos;
using Zopoesht.Application.Applications.ApplicationsTwo.Interfaces;
using Zopoesht.Application.Infrastructure.Services;
using Zopoesht.Data.Applications.Common.Enums;
using Zopoesht.Data.Users.Constants;
using Zopoesht.Hosting.Controllers.Common;

namespace Zopoesht.Hosting.Controllers.Applications
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationTwoController : BaseApplicationController<ApplicationTwoDto>
    {
        private readonly IApplicationTwoService service;
        private readonly RoleService roleService;
        private readonly ApplicationType applicationType = ApplicationType.ApplicationTwo;

        public ApplicationTwoController(
            IApplicationTwoService service,
            RoleService roleService

            )
            : base(service, ApplicationType.ApplicationTwo, roleService)
        {
            this.service = service;
            this.roleService = roleService;
        }

        [HttpPut("CancelModification")]
        public async Task CancelModification([FromBody] ApplicationTwoDto dto)
        {
            this.roleService.ValidateRolesException(UserRoleAliases.ADMINISTRATOR, UserRoleAliases.MOSV, UserRoleAliases.AUTHORIZED_ENTITY_ACTIVE);

            await this.service.CancelModification(dto, ApplicationType.ApplicationTwo);
        }
    }
}
