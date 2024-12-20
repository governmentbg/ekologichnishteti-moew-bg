using Zopoesht.Data.Common.Interfaces;
using Zopoesht.Data.Nomenclatures.StateAdministration;

namespace Zopoesht.Data.Applications.ApplicationTwo
{
    public class ApplicationTwoProsecutor : IEntity
    {
        public int Id { get; set; }

        public int ApplicationTwoId { get; set; }
        public ApplicationTwo ApplicationTwo { get; set; }

        public int ProsecutorId { get; set; }
        public Prosecutor Prosecutor { get; set; }
    }
}
