using Zopoesht.Infrastructure.DomainValidation;
using Zopoesht.Infrastructure.DomainValidation.Enums;
using Zopoesht.Infrastructure.Interfaces;

namespace Zopoesht.Application.Applications.ApplicationsTwo.Dtos.FinancialAssurance
{
    public class BankGuaranteeDto : IValidate
    {
        public string BankName { get; set; }
        public string GuaranteeNumber { get; set; }
        public DateTime? GuaranteeDate { get; set; }

        public DateTime? GuaranteeStart { get; set; }
        public DateTime? GuaranteeEnd { get; set; }

        public decimal Value { get; set; }

        public string AdditionalInformation { get; set; }

        public void ValidateProperties(DomainValidationService validationService)
        {
            if (string.IsNullOrWhiteSpace(BankName))
            {
                validationService.ThrowErrorMessage(ValidationErrorCode.Validation_RequiredField);
            }

            if (string.IsNullOrWhiteSpace(GuaranteeNumber))
            {
                validationService.ThrowErrorMessage(ValidationErrorCode.Validation_RequiredField);
            }

            if (!GuaranteeDate.HasValue || !GuaranteeStart.HasValue || !GuaranteeEnd.HasValue)
            {
                validationService.ThrowErrorMessage(ValidationErrorCode.Validation_RequiredField);
            }

            if (GuaranteeStart > GuaranteeEnd)
            {
                validationService.ThrowErrorMessage(ValidationErrorCode.Validation_FromDateGreaterThanToDate);
            }

            if (Value < 0)
            {
                validationService.ThrowErrorMessage(ValidationErrorCode.Validation_InvalidFormat);
            }
        }
    }
}
