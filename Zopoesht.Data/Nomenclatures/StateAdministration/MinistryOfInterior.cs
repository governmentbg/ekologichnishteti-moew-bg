using Zopoesht.Data.Common.Interfaces;
using Zopoesht.Data.Common.Models;

namespace Zopoesht.Data.Nomenclatures.StateAdministration
{
    public class MinistryOfInterior : Nomenclature, IIncludeAll<MinistryOfInterior>
    {
        public IQueryable<MinistryOfInterior> IncludeAll(IQueryable<MinistryOfInterior> query)
        {
            return query;
        }
    }
}
