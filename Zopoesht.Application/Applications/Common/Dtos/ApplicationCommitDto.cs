using Zopoesht.Data.Common.Enums;
using Zopoesht.Infrastructure.DomainValidation;
using Zopoesht.Infrastructure.Interfaces;

namespace Zopoesht.Application.Applications.Common.Dtos
{
    public abstract class ApplicationCommitDto : IValidate
    {
        public int Id { get; set; }

        public CommitState CommitState { get; set; }

        public DateTime BlankDate { get; set; }

        public int RootId { get; set; }

        public bool? HasHistory { get; set; }

        public bool? HasPermissionToEdit { get; set; }

        public bool? HasPermissionToControl { get; set; }

        public IEnumerable<ApplicationLockDto> Locks { get; set; }

        public abstract void ValidateProperties(DomainValidationService validationService);
    }
}
