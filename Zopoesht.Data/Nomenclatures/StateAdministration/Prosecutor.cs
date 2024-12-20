using Zopoesht.Data.Common.Interfaces;
using Zopoesht.Data.Common.Models;
using Zopoesht.Data.Nomenclatures.StateAdministration.Enums;

namespace Zopoesht.Data.Nomenclatures.StateAdministration
{
    public class Prosecutor : Nomenclature, IIncludeAll<Prosecutor>
    {
        public ProsecutorType ProsecutorType { get; set; }


        public IQueryable<Prosecutor> IncludeAll(IQueryable<Prosecutor> query)
        {
            return query;
        }
    }
}
