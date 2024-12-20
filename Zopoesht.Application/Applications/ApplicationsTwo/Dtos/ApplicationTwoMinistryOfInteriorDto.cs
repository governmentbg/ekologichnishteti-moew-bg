using Zopoesht.Infrastructure.DomainValidation.Enums;
using Zopoesht.Infrastructure.DomainValidation;
using Zopoesht.Infrastructure.Helpers.Dtos;
using Zopoesht.Infrastructure.Helpers.Extensions;
using Zopoesht.Infrastructure.Interfaces;

namespace Zopoesht.Application.Applications.ApplicationsTwo.Dtos
{
    public class ApplicationTwoMinistryOfInteriorDto : IValidate
    {
        public int MinistryOfInteriorId { get; set; }
        public NomenclatureDto MinistryOfInterior { get; set; }

        public void ValidateProperties(DomainValidationService validationService)
        {
            if (MinistryOfInteriorId <= 0)
            {
                validationService.ThrowErrorMessage(ValidationErrorCode.Validation_InvalidFormat);
            }

            if (MinistryOfInterior == null)
            {
                validationService.ThrowErrorMessage(ValidationErrorCode.Validation_RequiredField);
            }

            if (string.IsNullOrWhiteSpace(MinistryOfInterior.Name))
            {
                validationService.ThrowErrorMessage(ValidationErrorCode.Validation_RequiredField);
            }

            if (!ValidatePropertiesHelperExtension.IsValidCyrillicName(MinistryOfInterior.Name))
            {
                validationService.ThrowErrorMessage(ValidationErrorCode.Validation_InvalidFormat);
            }
        }
    }
}
