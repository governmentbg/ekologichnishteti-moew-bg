using Zopoesht.Data.Common.Interfaces;

namespace Zopoesht.Data.Applications.ApplicationOne
{
    public class LegalEntity : IEntity
    {
        public int Id { get; set; }
        public string LegalEntityName { get; set; }
        public string UIN { get; set; }

        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
