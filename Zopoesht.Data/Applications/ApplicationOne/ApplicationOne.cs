using Zopoesht.Data.Applications.ApplicationOne.Enums;
using Zopoesht.Data.Applications.Common.Models;

namespace Zopoesht.Data.Applications.ApplicationOne
{
    public class ApplicationOne : ApplicationCommit
    {
        public string RegisterNumber { get; set; }

        // Тип щета - потенциална или причинена (Threat or Damage)
        public ApplicationOneType ApplicationOneType { get; set; }

        public List<ApplicationOneAuthority> ApplicationOneAuthorities { get; set; } = new List<ApplicationOneAuthority>();

        public int ApplicantId { get; set; }
        public Applicant Applicant { get; set; }

        public ApplicationOneBasic ApplicationOneBasic { get; set; }

        public int? ApplicationOneLegalEntityId { get; set; }
        public ApplicationOneLegalEntity ApplicationOneLegalEntity { get; set; }

        public int? ApplicationOneThreatId { get; set; }
        public ApplicationOneThreat ApplicationOneThreat { get; set; }
        public int? ApplicationOneDamageId { get; set; }
        public ApplicationOneDamage ApplicationOneDamage { get; set; }

        public List<ApplicationOneFile> ApplicationOneFiles { get; set; } = new List<ApplicationOneFile>();

        public List<ApplicationTwo.ApplicationTwo> ApplicationTwos { get; set; } = new List<ApplicationTwo.ApplicationTwo>();
    }
}
