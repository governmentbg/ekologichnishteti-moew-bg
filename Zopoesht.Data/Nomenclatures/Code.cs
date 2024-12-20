using Zopoesht.Data.Common.Interfaces;
using Zopoesht.Data.Common.Models;

namespace Zopoesht.Data.Nomenclatures
{
    public class Code : Nomenclature, IIncludeAll<Code>
    {
        public IQueryable<Code> IncludeAll(IQueryable<Code> query)
        {
            return query;
        }
    }
}
