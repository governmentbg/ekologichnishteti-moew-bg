using Microsoft.AspNetCore.Mvc;
using Zopoesht.Application.Applications.Common.Dtos;
using Zopoesht.Application.Applications.Common.Interfaces;
using Zopoesht.Application.Infrastructure.Services;
using Zopoesht.Data.Applications.Common.Enums;
using Zopoesht.Data.Common.Enums;
using Zopoesht.Data.Users.Constants;

namespace Zopoesht.Hosting.Controllers.Common
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseApplicationController<TDto> : ControllerBase
         where TDto : ApplicationCommitDto
    {
        private readonly IApplicationService<TDto> service;
        private readonly RoleService roleService;
        private readonly ApplicationType type;

        public BaseApplicationController(IApplicationService<TDto> service, ApplicationType type, RoleService roleService)
        {
            this.service = service;
            this.roleService = roleService;
            this.type = type;
        }

        [HttpGet("{id:int}")]
        public async Task<TDto> GetById([FromRoute] int id)
        {
            return await this.service.GetById(id);
        }

        [HttpGet("history/{rootId:int}")]
        public async Task<ApplicationHistoryResultDto> GetHistory([FromRoute] int rootId)
        {
            this.roleService.ValidateRolesException(UserRoleAliases.ADMINISTRATOR, UserRoleAliases.MOSV);

            return await this.service.GetHistory(rootId, this.type);
        }

        [HttpPost]
        public async Task Create([FromBody] TDto dto)
        {
            this.roleService.ValidateRolesException(UserRoleAliases.ADMINISTRATOR, UserRoleAliases.MOSV, UserRoleAliases.AUTHORIZED_ENTITY_ACTIVE);

            await this.service.Create(dto);
        }

        [HttpPost("saveModification")]
        public async Task<TDto> SaveModification([FromBody] TDto dto)
        {
            this.roleService.ValidateRolesException(UserRoleAliases.ADMINISTRATOR, UserRoleAliases.MOSV, UserRoleAliases.AUTHORIZED_ENTITY_ACTIVE);

            return await this.service.SaveModification(dto);
        }

        [HttpPost("beginModification")]
        public async Task<TDto> BeginModification([FromBody] TDto dto, [FromQuery] CommitState state)
        {
            this.roleService.ValidateRolesException(UserRoleAliases.ADMINISTRATOR, UserRoleAliases.MOSV, UserRoleAliases.AUTHORIZED_ENTITY_ACTIVE);

            return await this.service.BeginModification(dto, state, this.type);
        }

        [HttpPost("finishmodification")]
        public virtual async Task<TDto> FinishModification([FromBody] TDto dto, [FromQuery] CommitState state)
        {
            this.roleService.ValidateRolesException(UserRoleAliases.ADMINISTRATOR, UserRoleAliases.MOSV, UserRoleAliases.AUTHORIZED_ENTITY_ACTIVE);

            return await this.service.FinishModification(dto, state, this.type);
        }

        [HttpPost("{applicationId:int}/complete")]
        public virtual async Task<TDto> Complete([FromRoute] int applicationId, [FromBody] ChangeApplicationStateDto changeStateDto)
        {
            this.roleService.ValidateRolesException(UserRoleAliases.ADMINISTRATOR, UserRoleAliases.MOSV, UserRoleAliases.AUTHORIZED_ENTITY_ACTIVE);

            await this.service.ChangeApplicationStateAndCreateHistory(applicationId, CommitState.Completed, this.type);

            return await this.service.GetById(applicationId);
        }

        [HttpPost("{applicationId:int}/enter")]
        public virtual async Task<TDto> Enter([FromRoute] int applicationId, [FromBody] ChangeApplicationStateDto changeStateDto)
        {
            this.roleService.ValidateRolesException(UserRoleAliases.ADMINISTRATOR, UserRoleAliases.MOSV, UserRoleAliases.AUTHORIZED_ENTITY_ACTIVE);

            await this.service.ChangeApplicationStateAndCreateHistory(applicationId, CommitState.Entered, this.type);

            return await this.service.GetById(applicationId);
        }
    }
}
