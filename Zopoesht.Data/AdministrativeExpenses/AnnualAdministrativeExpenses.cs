using Zopoesht.Data.AdministrativeExpenses.Enums;
using Zopoesht.Data.Common.Interfaces;
using Zopoesht.Data.Nomenclatures;

namespace Zopoesht.Data.AdministrativeExpenses
{
    public class AnnualAdministrativeExpenses : IEntity, IAuditable
    {
        public int Id { get; set; }

        public DateTime CreateDate { get; set; }

        public int CreatorUserId { get; set; }

        public int RootId { get; set; }

        public AnnualAdministrativeExpenseState State { get; set; }

        public int AuthorityId { get; set; }

        public Authority Authority { get; set; }

        public decimal Amount { get; set; }

        public int PeriodId { get; set; }

        public Period Period { get; set; }
    }
}
