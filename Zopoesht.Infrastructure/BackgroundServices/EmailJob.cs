using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Zopoesht.Data.Emails.Enums;
using Zopoesht.Data.Emails;
using Zopoesht.Infrastructure.Emails;
using Zopoesht.Infrastructure.Interfaces.Contexts;
using Zopoesht.Infrastructure.AppSettings;

namespace Zopoesht.Infrastructure.BackgroundServices
{
    public class EmailJob : IHostedService, IDisposable
    {
        private readonly IServiceProvider serviceProvider;
        private Timer timer;

        public EmailJob(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(AppSettingsConfiguration.EmailConfiguration.JobPeriod));

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            timer?.Dispose();
        }

        private async void DoWork(object state)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<IAppDbContext>();
                var emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();

                var pendingEmails = emailService.GetPendingEmails(AppSettingsConfiguration.EmailConfiguration.JobLimit);

                foreach (var email in pendingEmails)
                {
                    bool isSent = emailService.SendEmail(email, AppSettingsConfiguration.EmailConfiguration);
                    foreach (EmailAddressee addressee in email.Addressees)
                    {
                        addressee.AttemptsCounter += 1;

                        if (isSent)
                        {
                            addressee.Status = EmailStatus.Sent;
                        }
                        else if (!isSent && addressee.AttemptsCounter > 5)
                        {
                            addressee.Status = EmailStatus.Failed;
                        }
                    }
                }

                await dbContext.SaveChangesAsync();
            }
        }
    }
}
