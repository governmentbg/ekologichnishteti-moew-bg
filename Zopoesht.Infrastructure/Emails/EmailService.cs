﻿using Microsoft.EntityFrameworkCore;
using Zopoesht.Data.Emails;
using Zopoesht.Data.Emails.Enums;
using Zopoesht.Data.Nomenclatures.Emails;
using Zopoesht.Infrastructure.Interfaces;
using Zopoesht.Infrastructure.Interfaces.Contexts;
using Zopoesht.Infrastructure.Logs;
using HandlebarsDotNet;
using Zopoesht.Data.Logs.Enums;
using MimeKit;
using MimeKit.Text;
using MailKit.Net.Smtp;

namespace Zopoesht.Infrastructure.Emails
{
    public class EmailService : IEmailService
    {
        private readonly IAppDbContext context;
        private readonly ILoggingService loggingService;

        public EmailService(IAppDbContext context, ILoggingService loggingService)
        {
            this.context = context;
            this.loggingService = loggingService;
        }
        public async Task<Email> ComposeEmailAsync(string alias, object templateData, params string[] recipients)
        {
            EmailType type = await this.context.Set<EmailType>()
                .AsNoTracking()
                .SingleOrDefaultAsync(e => e.Alias == alias);

            string body = type.Body;

            if (templateData != null)
            {
                var template = Handlebars.Compile(type.Body);
                body = template(templateData);
            }

            var email = new Email(type.Id, type.Subject, body);

            EmailAddresseeType emailAddresseeType;

            for (int i = 0; i <= recipients.Length - 1; i++)
            {
                if (i == 0)
                {
                    emailAddresseeType = EmailAddresseeType.To;
                }
                else
                {
                    emailAddresseeType = EmailAddresseeType.Bcc;
                }

                var addressee = new EmailAddressee(emailAddresseeType, recipients[i].Trim());
                email.Addressees.Add(addressee);
            }

            return email;
        }

        public IEnumerable<Email> GetPendingEmails(int limit)
        {
            var pendingEmails = this.context.Set<Email>()
                .Include(e => e.Addressees)
                .Where(e => e.Addressees.Any(a => a.Status == EmailStatus.Pending))
                .OrderBy(e => e.Id)
                .Take(limit)
                .ToList();

            return pendingEmails;
        }

        public bool SendEmail(Email email, IEmailConfiguration emailConfiguration)
        {
            var pendingAddressees = email.Addressees
                .Where(e => e.Status == EmailStatus.Pending)
                .ToList();

            var mimeMessage = TryComposeMimeMessage(pendingAddressees, emailConfiguration.FromName, emailConfiguration.FromAddress, email.Subject, email.Body);

            if (mimeMessage == null)
            {
                return false;
            }
            using (var smtpClient = new SmtpClient())
            {
                try
                {
                    smtpClient.ServerCertificateValidationCallback = (s, c, h, e) => true;

                    smtpClient.Connect(emailConfiguration.SmtpHost, emailConfiguration.SmtpPort, emailConfiguration.SmtpUseSsl);

                    if (emailConfiguration.SmtpShouldAuthenticate)
                    {
                        smtpClient.Authenticate(emailConfiguration.SmtpUsername, emailConfiguration.SmtpPassword);
                    }

                    smtpClient.Send(mimeMessage);

                    foreach (var addresse in pendingAddressees)
                    {
                        addresse.Status = EmailStatus.Sent;
                    }

                    return true;
                }
                catch (Exception ex)
                {
                    this.loggingService.LogExceptionAsync(ex, LogType.EmailExceptionLog);

                    return false;
                }
                finally
                {
                    smtpClient.Disconnect(true);
                }
            }
        }

        private MimeMessage TryComposeMimeMessage(List<EmailAddressee> addresses, string fromName, string fromAddress, string subject, string body)
        {
            try
            {
                var mimeMessage = new MimeMessage();
                mimeMessage.From.Add(new MailboxAddress(fromName, fromAddress));

                var toAddresses = addresses.Select(e => MailboxAddress.Parse(e.Address)).ToList();

                if (addresses.Count == 1)
                {
                    mimeMessage.To.Add(toAddresses[0]);
                }
                else
                {
                    mimeMessage.Bcc.AddRange(toAddresses);
                }

                mimeMessage.Subject = subject;
                mimeMessage.Body = new TextPart(TextFormat.Html)
                {
                    Text = body
                };

                return mimeMessage;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
