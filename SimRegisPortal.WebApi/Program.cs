using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SendGrid.Extensions.DependencyInjection;
using SimRegisPortal.Core.AppSettings;
using SimRegisPortal.Core.AppSettings.Interfaces;
using SimRegisPortal.Persistence.Extensions;
using SimRegisPortal.Mailing.Provider;
using SimRegisPortal.Persistence.Context;


var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<AppSettings>(builder.Configuration);
var appSettings = builder.Configuration.Get<AppSettings>()!;

builder.Services.AddDbContext<AppDbContext>(
    options =>
    {
        options.UseSqlServer(
            appSettings.ConnectionStrings.SimRegisPortalDbConnection,
            opts =>
            {
                opts.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName);
            });
    });

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc(appSettings.SwaggerConfig.Name, new OpenApiInfo
    {
        Title = appSettings.SwaggerConfig.Title,
        Description = appSettings.SwaggerConfig.Description
    });
});

builder.Services.AddSendGrid(options =>
{
    options.ApiKey = appSettings.ExternalServices.SendGrid.ApiKey;
});

#region Injections

#region Singleton

builder.Services.AddSingleton(typeof(IAppSettingsProvider), typeof(AppSettingsProvider));
builder.Services.AddSingleton(typeof(IEmailProvider), typeof(EmailProvider));

#endregion

#region Scoped

#endregion

#region Transient

#endregion

#endregion


var app = builder.Build();

app.Services.PrepareDatabase();

if (!app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint(
            $"/swagger/{appSettings.SwaggerConfig.Name}/swagger.json",
            appSettings.SwaggerConfig.Title
        );
        options.DisplayRequestDuration();
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
