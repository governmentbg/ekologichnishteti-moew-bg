using Zopoesht.Data.Nomenclatures;

namespace Zopoesht.Application.Nomenclatures.Dtos
{
    public class SubActivitySearchFilterDto : BaseNomenclatureFilterDto<SubActivity>
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public int? ActivityId { get; set; }

        public override IQueryable<SubActivity> WhereBuilder(IQueryable<SubActivity> query)
        {
            if (!string.IsNullOrWhiteSpace(Code))
            {
                query = query.Where(e => e.Code.ToLower().Contains(Code.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(Name))
            {
                query = query.Where(e => e.Name.ToLower().Contains(Name.ToLower()));
            }

            if (ActivityId.HasValue)
            {
                query = query.Where(e => e.ActivityId == ActivityId);
            }

            return base.WhereBuilder(query);
        }
    }
}
