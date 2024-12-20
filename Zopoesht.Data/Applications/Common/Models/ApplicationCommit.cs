using Zopoesht.Data.Common.Enums;
using Zopoesht.Data.Common.Interfaces;

namespace Zopoesht.Data.Applications.Common.Models
{
    public abstract class ApplicationCommit : IEntity, IAuditable
    {
        public int Id { get; set; }
        public CommitState CommitState { get; set; }
        public DateTime CreateDate { get; set; }
        public int CreatorUserId { get; set; }
        public string ChangeStateDescription { get; set; }
        public int RootId { get; set; }
    }
}