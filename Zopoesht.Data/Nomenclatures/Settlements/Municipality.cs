using Microsoft.EntityFrameworkCore;
using Zopoesht.Data.Attributes;
using Zopoesht.Data.Common.Interfaces;
using Zopoesht.Data.Common.Models;

namespace Zopoesht.Data.Nomenclatures.Settlements
{
    public class Municipality : NomenclatureCode, IIncludeAll<Municipality>
    {
        public int DistrictId { get; set; }
        [Skip]
        public District District { get; set; }
        public string Code2 { get; set; }
        public string MainSettlementCode { get; set; }
        public string Category { get; set; }

        public IQueryable<Municipality> IncludeAll(IQueryable<Municipality> query)
        {
            return query
                .Include(e => e.District);
        }
    }
}
