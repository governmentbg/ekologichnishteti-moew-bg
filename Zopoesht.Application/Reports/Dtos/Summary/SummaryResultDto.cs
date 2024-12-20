namespace Zopoesht.Application.Reports.Dtos.Summary
{
    public class SummaryResultDto
    {
        public int Count { get; set; }
        public List<SummaryDto> FinishedSummaries { get; set; } = new List<SummaryDto>();
        public List<SummaryDto> OnGoingSummaries { get; set; } = new List<SummaryDto>();
    }
}
