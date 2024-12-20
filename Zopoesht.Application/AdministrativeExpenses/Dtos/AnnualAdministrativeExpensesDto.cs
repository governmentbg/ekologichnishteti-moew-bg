using Zopoesht.Infrastructure.DomainValidation.Enums;
using Zopoesht.Infrastructure.DomainValidation;
using Zopoesht.Infrastructure.Interfaces;
using Zopoesht.Data.Nomenclatures;
using Zopoesht.Data.AdministrativeExpenses.Enums;

namespace Zopoesht.Application.AdministrativeExpenses.Dtos
{
    public class AnnualAdministrativeExpensesDto : IValidate
    {
        public int Id { get; set; }

        public int RootId { get; set; }

        public AnnualAdministrativeExpenseState State { get; set; }

        public int AuthorityId { get; set; }

        public Authority Authority { get; set; }

        public decimal Amount { get; set; }

        public int PeriodId { get; set; }

        public void ValidateProperties(DomainValidationService validationService)
        {
            if (AuthorityId <= 0)
            {
                validationService.ThrowErrorMessage(ValidationErrorCode.Validation_RequiredField);
            }

            if (Amount <= 0)
            {
                validationService.ThrowErrorMessage(ValidationErrorCode.Validation_RequiredField);
            }

            if (PeriodId <= 0)
            {
                validationService.ThrowErrorMessage(ValidationErrorCode.Validation_RequiredField);
            }
        }
    }
}
