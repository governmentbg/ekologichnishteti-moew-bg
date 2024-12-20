using Zopoesht.Data.Common.Interfaces;
using Zopoesht.Data.Common.Models;
using Zopoesht.Data.Nomenclatures.StateAdministration.Enums;

namespace Zopoesht.Data.Nomenclatures.StateAdministration
{
    public class AdministrativeCourt : Nomenclature, IIncludeAll<AdministrativeCourt>
    {
        public AdministrativeCourtType AdministrativeCourtType { get; set; }


        public IQueryable<AdministrativeCourt> IncludeAll(IQueryable<AdministrativeCourt> query)
        {
            return query;
        }
    }
}
