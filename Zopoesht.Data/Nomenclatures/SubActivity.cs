using Zopoesht.Data.Attributes;
using Zopoesht.Data.Common.Interfaces;
using Zopoesht.Data.Common.Models;

namespace Zopoesht.Data.Nomenclatures
{
    public class SubActivity : NomenclatureCode, IIncludeAll<SubActivity>
    {
        public int ActivityId { get; set; }
        [Skip]
        public Activity Activity { get; set; }

        public IQueryable<SubActivity> IncludeAll(IQueryable<SubActivity> query)
        {
            return query;
        }
    }
}