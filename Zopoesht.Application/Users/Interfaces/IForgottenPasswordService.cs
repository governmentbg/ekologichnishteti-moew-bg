using Zopoesht.Application.Users.Dtos;
using Zopoesht.Infrastructure.Interfaces.Registration;

namespace Zopoesht.Application.Users.Interfaces
{
    public interface IForgottenPasswordService : ITransientService
    {
        Task SendMail(EmailForgottenPasswordDto model, CancellationToken cancellationToken);
    }
}
