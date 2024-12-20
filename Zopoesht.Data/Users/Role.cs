using Zopoesht.Data.Common.Interfaces;
using Zopoesht.Data.Common.Models;

namespace Zopoesht.Data.Users
{
    public class Role : Nomenclature, IIncludeAll<Role>
    {
        public IQueryable<Role> IncludeAll(IQueryable<Role> query)
        {
            return query;
        }
    }
}