using Zopoesht.Application.Common.Dtos;
using Zopoesht.Data.Common.Models;

namespace Zopoesht.Application.Nomenclatures.Dtos
{
    public class BaseNomenclatureFilterDto<TNomenclature> : FilterDto<TNomenclature>
            where TNomenclature : Nomenclature
    {
        public bool ExcludeAlreadyAdded { get; set; }

        public override IQueryable<TNomenclature> WhereBuilder(IQueryable<TNomenclature> query)
        {
            if (IsActive.HasValue)
            {
                if (IsActive == true)
                {
                    query = query.Where(e => e.IsActive);
                }
                else
                {
                    query = query.Where(e => !e.IsActive);
                }
            }

            query = ConstructTextFilter(query);

            return query;
        }

        public override IQueryable<TNomenclature> OrderBuilder(IQueryable<TNomenclature> query)
        {
            return query
                .OrderBy(e => e.ViewOrder)
                .ThenBy(e => e.Id)
                .ThenBy(e => e.Name);
        }

        public virtual IQueryable<TNomenclature> RemoveAlreadyAdded(IQueryable<TNomenclature> query, int[] alreadyAddedIds)
        {
            return query
                .Where(e => !alreadyAddedIds.Contains(e.Id));
        }

        protected virtual IQueryable<TNomenclature> ConstructTextFilter(IQueryable<TNomenclature> query)
        {
            if (!string.IsNullOrWhiteSpace(TextFilter))
            {
                var textFilter = $"{TextFilter.Trim().ToLower()}";
                query = query.Where(e => e.Name.Trim().ToLower().Contains(textFilter));
            }

            return query;
        }
    }
}