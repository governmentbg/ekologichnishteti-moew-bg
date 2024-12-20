using Zopoesht.Application.Operators.Dtos;
using Zopoesht.Infrastructure.Interfaces.Registration;

namespace Zopoesht.Application.Operators.Interfaces
{
    public interface IActivityOfferingService : ITransientService
    {
        Task Add(ActivityOfferingDto model, CancellationToken cancellationToken);

        Task Update(ActivityOfferingDto model, CancellationToken cancellationToken);
    }
}
