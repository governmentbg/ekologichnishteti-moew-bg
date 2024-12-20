using Microsoft.Extensions.DependencyInjection;
using Zopoesht.Infrastructure.Interfaces.Registration;

namespace Zopoesht.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services)
        {
            services.Scan(scan => scan
                .FromCallingAssembly()

                .AddClasses(classes => classes.AssignableTo<ITransientService>())
                .AsMatchingInterface()
                .WithTransientLifetime()

                .AddClasses(classes => classes.AssignableTo<IScopedService>())
                .AsMatchingInterface()
                .WithScopedLifetime()

                .AddClasses(classes => classes.AssignableTo<IScopedService>())
                .AsSelf()
                .WithScopedLifetime()
                );

            return services;
        }
    }
}