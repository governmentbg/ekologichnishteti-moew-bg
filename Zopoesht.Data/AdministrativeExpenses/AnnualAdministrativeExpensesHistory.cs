using Zopoesht.Data.Common.Interfaces;
using Zopoesht.Data.Users;

namespace Zopoesht.Data.AdministrativeExpenses
{
    public class AnnualAdministrativeExpensesHistory : IEntity
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public string UserFullName { get; set; }

        public DateTime ModificationDate { get; set; }

        public int AnnualAdministrativeExpenseId { get; set; }

        public AnnualAdministrativeExpenses AnnualAdministrativeExpense { get; set; }

        public int RootId { get; set; }
    }
}
