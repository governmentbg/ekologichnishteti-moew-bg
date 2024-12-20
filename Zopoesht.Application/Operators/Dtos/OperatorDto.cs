using Zopoesht.Data.Attributes;
using Zopoesht.Data.Nomenclatures.Settlements;
using Zopoesht.Infrastructure.Interfaces;
using Zopoesht.Infrastructure.DomainValidation;
using Zopoesht.Infrastructure.DomainValidation.Enums;
using Zopoesht.Infrastructure.Helpers.Dtos;
using Zopoesht.Data.Nomenclatures.Operators.Enums;
using Zopoesht.Infrastructure.Helpers.Extensions;

namespace Zopoesht.Application.Operators.Dtos
{
    public class OperatorDto: NomenclatureDto, IValidate
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Fullname { get; set; }
        public OperatorType Type { get; set; }
        public string LegalEntityName { get; set; }
        public string LegalEntityUic { get; set; }
        public int? SettlementId { get; set; }
        [Skip]
        public Settlement Settlement { get; set; }
        public string ManagementAddress { get; set; }
        public string PostalCode { get; set; }
        public int OperatorCorrespondenceId { get; set; }
        public OperatorCorrespondenceDto OperatorCorrespondence { get; set; }
        public List<TerrainDto> Terrains { get; set; }
        public List<ActivityOfferingDto> ActivityOfferings { get; set; }

        public void ValidateProperties(DomainValidationService validationService)
        {
            if (Type == OperatorType.Person)
            {
                if (string.IsNullOrWhiteSpace(FirstName))
                {
                    validationService.ThrowErrorMessage(ValidationErrorCode.Validation_RequiredField);
                }

                if (string.IsNullOrWhiteSpace(MiddleName))
                {
                    validationService.ThrowErrorMessage(ValidationErrorCode.Validation_RequiredField);
                }

                if (string.IsNullOrWhiteSpace(LastName))
                {
                    validationService.ThrowErrorMessage(ValidationErrorCode.Validation_RequiredField);
                }
            }
            else
            {
                if (string.IsNullOrWhiteSpace(LegalEntityName))
                {
                    validationService.ThrowErrorMessage(ValidationErrorCode.Validation_RequiredField);
                }

                if (string.IsNullOrWhiteSpace(LegalEntityUic))
                {
                    validationService.ThrowErrorMessage(ValidationErrorCode.Validation_RequiredField);
                }

                if (!ValidatePropertiesHelperExtension.IsValidNumber(LegalEntityUic))
                {
                    validationService.ThrowErrorMessage(ValidationErrorCode.Validation_InvalidFormat);
                }
            }

            if (!SettlementId.HasValue || SettlementId <= 0)
            {
                validationService.ThrowErrorMessage(ValidationErrorCode.Validation_RequiredField);
            }

            if (string.IsNullOrWhiteSpace(ManagementAddress))
            {
                validationService.ThrowErrorMessage(ValidationErrorCode.Validation_RequiredField);
            }

            if (!string.IsNullOrWhiteSpace(PostalCode) && !ValidatePropertiesHelperExtension.IsValidNumber(PostalCode))
            {
                validationService.ThrowErrorMessage(ValidationErrorCode.Validation_InvalidFormat);
            }

            if (Terrains != null && Terrains.Any())
            {
                Terrains.ForEach(t => t.ValidateProperties(validationService));
            }

            if (ActivityOfferings != null && ActivityOfferings.Any())
            {
                ActivityOfferings.ForEach(a => a.ValidateProperties(validationService));
            }
        }
    }
}
