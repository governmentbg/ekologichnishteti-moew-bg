using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zopoesht.Application.Users.Dtos;
using Zopoesht.Application.Users.Interfaces;

namespace Zopoesht.Hosting.Controllers.Users
{
    [ApiController]
    [AllowAnonymous]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ILoginService loginService;
        public AuthController(ILoginService loginService)
        {
            this.loginService = loginService;
        }

        [HttpPost]
        public async Task<UserLoginInfoDto> LoginUser([FromBody] UserCredentialsDto model, CancellationToken cancellationToken)
            => await loginService.Login(model, cancellationToken);
    }
}