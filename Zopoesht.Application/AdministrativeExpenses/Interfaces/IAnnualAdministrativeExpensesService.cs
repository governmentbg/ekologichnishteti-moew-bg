using Zopoesht.Application.AdministrativeExpenses.Dtos;
using Zopoesht.Infrastructure.Interfaces.Registration;

namespace Zopoesht.Application.AdministrativeExpenses.Interfaces
{
    public interface IAnnualAdministrativeExpensesService : ITransientService
    {
        Task<AnnualAdministrativeExpensesHistorySearchResultDto> GetHistory(int rootId, CancellationToken cancellationToken);

        Task<AnnualAdministrativeExpensesDto> Add(AnnualAdministrativeExpensesDto model, CancellationToken cancellationToken);

        Task Update(AnnualAdministrativeExpensesDto model, CancellationToken cancellationToken);
    }
}
