using Zopoesht.Infrastructure.Interfaces.Registration;
using Zopoesht.Application.Users.Dtos;

namespace Zopoesht.Application.Users.Interfaces
{
    public interface ILoginService : ITransientService
    {
        Task<UserLoginInfoDto> Login(UserCredentialsDto model, CancellationToken cancellationToken);
    }
}