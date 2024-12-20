using Zopoesht.Data.Common.Interfaces;
using Zopoesht.Data.Common.Models;

namespace Zopoesht.Data.Nomenclatures
{
    public class Country : Nomenclature, IIncludeAll<Country>
    {
        public string Code { get; set; }

        public IQueryable<Country> IncludeAll(IQueryable<Country> query)
        {
            return query;
        }
    }
}
