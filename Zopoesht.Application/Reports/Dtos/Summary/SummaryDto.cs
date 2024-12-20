using Zopoesht.Application.Applications.ApplicationsOne.Dtos;
using Zopoesht.Application.Operators.Dtos;
using Zopoesht.Application.Reports.Enums.Summary;
using Zopoesht.Data.Applications.ApplicationOne.Enums;

namespace Zopoesht.Application.Reports.Dtos.Summary
{
    public class SummaryDto
    {
        public SummaryStageType SummaryStageType { get; set; }
        public string Description { get; set; }
        public string DescriptionAlt { get; set; }
        public ApplicationOneType ApplicationOneType { get; set; }
        public int? StartYear { get; set; }
        public int? EndYear { get; set; }

        // Basic information
        public bool HasWaterDamage { get; set; }
        public bool HasSoilDamage { get; set; }
        public bool HasSpeciesDamage { get; set; }

        public DateTime? OccurrenceDate { get; set; }
        public string OccurrenceDateDescription { get; set; }
        public string OccurrenceDateDescriptionAlt { get; set; }
        public List<ActivityOfferingDto> ActivityOfferings { get; set; }


        // Additional information
        public DateTime? ProcedureInitiatedDate { get; set; }
        public string ProcedureInitiatedDateDescription { get; set; }
        public string ProcedureInitiatedDateDescriptionAlt { get; set; }

        public ApplicantDto Applicant { get; set; }

        public string ActionsTaken { get; set; }
        public string ActionsTakenAlt { get; set; }
    }
}
