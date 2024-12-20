namespace Zopoesht.Application.Common.Dtos
{
    public class FilterDto<TEntity>
         where TEntity : class
    {
        public string TextFilter { get; set; }
        public bool? IsActive { get; set; }

        public int? Limit { get; set; }
        public int? Offset { get; set; }

        public virtual IQueryable<TEntity> WhereBuilder(IQueryable<TEntity> query)
        {
            return query;
        }

        public virtual IQueryable<TEntity> OrderBuilder(IQueryable<TEntity> query)
        {
            return query;
        }
    }
}