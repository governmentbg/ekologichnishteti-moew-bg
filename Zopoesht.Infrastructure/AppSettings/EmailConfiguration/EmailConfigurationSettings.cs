﻿using Zopoesht.Infrastructure.Interfaces;

namespace Zopoesht.Infrastructure.AppSettings.EmailConfiguration
{
    public class EmailConfigurationSettings : IEmailConfiguration
    {
        public string FromAddress { get; set; }

        public string FromName { get; set; }

        public bool JobEnabled { get; set; }

        public int JobPeriod { get; set; }

        public int JobLimit { get; set; }

        public string SmtpHost { get; set; }

        public int SmtpPort { get; set; }

        public bool SmtpUseSsl { get; set; }

        public bool SmtpShouldAuthenticate { get; set; }

        public string SmtpUsername { get; set; }

        public string SmtpPassword { get; set; }
    }
}
