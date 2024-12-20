using Zopoesht.Data.Common.Interfaces;

namespace Zopoesht.Data.Common.Interfaces
{
    public interface IIncludeAll<TEntity>
        where TEntity : IEntity, IConcurrency
    {
        IQueryable<TEntity> IncludeAll(IQueryable<TEntity> query);
    }
}
