namespace Zopoesht.Application.AdministrativeExpenses.Dtos
{
    public class AnnualAdministrativeExpensesHistorySearchResultDto
    {
        public string AuthorityName { get; set; }

        public string PeriodName { get; set; }

        public List<AnnualAdministrativeExpensesHistoryDto> Histories { get; set; }
    }
}
