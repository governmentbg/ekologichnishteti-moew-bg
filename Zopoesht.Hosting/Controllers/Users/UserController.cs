using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zopoesht.Application.Common.Dtos;
using Zopoesht.Application.Users.Dtos;
using Zopoesht.Application.Users.Interfaces;
using Zopoesht.Infrastructure.Interfaces.Users;

namespace Zopoesht.Hosting.Controllers.Users
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IUserContext userContext;


        public UserController(IUserService userService, IUserContext userContext)
        {
            this.userService = userService;
            this.userContext = userContext;
        }

        [HttpPost]
        public async Task<SearchResultItemDto<UserSearchResultDto>> GetFiltered([FromBody] UserSearchFilterDto model, CancellationToken cancellationToken)
            => await userService.GetFiltered(model, cancellationToken);

        [HttpGet("{id:int}")]
        public async Task<UserDto> GetById([FromRoute] int id, CancellationToken cancellationToken)
            => await userService.GetById(id, cancellationToken);

        [HttpPost("Add")]
        public async Task Add([FromBody] UserDto model, CancellationToken cancellationToken)
            => await userService.Add(model, cancellationToken);

        [HttpPut]
        public async Task Update([FromBody] UserDto model, CancellationToken cancellationToken)
            => await userService.Update(model, cancellationToken);

        [HttpPut("ChangePassword")]
        public async Task ChangePassword([FromBody] UserPasswordDto model, CancellationToken cancellationToken)
            => await userService.ChangePassword(model, cancellationToken);

        [HttpGet("UserContext")]
        public async Task<IUserContext> UserInfo()
        {
            return userContext;
        }
    }
}
