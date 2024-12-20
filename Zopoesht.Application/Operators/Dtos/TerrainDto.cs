using OfficeOpenXml.FormulaParsing.Excel.Operators;
using Zopoesht.Data.Nomenclatures.Settlements;
using Zopoesht.Infrastructure.DomainValidation;
using Zopoesht.Infrastructure.DomainValidation.Enums;
using Zopoesht.Infrastructure.Interfaces;

namespace Zopoesht.Application.Operators.Dtos
{
    public class TerrainDto : IValidate
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int OperatorId { get; set; }
        public Operator Operator { get; set; }

        public int? DistrictId { get; set; }
        public District District { get; set; }

        public int? MunicipalityId { get; set; }
        public Municipality Municipality { get; set; }
        public int? SettlementId { get; set; }

        public Settlement Settlement { get; set; }
        public string Address { get; set; }

        public void ValidateProperties(DomainValidationService validationService)
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                validationService.ThrowErrorMessage(ValidationErrorCode.Validation_RequiredField);
            }

            if (DistrictId == null || District == null)
            {
                validationService.ThrowErrorMessage(ValidationErrorCode.Validation_RequiredField);
            }

            if (MunicipalityId == null || Municipality == null)
            {
                validationService.ThrowErrorMessage(ValidationErrorCode.Validation_RequiredField);
            }

            if (SettlementId == null || Settlement == null)
            {
                validationService.ThrowErrorMessage(ValidationErrorCode.Validation_RequiredField);
            }

            if (string.IsNullOrWhiteSpace(Address))
            {
                validationService.ThrowErrorMessage(ValidationErrorCode.Validation_RequiredField);
            }
        }
    }
}
