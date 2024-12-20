using Zopoesht.Application.Common.Dtos;
using Zopoesht.Application.Operators.Dtos;
using Zopoesht.Infrastructure.Interfaces.Registration;

namespace Zopoesht.Application.Operators.Interfaces
{
    public interface IOperatorService : ITransientService
    {
        Task<SearchResultItemDto<OperatorSearchResultDto>> GetFiltered(OperatorSearchFilterDto model, CancellationToken cancellationToken);

        Task<OperatorDto> GetById(int id, CancellationToken cancellationToken);

        Task Add(OperatorDto model, CancellationToken cancellationToken);

        Task Update(OperatorDto model, CancellationToken cancellationToken);
    }
}
