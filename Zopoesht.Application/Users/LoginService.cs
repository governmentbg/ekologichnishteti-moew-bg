using Zopoesht.Application.Users.Dtos;
using Zopoesht.Application.Users.Interfaces;
using Zopoesht.Infrastructure.DomainValidation;
using Zopoesht.Infrastructure.Interfaces.Contexts;
using Zopoesht.Infrastructure.Users.Interfaces;
using Zopoesht.Data.Users;
using Zopoesht.Data.Users.Enums;
using Microsoft.EntityFrameworkCore;
using Zopoesht.Infrastructure.DomainValidation.Enums;
using Zopoesht.Infrastructure.Helpers.Extensions;

namespace Zopoesht.Application.Users
{
    public class LoginService : ILoginService
    {
        private readonly IAppDbContext context;
        private readonly IPasswordService passwordService;
        private readonly IJWTService jwtService;
        private readonly DomainValidationService validation;

        public LoginService(
            IAppDbContext context,
            IPasswordService passwordService,
            IJWTService jwtService,
            DomainValidationService validation
            )
        {
            this.context = context;
            this.passwordService = passwordService;
            this.jwtService = jwtService;
            this.validation = validation;
        }

        public async Task<UserLoginInfoDto> Login(UserCredentialsDto model, CancellationToken cancellationToken)
        {
            var user = await this.context.Set<User>()
                    .AsNoTracking()
                    .Include(u => u.Role)
                    .SingleOrDefaultAsync(u => u.Username.Trim() == model.Username.Trim(), cancellationToken);

            if (user == null)
            {
                this.validation.ThrowErrorMessage(UserErrorCode.User_InvalidCredentials);
            }

            if (user.Status == UserStatus.Inactive)
            {
                this.validation.ThrowErrorMessage(UserErrorCode.User_IsInactive);
            }

            if (user.Status == UserStatus.Deactivated)
            {
                this.validation.ThrowErrorMessage(UserErrorCode.User_IsDeactivated);
            }

            bool isSamePassword = this.passwordService.VerifyHashedPassword(user.Password, model.Password, user.PasswordSalt);
            if (!isSamePassword)
            {
                this.validation.ThrowErrorMessage(UserErrorCode.User_InvalidCredentials);
            }

            var result = new UserLoginInfoDto
            {
                Fullname = user.FirstName + " " + user.LastName,
                RoleAlias = user.Role.Alias,
                Token = this.jwtService.CreateToken(user.Id, user.Role.Alias, user.Username, user.AuthorityId),
                Authority = user.Authority.ToNomenclatureDto()
            };

            return result;
        }
    }
}
