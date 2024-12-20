using Zopoesht.Infrastructure.Interfaces.Registration;

namespace Zopoesht.Infrastructure.Interfaces.Users
{
    public interface IUserContext : IScopedService
    {
        int UserId { get; }
        string Username { get; }
        string Role { get; }
        int? AuthorityId { get; }
    }
}