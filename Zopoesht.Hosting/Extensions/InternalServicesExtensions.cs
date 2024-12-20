using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Zopoesht.Infrastructure.AppSettings;
using Zopoesht.Infrastructure.Middlewares;
using Zopoesht.Persistence.Contexts;
using Zopoesht.Infrastructure.Interfaces.Contexts;
using Zopoesht.Infrastructure.BackgroundServices;
using Zopoesht.Application.Infrastructure.AutoMapperProfiles;

namespace Zopoesht.Hosting.Extensions
{
    public static class InternalServicesExtensions
    {
        public static void ConfigureDbContextService(this IServiceCollection services)
        {
            services
                .AddDbContext<IAppDbContext, AppDbContext>(o =>
                {
                    o.UseNpgsql(AppSettingsConfiguration.ConnectionString,
                        e => e.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery));
                    o.EnableSensitiveDataLogging();
                })
                .AddDbContext<IAppLogContext, AppLogContext>(o =>
                {
                    o.UseNpgsql(AppSettingsConfiguration.LogConnectionString,
                        e => e.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery));
                    o.EnableSensitiveDataLogging();
                });

            //File storage
        }

        public static void ConfigureAutoMapper(this IServiceCollection services)
        {
            services.AddScoped(provider => new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(typeof(ApplicationOneProfile));
                cfg.AddProfile(typeof(ApplicationTwoProfile));
                cfg.AddProfile(typeof(ApplicationCommonProfile));
                cfg.AddProfile(typeof(UserProfile));
                cfg.AddProfile(typeof(OperatorProfile));
                cfg.AddProfile(typeof(AdministrativeExpensesProfile));
                cfg.AddProfile(typeof(ReportsProfile));
            }).CreateMapper());
        }

        public static void ConfigureStaticFiles(this WebApplication app)
        {
            app.UseDefaultFiles();
            app.UseStaticFiles(new StaticFileOptions
            {
                OnPrepareResponse = context =>
                {
                    if (context.File.Name == "index.html")
                    {
                        context.Context.Response.Headers.Append("Cache-Control", "no-cache, no-store");
                        context.Context.Response.Headers.Append("Expires", "-1");
                    }
                }
            });
        }

        public static void ConfigureMiddlewares(this IApplicationBuilder app)
        {
            app.UseMiddleware<RedirectionMiddleware>();
            app.UseMiddleware<RequestLoggingMiddleware>();
        }

        public static void StartJobs(this IServiceCollection services)
        {
            var emailConfiguration = AppSettingsConfiguration.EmailConfiguration;

            if (emailConfiguration.JobEnabled)
            {
                services.AddHostedService<EmailJob>();
            }
        }
    }
}