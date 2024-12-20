using Zopoesht.Data.Nomenclatures;
using Zopoesht.Infrastructure.DomainValidation;
using Zopoesht.Infrastructure.DomainValidation.Enums;
using Zopoesht.Infrastructure.Helpers.Dtos;
using Zopoesht.Infrastructure.Interfaces;

namespace Zopoesht.Application.AdministrativeExpenses.Dtos
{
    public class PeriodDto : NomenclatureDto, IValidate
    {
        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool HasAnnualAdministrativeExpenses { get; set; }

        public List<AnnualAdministrativeExpensesDto> AnnualAdministrativeExpenses { get; set; } = new List<AnnualAdministrativeExpensesDto>();

        public void ValidateProperties(DomainValidationService validationService)
        {
            if (!StartDate.HasValue)
            {
                validationService.ThrowErrorMessage(ValidationErrorCode.Validation_RequiredField);
            }

            if (!EndDate.HasValue)
            {
                validationService.ThrowErrorMessage(ValidationErrorCode.Validation_RequiredField);
            }

            if (StartDate > EndDate)
            {
                validationService.ThrowErrorMessage(ValidationErrorCode.Validation_FromDateGreaterThanToDate);
            }
        }
    }
}
