using Microsoft.Identity.Client;
using Zopoesht.Data.Nomenclatures;

namespace Zopoesht.Application.Nomenclatures.Dtos
{
    public class ActivityFilterDto : BaseNomenclatureFilterDto<Activity>
    {
        public List<int> Ids { get; set; } = new List<int>();

        public bool OnlyAdded { get; set; }

        public override IQueryable<Activity> WhereBuilder(IQueryable<Activity> query)
        {
            if (OnlyAdded)
            {
                query = query.Where(e => Ids.Contains(e.Id));
            }

            return base.WhereBuilder(query);
        }
    }
}
