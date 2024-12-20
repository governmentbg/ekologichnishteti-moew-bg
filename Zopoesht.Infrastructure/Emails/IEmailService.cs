using Zopoesht.Data.Emails;
using Zopoesht.Infrastructure.Interfaces;
using Zopoesht.Infrastructure.Interfaces.Registration;

namespace Zopoesht.Infrastructure.Emails
{
    public interface IEmailService : IScopedService
    {
        IEnumerable<Email> GetPendingEmails(int limit);

        Task<Email> ComposeEmailAsync(string alias, object templateData, params string[] recipients);

        bool SendEmail(Email email, IEmailConfiguration emailConfiguration);
    }
}
