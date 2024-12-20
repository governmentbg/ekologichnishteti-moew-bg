using Microsoft.EntityFrameworkCore;
using Zopoesht.Data.Attributes;
using Zopoesht.Data.Common.Interfaces;
using Zopoesht.Data.Common.Models;
using Zopoesht.Data.Nomenclatures.Operators.Enums;
using Zopoesht.Data.Nomenclatures.Settlements;

namespace Zopoesht.Data.Nomenclatures.Operators
{
    public class Operator : Nomenclature, IIncludeAll<Operator>
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public OperatorType Type { get; set; }
        public string LegalEntityName { get; set; }
        public string LegalEntityUic { get; set; }
        public int? SettlementId { get; set; }
        [Skip]
        public Settlement Settlement { get; set; }
        public string ManagementAddress { get; set; }
        public string PostalCode { get; set; }
        public int? MigrationId { get; set; }
        public int OperatorCorrespondenceId { get; set; }
        public OperatorCorrespondence OperatorCorrespondence { get; set; }
        public ICollection<Terrain> Terrains { get; set; }
        public ICollection<ActivityOffering> ActivityOfferings { get; set; }

        public Operator()
        {
            Terrains = new List<Terrain>();
            ActivityOfferings = new List<ActivityOffering>();
        }

        public IQueryable<Operator> IncludeAll(IQueryable<Operator> query)
        {
            return query
                .Include(e => e.ActivityOfferings)
                    .ThenInclude(e => e.Activity)
                .Include(e => e.ActivityOfferings)
                    .ThenInclude(e => e.SubActivity)
                .Include(e => e.ActivityOfferings)
                    .ThenInclude(e => e.AuthorityRiosv)
                .Include(e => e.ActivityOfferings)
                    .ThenInclude(e => e.AuthorityBasin)
                .Include(e => e.ActivityOfferings)
                    .ThenInclude(e => e.Terrain)
                        .ThenInclude(e => e.Settlement)
                .Include(e => e.ActivityOfferings)
                    .ThenInclude(e => e.Terrain)
                        .ThenInclude(e => e.District)
                .Include(e => e.ActivityOfferings)
                    .ThenInclude(e => e.Terrain)
                        .ThenInclude(e => e.Municipality)
                .Include(e => e.Settlement)
                    .ThenInclude(e => e.District)
                .Include(e => e.Terrains)
                    .ThenInclude(e => e.Settlement)
                .Include(e => e.Terrains)
                    .ThenInclude(e => e.District)
                .Include(e => e.Terrains)
                    .ThenInclude(e => e.Municipality)
                .Include(e => e.OperatorCorrespondence)
                    .ThenInclude(e => e.Settlement)
                        .ThenInclude(e => e.District);
        }
    }
}
