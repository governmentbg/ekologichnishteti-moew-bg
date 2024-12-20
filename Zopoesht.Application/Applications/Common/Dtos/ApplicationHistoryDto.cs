using Zopoesht.Data.Applications.Common.Enums;

namespace Zopoesht.Application.Applications.Common.Dtos
{
    public class ApplicationHistoryDto
    {
        public ApplicationHistoryType Type { get; set; }

        public DateTime ModificationDate { get; set; }

        public int? ApplicationId { get; set; }
        public int? RootId { get; set; }

        public string UserFullName { get; set; }
    }
}
