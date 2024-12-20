using Zopoesht.Infrastructure.Interfaces.Registration;

namespace Zopoesht.Infrastructure.Users.Interfaces
{
    public interface IJWTService : IScopedService
    {
        string CreateToken(int userId, string roleAlias, string username, int? authorityId);
    }
}