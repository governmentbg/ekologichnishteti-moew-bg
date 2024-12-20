using Zopoesht.Data.Common.Interfaces;
using Zopoesht.Data.Common.Models;

namespace Zopoesht.Data.Nomenclatures.Settlements
{
    public class District : NomenclatureCode, IIncludeAll<District>
    {
        public string Code2  { get; set; }
        public string SecondLevelRegionCode { get; set; }
        public string MainSettlementCode { get; set; }

        public IQueryable<District> IncludeAll(IQueryable<District> query)
        {
            return query;
        }
    }
}
