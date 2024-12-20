using Zopoesht.Data.Applications.ApplicationOne.Enums;
using Zopoesht.Infrastructure.DomainValidation;
using Zopoesht.Infrastructure.DomainValidation.Constants;
using Zopoesht.Infrastructure.DomainValidation.Enums;
using Zopoesht.Infrastructure.Helpers.Extensions;
using Zopoesht.Infrastructure.Interfaces;

namespace Zopoesht.Application.Applications.ApplicationsOne.Dtos
{
    public class ApplicationOneLegalEntityDto : IValidate
    {
        public string Address { get; set; }

        public string ApplicantViolations { get; set; }

        public string RecoveryAdvice { get; set; }

        public string AllegedOccurenceConsequences { get; set; }

        public void ValidateProperties(DomainValidationService validationService)
        {
            if (string.IsNullOrWhiteSpace(Address))
            {
                validationService.ThrowErrorMessage(ValidationErrorCode.Validation_RequiredField);
            }

            //if (!ValidatePropertiesHelperExtension.IsValidLettersAndNumbers(Address))
            //{
            //    validationService.ThrowErrorMessage(ValidationErrorCode.Validation_InvalidFormat);
            //}

            if (string.IsNullOrWhiteSpace(RecoveryAdvice))
            {
                validationService.ThrowErrorMessage(ValidationErrorCode.Validation_RequiredField);
            }

            //if (!ValidatePropertiesHelperExtension.IsValidLettersAndNumbers(RecoveryAdvice))
            //{
            //    validationService.ThrowErrorMessage(ValidationErrorCode.Validation_InvalidFormat);
            //}

            if (RecoveryAdvice.Length > ValidationConstants.TextMaxLength1024)
            {
                validationService.ThrowErrorMessage(ValidationErrorCode.Validation_ExcessiveText);
            }

            if (string.IsNullOrWhiteSpace(AllegedOccurenceConsequences))
            {
                validationService.ThrowErrorMessage(ValidationErrorCode.Validation_RequiredField);
            }

            //if (!ValidatePropertiesHelperExtension.IsValidLettersAndNumbers(AllegedOccurenceConsequences))
            //{
            //    validationService.ThrowErrorMessage(ValidationErrorCode.Validation_InvalidFormat);
            //}

            if (AllegedOccurenceConsequences.Length > ValidationConstants.TextMaxLength1024)
            {
                validationService.ThrowErrorMessage(ValidationErrorCode.Validation_ExcessiveText);
            }
        }

        public void ValidateApplicantViolations(ApplicantType applicantType, DomainValidationService validationService)
        {
            if (applicantType == ApplicantType.LegalEntity)
            {
                if (string.IsNullOrWhiteSpace(ApplicantViolations))
                {
                    validationService.ThrowErrorMessage(ValidationErrorCode.Validation_RequiredField);
                }

                if (!ValidatePropertiesHelperExtension.IsValidLettersAndNumbers(ApplicantViolations))
                {
                    validationService.ThrowErrorMessage(ValidationErrorCode.Validation_InvalidFormat);
                }
            }
        }
    }
}
