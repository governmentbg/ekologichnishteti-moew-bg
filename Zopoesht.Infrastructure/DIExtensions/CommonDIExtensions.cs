using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Zopoesht.Infrastructure.DomainValidation;
using Zopoesht.Infrastructure.Interfaces.Registration;
using Scrutor;
using FileStorageNetCore;
using Microsoft.Extensions.Configuration;

namespace Zopoesht.Infrastructure.DIExtensions
{
    public static class CommonDIExtensions
    {
        public static void ConfigureCommonServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Scan(scan => scan
                .FromCallingAssembly()

                .AddClasses(classes => classes.AssignableTo<IScopedService>())
                .AsMatchingInterface()
                .WithScopedLifetime());

            services
                .Scan(scan => scan
                .AddType<DomainValidationService>()
                .AsSelf()
                .WithScopedLifetime());

            services
            .AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddFileStorage(configuration.GetSection("DbConfiguration:Descriptors"), configuration.GetSection("fileStoragesEncryption"));

            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }
    }
}