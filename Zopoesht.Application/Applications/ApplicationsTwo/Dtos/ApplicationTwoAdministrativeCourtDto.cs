using Zopoesht.Application.Nomenclatures.Dtos;
using Zopoesht.Infrastructure.DomainValidation.Enums;
using Zopoesht.Infrastructure.DomainValidation;
using Zopoesht.Infrastructure.Helpers.Extensions;
using Zopoesht.Infrastructure.Interfaces;

namespace Zopoesht.Application.Applications.ApplicationsTwo.Dtos
{
    public class ApplicationTwoAdministrativeCourtDto : IValidate
    {
        public int AdministrativeCourtId { get; set; }

        public AdministrativeCourtDto AdministrativeCourt { get; set; }

        public void ValidateProperties(DomainValidationService validationService)
        {
            if (AdministrativeCourtId <= 0)
            {
                validationService.ThrowErrorMessage(ValidationErrorCode.Validation_InvalidFormat);
            }

            if (AdministrativeCourt == null)
            {
                validationService.ThrowErrorMessage(ValidationErrorCode.Validation_RequiredField);
            }

            if (string.IsNullOrWhiteSpace(AdministrativeCourt.Name))
            {
                validationService.ThrowErrorMessage(ValidationErrorCode.Validation_RequiredField);
            }

            if (!ValidatePropertiesHelperExtension.IsValidCyrillicName(AdministrativeCourt.Name))
            {
                validationService.ThrowErrorMessage(ValidationErrorCode.Validation_InvalidFormat);
            }
        }
    }
}
