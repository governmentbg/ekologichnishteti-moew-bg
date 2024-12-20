using Zopoesht.Data.Nomenclatures.Operators;

namespace Zopoesht.Application.Nomenclatures.Dtos
{
    public class TerrainFilterDto : BaseNomenclatureFilterDto<Terrain>
    {
        public List<int> Ids { get; set; } = new List<int>();

        public override IQueryable<Terrain> WhereBuilder(IQueryable<Terrain> query)
        {
            if (Ids.Count == 1)
            {
                var operatorId = Ids[0];

                if (operatorId > 0)
                {
                    query = query.Where(e => e.OperatorId == operatorId);
                }
            }

            return base.WhereBuilder(query);
        }
    }
}
