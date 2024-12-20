using Microsoft.EntityFrameworkCore;
using Zopoesht.Data.AdministrativeExpenses;
using Zopoesht.Data.Common.Interfaces;
using Zopoesht.Data.Common.Models;

namespace Zopoesht.Data.Nomenclatures
{
    public class Period : Nomenclature, IIncludeAll<Period>
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public List<AnnualAdministrativeExpenses> AnnualAdministrativeExpenses { get; set; } = new List<AnnualAdministrativeExpenses>();

        public IQueryable<Period> IncludeAll(IQueryable<Period> query)
        {
            return query
                .Include(e => e.AnnualAdministrativeExpenses);
        }
    }
}
