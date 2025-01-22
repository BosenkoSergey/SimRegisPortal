using CMS.Core.AppSettings;
using CMS.Core.AppSettings.Interfaces;
using CMS.Domain.Database;
using CMS.Domain.Database.Extensions;
using CMS.Domain.Database.Repository;
using CMS.Domain.Database.Services.Common;
using CMS.Domain.Database.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<AppSettings>(builder.Configuration);
var appSettings = builder.Configuration.Get<AppSettings>()!;

builder.Services.AddDbContext<CmsDbContext>(
    options =>
    {
        options.UseSqlServer(
            appSettings.ConnectionStrings.CmsDbConnection,
            opts =>
            {
                opts.MigrationsAssembly(typeof(CmsDbContext).Assembly.FullName);
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

#region Injections

#region Singleton

builder.Services.AddSingleton(typeof(IAppSettingsProvider), typeof(AppSettingsProvider));

#endregion

#region Scoped

builder.Services.AddScoped(typeof(IRepository<>), typeof(CmsRepository<>));
builder.Services.AddScoped(typeof(IUnitOfWork), typeof(CmsUnitOfWork));
builder.Services.AddScoped(typeof(IEntityService<>), typeof(EntityService<>));

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
