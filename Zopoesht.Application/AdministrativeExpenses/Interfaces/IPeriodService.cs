using Zopoesht.Application.AdministrativeExpenses.Dtos;
using Zopoesht.Infrastructure.Interfaces.Registration;

namespace Zopoesht.Application.AdministrativeExpenses.Interfaces
{
    public interface IPeriodService : ITransientService
    {
        Task<List<PeriodDto>> GetAll(CancellationToken cancellationToken);

        Task<int> Add(PeriodDto model, CancellationToken cancellationToken);

        Task Update(PeriodDto model, CancellationToken cancellationToken);
    }
}
