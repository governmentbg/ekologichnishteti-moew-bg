using Zopoesht.Data.Common.Interfaces;
using Zopoesht.Data.Common.Models;
using Zopoesht.Data.Nomenclatures.Operators;

namespace Zopoesht.Data.Nomenclatures
{
    public class Authority : Nomenclature, IIncludeAll<Authority>
    {
        public AuthorityType AuthorityType { get; set; }

        public int? MigrationId { get; set; }

        public IQueryable<Authority> IncludeAll(IQueryable<Authority> query)
        {
            return query;
        }
    }
}
