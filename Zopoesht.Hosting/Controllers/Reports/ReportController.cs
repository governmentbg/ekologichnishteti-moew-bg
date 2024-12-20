using Microsoft.AspNetCore.Mvc;
using Zopoesht.Application.Common.Dtos;
using Zopoesht.Application.Reports.Dtos;
using Zopoesht.Application.Reports.Interfaces;

namespace Zopoesht.Hosting.Controllers.Reports
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly IReportService reportService;

        public ReportController(IReportService reportService)
        {
            this.reportService = reportService;
        }

        [HttpPost]
        public async Task<SearchResultItemDto<ReportSearchResultDto>> GetFiltered([FromBody] ReportSearchFilterDto model, CancellationToken cancellationToken)
            => await reportService.GetFiltered(model, cancellationToken);
    }
}
