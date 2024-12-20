using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zopoesht.Application.Users.Dtos;
using Zopoesht.Application.Users.Interfaces;

namespace Zopoesht.Hosting.Controllers.Users
{
    [ApiController]
    [AllowAnonymous]
    [Route("api/[controller]")]
    public class ForgottenPasswordController : ControllerBase
    {
        private readonly IForgottenPasswordService forgottenPasswordService;

        public ForgottenPasswordController(IForgottenPasswordService forgottenPasswordService)
        {
            this.forgottenPasswordService = forgottenPasswordService;
        }

        [HttpPost]
        public async Task SendForgottenPasswordMail([FromBody] EmailForgottenPasswordDto model, CancellationToken cancellationToken)
            => await this.forgottenPasswordService.SendMail(model, cancellationToken);
    }
}
