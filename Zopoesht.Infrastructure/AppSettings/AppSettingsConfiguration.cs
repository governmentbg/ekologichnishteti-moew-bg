using Microsoft.Extensions.Configuration;
using Zopoesht.Infrastructure.AppSettings.AuthConfiguration;
using Zopoesht.Infrastructure.AppSettings.DeepLTranslateConfiguration;
using Zopoesht.Infrastructure.AppSettings.EmailConfiguration;
using Zopoesht.Infrastructure.AppSettings.FileStorageConfigurations;

namespace Zopoesht.Infrastructure.AppSettings
{
    public static class AppSettingsConfiguration
    {
        public static string ConnectionString { get; private set; }
        public static string LogConnectionString { get; private set; }
        public static AuthConfigurationSettings AuthConfiguration { get; set; }
        public static EmailConfigurationSettings EmailConfiguration { get; set; }

        public static DeepLTranslateConfigurationSettings DeepLTranslateConfiguration { get; set; }

        public static List<FileStorageSettings> FileStorages { get; private set; } = new List<FileStorageSettings>();
        //public static bool IsTestEnvironment { get; private set; }

        public static void ConfigureAppSettings(IConfiguration configuration)
        {
            if (configuration.GetSection("DbConfiguration:ConnectionString").Exists())
            {
                ConnectionString = configuration.GetSection("DbConfiguration:ConnectionString").Get<string>();
            }

            if (configuration.GetSection("DbConfiguration:LogConnectionString").Exists())
            {
                LogConnectionString = configuration.GetSection("DbConfiguration:LogConnectionString").Get<string>();
            }

            if (configuration.GetSection("AuthConfiguration").Exists())
            {
                AuthConfiguration = configuration.GetSection("AuthConfiguration").Get<AuthConfigurationSettings>();
            }

            if (configuration.GetSection("EmailConfiguration").Exists())
            {
                EmailConfiguration = configuration.GetSection("EmailConfiguration").Get<EmailConfigurationSettings>();
            }

            if (configuration.GetSection("DeepLTranslateConfiguration").Exists())
            {
                DeepLTranslateConfiguration = configuration.GetSection("DeepLTranslateConfiguration").Get<DeepLTranslateConfigurationSettings>();
            }

            if (configuration.GetSection("DbConfiguration:Descriptors").Exists())
            {
                FileStorages = configuration.GetSection("DbConfiguration:Descriptors").Get<List<FileStorageSettings>>();
            }
        }
    }
}