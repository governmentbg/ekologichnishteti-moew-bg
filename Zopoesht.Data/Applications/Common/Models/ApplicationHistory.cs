using Zopoesht.Data.Applications.Common.Enums;
using Zopoesht.Data.Common.Interfaces;

namespace Zopoesht.Data.Applications.Common.Models
{
    public class ApplicationHistory : IEntity
    {
        public int Id { get; set; }

        public ApplicationHistoryType Type { get; set; }

        public int UserId { get; set; }

        public string UserFullName { get; set; }

        public DateTime ModificationDate { get; set; }

        public int ApplicationId { get; set; }
        public int RootId { get; set; }

        public ApplicationType ApplicationType { get; set; }
    }
}
