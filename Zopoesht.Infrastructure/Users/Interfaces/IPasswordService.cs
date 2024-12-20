using Zopoesht.Infrastructure.Interfaces.Registration;

namespace Zopoesht.Infrastructure.Users.Interfaces
{
    public interface IPasswordService : IScopedService
    {
        string GenerateSalt(int bitCount);
        string HashPassword(string password, string salt);
        bool VerifyHashedPassword(string hashedPassword, string providedPassword, string salt);
    }
}