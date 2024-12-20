namespace Zopoesht.Infrastructure.DomainValidation.Enums
{
    public enum AdministrativeExpensesErrorCode
    {
        AdministrativeExpenses_PeriodDoesNotExist = 801,
        AdministrativeExpenses_PeriodCannotUpdate = 802,
        AdministrativeExpenses_AnnualAdministrativeExpenseDoesNotExist = 803,
        AdministrativeExpenses_NotEnoughPermissions = 804,
        AdministrativeExpenses_AlreadyExistingExpenseWithAuthority = 805
    }
}
