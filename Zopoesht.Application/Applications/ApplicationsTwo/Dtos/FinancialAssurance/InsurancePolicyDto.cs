using Zopoesht.Infrastructure.DomainValidation;
using Zopoesht.Infrastructure.DomainValidation.Enums;
using Zopoesht.Infrastructure.Interfaces;

namespace Zopoesht.Application.Applications.ApplicationsTwo.Dtos.FinancialAssurance
{
    public class InsurancePolicyDto : IValidate
    {
        public string CompanyName { get; set; }
        public string PolicyNumber { get; set; }
        public DateTime? PolicyDate { get; set; }

        public DateTime? InsuranceStart { get; set; }
        public DateTime? InsuranceEnd { get; set; }

        public decimal ResponsibilityLimit { get; set; }

        public string AdditionalInformation { get; set; }


        public void ValidateProperties(DomainValidationService validationService)
        {
            if (string.IsNullOrWhiteSpace(CompanyName)) 
            {
                validationService.ThrowErrorMessage(ValidationErrorCode.Validation_RequiredField);
            }

            if (string.IsNullOrWhiteSpace(PolicyNumber))
            {
                validationService.ThrowErrorMessage(ValidationErrorCode.Validation_RequiredField);
            }

            if (!PolicyDate.HasValue || !InsuranceStart.HasValue || !InsuranceEnd.HasValue)
            {
                validationService.ThrowErrorMessage(ValidationErrorCode.Validation_RequiredField);
            }

            if (InsuranceStart > InsuranceEnd) 
            {
                validationService.ThrowErrorMessage(ValidationErrorCode.Validation_FromDateGreaterThanToDate);
            }

            if (ResponsibilityLimit < 0)
            {
                validationService.ThrowErrorMessage(ValidationErrorCode.Validation_InvalidFormat);
            }
        }
    }
}
