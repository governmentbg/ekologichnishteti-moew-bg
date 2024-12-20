using Zopoesht.Data.Common.Interfaces;
using Zopoesht.Data.Nomenclatures;

namespace Zopoesht.Data.Applications.Common.Models
{
    public abstract class ApplicationAuthority : IEntity
    {
        public int Id { get; set; }
        public int AuthorityId { get; set; }
        public Authority Authority { get; set; }
    }
}
