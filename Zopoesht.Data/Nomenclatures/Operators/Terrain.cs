using Microsoft.EntityFrameworkCore;
using Zopoesht.Data.Attributes;
using Zopoesht.Data.Common.Interfaces;
using Zopoesht.Data.Common.Models;
using Zopoesht.Data.Nomenclatures.Settlements;

namespace Zopoesht.Data.Nomenclatures.Operators
{
    public class Terrain : Nomenclature, IIncludeAll<Terrain>
    {
        public int OperatorId { get; set; }
        [Skip]
        public Operator Operator { get; set; }

        public int? DistrictId { get; set; }
        [Skip]
        public District District { get; set; }
        public int? MunicipalityId { get; set; }
        [Skip]
        public Municipality Municipality { get; set; }
        public int? SettlementId { get; set; }
        [Skip]
        public Settlement Settlement { get; set; }
        public string Address { get; set; }

        public int? MigrationId { get; set; }

        public IQueryable<Terrain> IncludeAll(IQueryable<Terrain> query)
        {
            return query
                .Include(e => e.Operator)
                .Include(e => e.District)
                .Include(e => e.Municipality)
                .Include(e => e.Settlement);
        }
    }
}
