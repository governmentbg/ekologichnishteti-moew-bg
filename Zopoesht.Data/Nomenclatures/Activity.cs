using Zopoesht.Data.Common.Interfaces;
using Zopoesht.Data.Common.Models;

namespace Zopoesht.Data.Nomenclatures
{
    public class Activity : NomenclatureCode, IIncludeAll<Activity>
    {
        public IQueryable<Activity> IncludeAll(IQueryable<Activity> query)
        {
            return query;
        }
    }
}
