using Zopoesht.Application.Applications.ApplicationsOne.Dtos;
using Zopoesht.Application.Applications.Common.Interfaces;
using Zopoesht.Application.Common.Dtos;
using Zopoesht.Application.Reports.Dtos;

namespace Zopoesht.Application.Applications.ApplicationsOne.Interfaces
{
    public interface IApplicationOneService : IApplicationService<ApplicationOneDto>
    {
        Task<SearchResultItemDto<ApplicationOneSearchResultDto>> GetFiltered(ApplicationOneSearchFilterDto model, CancellationToken cancellationToken);

        Task DeleteApplication(int id, CancellationToken cancellationToken);

        Task<ApplicationOneDto> RestoreApplication(int id, CancellationToken cancellationToken);

        Task<int> GetIdByAppTwoId(int appTwoId, CancellationToken cancellationToken);

        Task<byte[]> ExportExcelFile(ReportSearchFilterDto filter, CancellationToken cancellationToken);
    }
}
