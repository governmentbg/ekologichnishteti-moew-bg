using Microsoft.AspNetCore.Mvc;
using Zopoesht.Application.Reports.Dtos.Summary;
using Zopoesht.Application.Reports.Interfaces;

namespace Zopoesht.Hosting.Controllers.Reports
{
    [ApiController]
    [Route("api/[controller]")]
    public class SummaryController : ControllerBase
    {
        private readonly ISummaryService summaryService;

        public SummaryController(ISummaryService summaryService)
        {
            this.summaryService = summaryService;
        }

        [HttpPost]
        public async Task<SummaryResultDto> GetSummary(SummaryFilterDto filter)
        {
            return await this.summaryService.GetSummary(filter);
        }

        [HttpPost("Export")]
        public async Task<IActionResult> ExportExcelFile([FromBody] SummaryFilterDto filter, CancellationToken cancellationToken)
        {
            byte[] byteArray = await this.summaryService.ExportWordFile(filter, cancellationToken);

            return File(byteArray, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", $"Summary_{DateTime.Now:yyyyMMdd}.docx");
        }
    }
}
