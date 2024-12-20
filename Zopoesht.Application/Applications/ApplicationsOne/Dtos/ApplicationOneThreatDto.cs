using Zopoesht.Infrastructure.DomainValidation;
using Zopoesht.Infrastructure.DomainValidation.Constants;
using Zopoesht.Infrastructure.DomainValidation.Enums;
using Zopoesht.Infrastructure.Interfaces;

namespace Zopoesht.Application.Applications.ApplicationsOne.Dtos
{
    public class ApplicationOneThreatDto : IValidate
    {
        public string PreventiveMeasuresTaken { get; set; }

        public string AnalyticProtocols { get; set; }

        public string MeasuresAdvice { get; set; }

        public string FinancialStatement { get; set; }

        public void ValidateProperties(DomainValidationService validationService)
        {
            if (string.IsNullOrWhiteSpace(PreventiveMeasuresTaken))
            {
                validationService.ThrowErrorMessage(ValidationErrorCode.Validation_RequiredField);
            }

            if (PreventiveMeasuresTaken.Length > ValidationConstants.TextMaxLength1024)
            {
                validationService.ThrowErrorMessage(ValidationErrorCode.Validation_ExcessiveText);
            }

            //if (!ValidatePropertiesHelperExtension.IsValidLettersAndNumbers(PreventiveMeasuresTaken))
            //{
            //    validationService.ThrowErrorMessage(ValidationErrorCode.Validation_InvalidFormat);
            //}

            if (string.IsNullOrWhiteSpace(AnalyticProtocols))
            {
                validationService.ThrowErrorMessage(ValidationErrorCode.Validation_RequiredField);
            }

            //if (!ValidatePropertiesHelperExtension.IsValidLettersAndNumbers(AnalyticProtocols))
            //{
            //    validationService.ThrowErrorMessage(ValidationErrorCode.Validation_InvalidFormat);
            //}

            if (string.IsNullOrWhiteSpace(MeasuresAdvice))
            {
                validationService.ThrowErrorMessage(ValidationErrorCode.Validation_RequiredField);
            }

            //if (!ValidatePropertiesHelperExtension.IsValidLettersAndNumbers(MeasuresAdvice))
            //{
            //    validationService.ThrowErrorMessage(ValidationErrorCode.Validation_InvalidFormat);
            //}

            if (MeasuresAdvice.Length > ValidationConstants.TextMaxLength1024)
            {
                validationService.ThrowErrorMessage(ValidationErrorCode.Validation_ExcessiveText);
            }

            if (string.IsNullOrWhiteSpace(FinancialStatement))
            {
                validationService.ThrowErrorMessage(ValidationErrorCode.Validation_RequiredField);
            }

            //if (!ValidatePropertiesHelperExtension.IsValidLettersAndNumbers(FinancialStatement))
            //{
            //    validationService.ThrowErrorMessage(ValidationErrorCode.Validation_InvalidFormat);
            //}
        }
    }
}
