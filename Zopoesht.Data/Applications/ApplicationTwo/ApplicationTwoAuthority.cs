using Zopoesht.Data.Applications.Common.Models;

namespace Zopoesht.Data.Applications.ApplicationTwo
{
    public class ApplicationTwoAuthority : ApplicationAuthority
    {
        public int ApplicationTwoId { get; set; }
        public ApplicationTwo ApplicationTwo { get; set; }
    }
}
