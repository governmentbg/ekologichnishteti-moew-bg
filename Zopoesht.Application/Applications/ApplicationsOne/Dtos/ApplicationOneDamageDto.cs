using Zopoesht.Infrastructure.DomainValidation;
using Zopoesht.Infrastructure.DomainValidation.Constants;
using Zopoesht.Infrastructure.DomainValidation.Enums;
using Zopoesht.Infrastructure.Interfaces;

namespace Zopoesht.Application.Applications.ApplicationsOne.Dtos
{
    public class ApplicationOneDamageDto : IValidate
    {
        public string OccurenceConsequences { get; set; }

        public string MeasuresTaken { get; set; }

        public void ValidateProperties(DomainValidationService validationService)
        {
            if (string.IsNullOrWhiteSpace(OccurenceConsequences))
            {
                validationService.ThrowErrorMessage(ValidationErrorCode.Validation_RequiredField);
            }

            //if (!ValidatePropertiesHelperExtension.IsValidLettersAndNumbers(OccurenceConsequences))
            //{
            //    validationService.ThrowErrorMessage(ValidationErrorCode.Validation_InvalidFormat);
            //}

            if (OccurenceConsequences.Length > ValidationConstants.TextMaxLength1024)
            {
                validationService.ThrowErrorMessage(ValidationErrorCode.Validation_ExcessiveText);
            }

            if (string.IsNullOrWhiteSpace(MeasuresTaken))
            {
                validationService.ThrowErrorMessage(ValidationErrorCode.Validation_RequiredField);
            }

            //if (!ValidatePropertiesHelperExtension.IsValidLettersAndNumbers(MeasuresTaken))
            //{
            //    validationService.ThrowErrorMessage(ValidationErrorCode.Validation_InvalidFormat);
            //}

            if (MeasuresTaken.Length > ValidationConstants.TextMaxLength1024)
            {
                validationService.ThrowErrorMessage(ValidationErrorCode.Validation_ExcessiveText);
            }
        }
    }
}
