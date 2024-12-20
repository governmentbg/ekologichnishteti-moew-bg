using Zopoesht.Application.Reports.Dtos.Summary;
using Zopoesht.Infrastructure.Interfaces.Registration;

namespace Zopoesht.Application.Reports.Interfaces
{
    public interface ISummaryService : ITransientService
    {
        Task<SummaryResultDto> GetSummary(SummaryFilterDto filter);

        Task<byte[]> ExportWordFile(SummaryFilterDto filter, CancellationToken cancellationToken);
    }
}
