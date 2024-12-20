using System.Linq.Expressions;

namespace Zopoesht.Application.Common.Interfaces
{
    public interface IMapping<TFrom, TTo>
    {
        public abstract Expression<Func<TFrom, TTo>> Map();
    }
}