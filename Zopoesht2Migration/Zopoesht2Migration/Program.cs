using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Zopoesht2Migration;
using Zopoesht2Migration.DbContexts;
using Zopoesht2Migration.DbContexts.MosvEcologicalDamage;
using Zopoesht2Migration.DbContexts.Zopoesht2;

var services = new ServiceCollection();
services
.AddDbContext<Zopoesht2Context>(o =>
{
    o.UseNpgsql(DbConfigurations.Zopoesht2Context,
        e => e.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery));
})
.AddDbContext<MosvEcologicalDamageContext>(o =>
     {
         o.UseSqlServer(DbConfigurations.Zopoesht1ProdContext,
             e => e.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery));
});

services.AddScoped<MigrationService>();

ServiceProvider serviceProvider = services.BuildServiceProvider();

Console.WriteLine($"Migration starts ! Time: {DateTime.Now}");

try
{
    var migrationService = serviceProvider.GetService<MigrationService>();
    MigrationService.StartMigration();
}
catch (Exception exception)
{
    while (exception.InnerException != null) { exception = exception.InnerException; }
    var exceptionInfo = $"\nDate: {DateTime.Now}\nType: {exception.GetType().FullName}\nMessage: {exception.Message}\nStackTrace: {exception.StackTrace}\n";
    Console.WriteLine(exceptionInfo);
}
finally
{
    Console.WriteLine($"\nMigration finished ! Time: {DateTime.Now}\n");
    Console.ReadLine();
}
