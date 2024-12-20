using Zopoesht.Data.Common.Interfaces;
using Zopoesht.Data.Common.Models;

namespace Zopoesht.Data.Nomenclatures
{
    public class Year : Nomenclature, IIncludeAll<Year>
    {
        public IQueryable<Year> IncludeAll(IQueryable<Year> query)
        {
            return query;
        }
    }
}
