using Zopoesht.Data.Applications.ApplicationTwo.Enums;
using Zopoesht.Infrastructure.DomainValidation;
using Zopoesht.Infrastructure.DomainValidation.Enums;
using Zopoesht.Infrastructure.Interfaces;

namespace Zopoesht.Application.Applications.ApplicationsTwo.Dtos.FinancialAssurance
{
    public class MortgageDto : IValidate
    {
        public MortgageType MortgageType { get; set; }
        public string MortgageNumber { get; set; }
        public DateTime? MortgageDate { get; set; }
        public string MortgageDescription { get; set; }

        public decimal Value { get; set; }

        public string AdditionalInformation { get; set; }

        public void ValidateProperties(DomainValidationService validationService)
        {
            if (string.IsNullOrWhiteSpace(MortgageNumber))
            {
                validationService.ThrowErrorMessage(ValidationErrorCode.Validation_RequiredField);
            }

            if (!MortgageDate.HasValue)
            {
                validationService.ThrowErrorMessage(ValidationErrorCode.Validation_RequiredField);
            }

            if (string.IsNullOrWhiteSpace(MortgageDescription))
            {
                validationService.ThrowErrorMessage(ValidationErrorCode.Validation_RequiredField);
            }

            if (Value < 0)
            {
                validationService.ThrowErrorMessage(ValidationErrorCode.Validation_InvalidFormat);
            }
        }
    }
}
