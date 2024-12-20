using Zopoesht.Infrastructure.DomainValidation;
using Zopoesht.Infrastructure.DomainValidation.Enums;
using Zopoesht.Infrastructure.Helpers.Extensions;
using Zopoesht.Infrastructure.Interfaces;

namespace Zopoesht.Application.Applications.ApplicationsOne.Dtos
{
    public class LegalEntityDto : IValidate
    {
        public string LegalEntityName { get; set; }

        public string UIN { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public void ValidateProperties(DomainValidationService validationService)
        {
            if (string.IsNullOrWhiteSpace(LegalEntityName))
            {
                validationService.ThrowErrorMessage(ValidationErrorCode.Validation_RequiredField);
            }

            if (!ValidatePropertiesHelperExtension.IsValidLatinAndCyrillicName(LegalEntityName))
            {
                validationService.ThrowErrorMessage(ValidationErrorCode.Validation_InvalidFormat);
            }

            if (string.IsNullOrWhiteSpace(UIN))
            {
                validationService.ThrowErrorMessage(ValidationErrorCode.Validation_RequiredField);
            }

            if (!ValidatePropertiesHelperExtension.IsValidNumber(UIN))
            {
                validationService.ThrowErrorMessage(ValidationErrorCode.Validation_InvalidFormat);
            }

            if (string.IsNullOrWhiteSpace(Email))
            {
                validationService.ThrowErrorMessage(ValidationErrorCode.Validation_RequiredField);
            }

            if (!ValidatePropertiesHelperExtension.IsValidEmail(Email))
            {
                validationService.ThrowErrorMessage(ValidationErrorCode.Validation_InvalidEmail);
            }

            if (!string.IsNullOrWhiteSpace(Phone) && !ValidatePropertiesHelperExtension.IsValidPhoneNumber(Phone))
            {
                validationService.ThrowErrorMessage(ValidationErrorCode.Validation_InvalidFormat);
            }
        }
    }
}
