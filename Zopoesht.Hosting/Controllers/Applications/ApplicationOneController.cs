using Microsoft.AspNetCore.Mvc;
using Zopoesht.Application.Applications.ApplicationsOne.Dtos;
using Zopoesht.Application.Applications.ApplicationsOne.Interfaces;
using Zopoesht.Application.Common.Dtos;
using Zopoesht.Application.Infrastructure.Services;
using Zopoesht.Application.Reports.Dtos;
using Zopoesht.Data.Applications.Common.Enums;
using Zopoesht.Data.Common.Enums;
using Zopoesht.Data.Users.Constants;
using Zopoesht.Hosting.Controllers.Common;

namespace Zopoesht.Hosting.Controllers.Applications
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationOneController : BaseApplicationController<ApplicationOneDto>
    {
        private readonly IApplicationOneService service;
        private readonly RoleService roleService;
        private readonly ApplicationType applicationType = ApplicationType.ApplicationOne;

        public ApplicationOneController(
            IApplicationOneService service,
            RoleService roleService

            )
            : base(service, ApplicationType.ApplicationOne, roleService)
        {
            this.service = service;
           this.roleService = roleService;
        }

        [HttpPost("Filter")]
        public async Task<SearchResultItemDto<ApplicationOneSearchResultDto>> GetFiltered([FromBody] ApplicationOneSearchFilterDto model, CancellationToken cancellationToken)
            => await service.GetFiltered(model, cancellationToken);

        [HttpDelete("{id:int}")]
        public async Task Delete([FromRoute] int id, CancellationToken cancellationToken)
        {
            this.roleService.ValidateRolesException(UserRoleAliases.ADMINISTRATOR, UserRoleAliases.MOSV);

            await service.DeleteApplication(id, cancellationToken);
        }

        [HttpPut]
        public async Task<ApplicationOneDto> Restore([FromBody] int id, CancellationToken cancellationToken)
        {
            this.roleService.ValidateRolesException(UserRoleAliases.ADMINISTRATOR, UserRoleAliases.MOSV);

            return await service.RestoreApplication(id, cancellationToken);
        }

        [HttpGet("getByState")]
        public async Task<List<ApplicationOneSearchResultDto>> GetByState([FromQuery] CommitState state)
        {
            if (state == CommitState.Deleted)
            {
                this.roleService.ValidateRolesException(UserRoleAliases.ADMINISTRATOR, UserRoleAliases.MOSV);
            }

            return await this.service.GetByState<ApplicationOneSearchResultDto>(state);
        }

        [HttpGet("GetId")]
        public async Task<int> GetIdByAppTwoId([FromQuery] int appTwoId, CancellationToken cancellationToken)
        {
            return await this.service.GetIdByAppTwoId(appTwoId, cancellationToken);
        }

        [HttpPost("Export")]
        public async Task<IActionResult> ExportExcelFile([FromBody] ReportSearchFilterDto filter, CancellationToken cancellationToken)
        {
            byte[] byteArray = await this.service.ExportExcelFile(filter, cancellationToken);

            return File(byteArray, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"Report_{DateTime.Now:yyyyMMdd}.xlsx");
        }

        [HttpPut("CancelModification")]
        public async Task CancelModification([FromBody] ApplicationOneDto dto)
        {
            this.roleService.ValidateRolesException(UserRoleAliases.ADMINISTRATOR, UserRoleAliases.MOSV, UserRoleAliases.AUTHORIZED_ENTITY_ACTIVE);

            await this.service.CancelModification(dto, ApplicationType.ApplicationOne);
        }
    }
}
