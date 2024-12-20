using Microsoft.EntityFrameworkCore.Storage;

namespace Zopoesht.Infrastructure.Interfaces.Contexts
{
    public interface IAppDbContext : IBaseContext
    {
        Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
    }
}