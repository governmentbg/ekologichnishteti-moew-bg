using Zopoesht.Data.Nomenclatures;

namespace Zopoesht.Application.Nomenclatures.Dtos
{
    public class CodeSearchFilter : BaseNomenclatureFilterDto<Code>
    {
        public string Name { get; set; }

        public override IQueryable<Code> WhereBuilder(IQueryable<Code> query)
        {
            if (!string.IsNullOrWhiteSpace(Name))
            {
                query = query.Where(e => e.Name.ToLower().Contains(Name.ToLower()));
            }

            return base.WhereBuilder(query);
        }
    }
}
