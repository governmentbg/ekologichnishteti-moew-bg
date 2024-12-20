using Zopoesht.Hosting.Extensions;
using Zopoesht.Infrastructure.AppSettings;
using Zopoesht.Infrastructure.DIExtensions;
using Zopoesht.Application;
using Zopoesht.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
var configuration = builder.Environment.SetupAppSettings();
AppSettingsConfiguration.ConfigureAppSettings(configuration);
builder.Services.ConfigureDbContextService();
builder.Services.ConfigureCommonServices(configuration);
builder.Services.ConfigureApplicationServices();
builder.Services.ConfigureAutoMapper();
builder.Services.StartJobs();
builder.Services.ConfigureJwtAuthService();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();

app.ConfigureMiddlewares();

app.ConfigureStaticFiles();

app.MapControllers()
   .RequireAuthorization();

app.Run();
