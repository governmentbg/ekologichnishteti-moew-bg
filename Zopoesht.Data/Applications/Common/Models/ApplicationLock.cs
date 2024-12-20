using Zopoesht.Data.Applications.Common.Enums;
using Zopoesht.Data.Common.Interfaces;
using Zopoesht.Data.Users;

namespace Zopoesht.Data.Applications.Common.Models
{
    public class ApplicationLock : IEntity
    {
        public int Id { get; set; }

        public ApplicationType ApplicationType { get; set; }

        public int ApplicationId { get; set; }

        public DateTime? LockedDate { get; set; }
        public DateTime? UnlockedDate { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public bool IsLocked { get; set; }


        public ApplicationLock(int applicationId, int userId, ApplicationType applicationType)
        {
            ApplicationType = applicationType;
            ApplicationId = applicationId;
            UserId = userId;
        }

        public void Lock()
        {
            LockedDate = DateTime.Now;
            IsLocked = true;
        }

        public void Unlock()
        {
            UnlockedDate = DateTime.Now;
            IsLocked = false;
        }
    }
}
