using Zopoesht.Application.Common.Dtos;
using Zopoesht.Application.Reports.Dtos;
using Zopoesht.Infrastructure.Interfaces.Registration;

namespace Zopoesht.Application.Reports.Interfaces
{
    public interface IReportService : ITransientService
    {
        Task<SearchResultItemDto<ReportSearchResultDto>> GetFiltered(ReportSearchFilterDto model, CancellationToken cancellationToken);
    }
}
