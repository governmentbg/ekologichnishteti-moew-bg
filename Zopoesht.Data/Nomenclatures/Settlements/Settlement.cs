using Microsoft.EntityFrameworkCore;
using Zopoesht.Data.Attributes;
using Zopoesht.Data.Common.Interfaces;
using Zopoesht.Data.Common.Models;

namespace Zopoesht.Data.Nomenclatures.Settlements
{
    public class Settlement: NomenclatureCode, IIncludeAll<Settlement>
    {
        public int MunicipalityId { get; set; }
        [Skip]
        public Municipality Municipality { get; set; }
        public int DistrictId { get; set; }
        [Skip]
        public District District { get; set; }
        public string MunicipalityCode { get; set; }
        public string DistrictCode { get; set; }
        public string MunicipalityCode2 { get; set; }
        public string DistrictCode2 { get; set; }
        public string TypeName { get; set; }
        public string SettlementName { get; set; }
        public string TypeCode { get; set; }
        public string MayoraltyCode { get; set; }
        public string Category { get; set; }
        public string Altitude { get; set; }
        public bool IsDistrict { get; set; }

        public IQueryable<Settlement> IncludeAll(IQueryable<Settlement> query)
        {
            return query
                .Include(e => e.Municipality)
                .Include(e => e.District);
        }
    }
}
