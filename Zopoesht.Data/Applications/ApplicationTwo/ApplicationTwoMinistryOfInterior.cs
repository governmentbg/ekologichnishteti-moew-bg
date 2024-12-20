using Zopoesht.Data.Common.Interfaces;
using Zopoesht.Data.Nomenclatures.StateAdministration;

namespace Zopoesht.Data.Applications.ApplicationTwo
{
    public class ApplicationTwoMinistryOfInterior : IEntity
    {
        public int Id { get; set; }

        public int ApplicationTwoId { get; set; }
        public ApplicationTwo ApplicationTwo { get; set; }

        public int MinistryOfInteriorId { get; set; }
        public MinistryOfInterior MinistryOfInterior { get; set; }
    }
}
