using Zopoesht.Data.Nomenclatures.Settlements;

namespace Zopoesht.Application.Nomenclatures.Dtos.Settlements
{
    public class MunicipalityFilterDto : BaseNomenclatureFilterDto<Municipality>
    {
        public int? DistrictId { get; set; }

        public override IQueryable<Municipality> WhereBuilder(IQueryable<Municipality> query)
        {
            if (DistrictId.HasValue)
            {
                query = query.Where(e => e.DistrictId == DistrictId);
            }

            return base.WhereBuilder(query);
        }
    }
}
