using CMS.Core.AppSettings;
using Common.Core.AppSettings;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<AppSettings>(builder.Configuration);
var appSettings = builder.Configuration.Get<AppSettings>()!;

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

#region Injections

#region Singleton

builder.Services.AddSingleton(typeof(IAppSettingsProvider), typeof(AppSettingsProvider));

#endregion

#region Scoped

#endregion

#region Transient

#endregion

#endregion


var app = builder.Build();

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
