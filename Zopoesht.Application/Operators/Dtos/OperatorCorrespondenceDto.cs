using Zopoesht.Data.Attributes;
using Zopoesht.Data.Nomenclatures.Settlements;
using Zopoesht.Infrastructure.DomainValidation;
using Zopoesht.Infrastructure.DomainValidation.Enums;
using Zopoesht.Infrastructure.Helpers.Extensions;
using Zopoesht.Infrastructure.Interfaces;

namespace Zopoesht.Application.Operators.Dtos
{
    public class OperatorCorrespondenceDto : IValidate
    {
        public int Id { get; set; }
        public int? SettlementId { get; set; }
        [Skip]
        public Settlement Settlement { get; set; }
        public string CorrespondenceAddress { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string ContactPerson { get; set; }
        public string PostalCode { get; set; }

        public void ValidateProperties(DomainValidationService validationService)
        {
            if (!SettlementId.HasValue || SettlementId <= 0)
            {
                validationService.ThrowErrorMessage(ValidationErrorCode.Validation_RequiredField);
            }

            if (string.IsNullOrWhiteSpace(CorrespondenceAddress))
            {
                validationService.ThrowErrorMessage(ValidationErrorCode.Validation_RequiredField);
            }

            if (string.IsNullOrWhiteSpace(Phone))
            {
                validationService.ThrowErrorMessage(ValidationErrorCode.Validation_RequiredField);
            }

            if (!ValidatePropertiesHelperExtension.IsValidPhoneNumber(Phone))
            {
                validationService.ThrowErrorMessage(ValidationErrorCode.Validation_InvalidFormat);
            }

            if (string.IsNullOrWhiteSpace(Email))
            {
                validationService.ThrowErrorMessage(ValidationErrorCode.Validation_RequiredField);
            }

            if (!ValidatePropertiesHelperExtension.IsValidEmail(Email))
            {
                validationService.ThrowErrorMessage(ValidationErrorCode.Validation_InvalidFormat);
            }

            if (string.IsNullOrWhiteSpace(ContactPerson))
            {
                validationService.ThrowErrorMessage(ValidationErrorCode.Validation_RequiredField);
            }

            if (!string.IsNullOrWhiteSpace(PostalCode) && !ValidatePropertiesHelperExtension.IsValidNumber(PostalCode))
            {
                validationService.ThrowErrorMessage(ValidationErrorCode.Validation_InvalidFormat);
            }
        }
    }
}
