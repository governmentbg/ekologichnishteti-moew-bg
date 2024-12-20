using Zopoesht.Application.Operators.Dtos;
using Zopoesht.Infrastructure.Interfaces.Registration;

namespace Zopoesht.Application.Operators.Interfaces
{
    public interface ITerrainService : ITransientService
    {
        Task Add(TerrainDto model, CancellationToken cancellationToken);

        Task Update(TerrainDto model, CancellationToken cancellationToken);
    }
}
