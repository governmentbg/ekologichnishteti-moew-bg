using Zopoesht.Data.Nomenclatures;

namespace Zopoesht.Application.Nomenclatures.Dtos
{
    public class PeriodFilterDto : BaseNomenclatureFilterDto<Period>
    {
        public override IQueryable<Period> WhereBuilder(IQueryable<Period> query)
        {
            return base.WhereBuilder(query);
        }
    }
}
