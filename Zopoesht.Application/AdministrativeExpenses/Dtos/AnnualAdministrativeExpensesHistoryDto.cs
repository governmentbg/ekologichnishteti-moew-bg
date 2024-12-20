using Zopoesht.Application.Nomenclatures.Dtos;

namespace Zopoesht.Application.AdministrativeExpenses.Dtos
{
    public class AnnualAdministrativeExpensesHistoryDto
    {
        public int UserId { get; set; }

        public AuthorityDto UserAuthority { get; set; }

        public string UserFullName { get; set; }

        public DateTime ModificationDate { get; set; }

        public decimal AnnualAdministrativeExpenseAmount { get; set; }
    }
}
