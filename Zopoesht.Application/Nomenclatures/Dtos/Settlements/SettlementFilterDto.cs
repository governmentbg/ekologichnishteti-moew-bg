using Zopoesht.Data.Nomenclatures.Settlements;

namespace Zopoesht.Application.Nomenclatures.Dtos.Settlements
{
    public class SettlementFilterDto : BaseNomenclatureFilterDto<Settlement>
    {
        public int? DistrictId { get; set; }
        public int? MunicipalityId { get; set; }

        public override IQueryable<Settlement> WhereBuilder(IQueryable<Settlement> query)
        {

            if (DistrictId.HasValue)
            {
                query = query.Where(e => e.DistrictId == DistrictId);
            }

            if (MunicipalityId.HasValue)
            {
                query = query.Where(e => e.MunicipalityId == MunicipalityId);
            }

            return base.WhereBuilder(query);
        }
    }
}
