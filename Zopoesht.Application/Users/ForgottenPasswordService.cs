using Microsoft.EntityFrameworkCore;
using Zopoesht.Application.Users.Dtos;
using Zopoesht.Application.Users.Interfaces;
using Zopoesht.Data.Emails;
using Zopoesht.Data.Users.Enums;
using Zopoesht.Data.Users;
using Zopoesht.Infrastructure.DomainValidation;
using Zopoesht.Infrastructure.DomainValidation.Enums;
using Zopoesht.Infrastructure.Emails;
using Zopoesht.Infrastructure.Interfaces.Contexts;
using Zopoesht.Infrastructure.Users.Interfaces;
using Zopoesht.Infrastructure.AppSettings.AuthConfiguration;
using Zopoesht.Infrastructure.AppSettings;
using Zopoesht.Data.Common.Constants;

namespace Zopoesht.Application.Users
{
    public class ForgottenPasswordService : IForgottenPasswordService
    {
        private readonly IAppDbContext context;
        private readonly IEmailService emailService;
        private readonly IPasswordService passwordService;
        private readonly DomainValidationService validation;
        private readonly AuthConfigurationSettings authConfiguration;


        public ForgottenPasswordService(
            IAppDbContext context,
            IEmailService emailService,
            IPasswordService passwordService,
            DomainValidationService validation
            )
        {
            this.context = context;
            this.emailService = emailService;
            this.passwordService = passwordService;
            this.validation = validation;
            this.authConfiguration = AppSettingsConfiguration.AuthConfiguration;
        }

        public async Task SendMail(EmailForgottenPasswordDto model, CancellationToken cancellationToken)
        {
            var user = await this.context.Set<User>()
                    .AsNoTracking()
                    .SingleOrDefaultAsync(e => e.Email.Trim().ToLower() == model.Email.Trim().ToLower());

            if (user == null)
            {
                this.validation.ThrowErrorMessage(UserErrorCode.User_InvalidEmail);
            }

            if (user.Status != UserStatus.Active)
            {
                this.validation.ThrowErrorMessage(UserErrorCode.User_NotActive);
            }

            PasswordToken passwordToken = new PasswordToken(user.Id, 20160);
            this.context.Set<PasswordToken>().Add(passwordToken);

            var payload = new
            {
                Username = user.Username,
                ForgottenPasswordLink = $"{authConfiguration.Issuer}/passwordRecovery?token={passwordToken.Value}"
            };

            Email email = await this.emailService.ComposeEmailAsync(EmailTypeAlias.FORGOTTEN_PASSWORD, payload, user.Email);
            this.context.Set<Email>().Add(email);
            await this.context.SaveChangesAsync(cancellationToken);
        }
    }
}
