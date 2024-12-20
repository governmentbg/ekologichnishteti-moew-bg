using Zopoesht.Infrastructure.DomainValidation;
using Zopoesht.Infrastructure.DomainValidation.Enums;
using Zopoesht.Infrastructure.Helpers.Extensions;
using Zopoesht.Infrastructure.Interfaces;

namespace Zopoesht.Application.Users.Dtos
{
    public class UserPasswordDto : IValidate
    {
        public int UserId { get; set; }

        public string CurrentPassword { get; set; }

        public string NewPassword { get; set; }

        public string NewPasswordConfirmation { get; set; }

        public void ValidateProperties(DomainValidationService validationService)
        {
            if (string.IsNullOrWhiteSpace(CurrentPassword))
            {
                validationService.ThrowErrorMessage(ValidationErrorCode.Validation_RequiredField);
            }

            if (!ValidatePropertiesHelperExtension.IsValidPassword(CurrentPassword))
            {
                validationService.ThrowErrorMessage(ValidationErrorCode.Validation_InvalidFormat);
            }

            if (string.IsNullOrWhiteSpace(NewPassword))
            {
                validationService.ThrowErrorMessage(ValidationErrorCode.Validation_RequiredField);
            }

            if (string.IsNullOrWhiteSpace(NewPasswordConfirmation))
            {
                validationService.ThrowErrorMessage(ValidationErrorCode.Validation_RequiredField);
            }

            if (NewPassword != NewPasswordConfirmation)
            {
                validationService.ThrowErrorMessage(UserErrorCode.User_MissmatchedPasswords);
            }

            if (!ValidatePropertiesHelperExtension.IsValidPassword(NewPassword))
            {
                validationService.ThrowErrorMessage(ValidationErrorCode.Validation_InvalidFormat);
            }
        }
    }
}
