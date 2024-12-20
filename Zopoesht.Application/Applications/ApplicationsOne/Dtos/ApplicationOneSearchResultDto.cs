using Zopoesht.Application.Applications.Common.Dtos;
using Zopoesht.Data.Applications.ApplicationOne.Enums;
using Zopoesht.Data.Common.Enums;

namespace Zopoesht.Application.Applications.ApplicationsOne.Dtos
{
    public class ApplicationOneSearchResultDto
    {
        public int Id { get; set; }

        public int RootId { get; set; }

        public string RegisterNumber { get; set; }

        public IEnumerable<ApplicationAuthorityDto> ApplicationAuthorities { get; set; }

        public ApplicationOneType ApplicationOneType { get; set; }

        public ApplicantType ApplicantType { get; set; }

        public CommitState CommitState { get; set; }

        public string ApplicantName { get; set; }

        public string BasicName { get; set; }

        public DateTime CreateDate { get; set; }

        public bool HasApplicationTwo { get; set; }

        public CommitState? ApplicationTwoCommitState { get; set; }
    }
}
