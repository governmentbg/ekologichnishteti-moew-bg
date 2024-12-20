using Zopoesht.Data.Common.Interfaces;
using Zopoesht.Data.Nomenclatures.StateAdministration;

namespace Zopoesht.Data.Applications.ApplicationTwo
{
    public class ApplicationTwoAdministrativeCourt : IEntity
    {
        public int Id { get; set; }

        public int ApplicationTwoId { get; set; }
        public ApplicationTwo ApplicationTwo { get; set; }

        public int AdministrativeCourtId { get; set; }
        public AdministrativeCourt AdministrativeCourt { get; set; }
    }
}