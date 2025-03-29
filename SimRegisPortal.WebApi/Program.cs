using System.Reflection;
using System.Text;
using AutoMapper;
using FluentValidation;
using MediatR.Extensions.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SendGrid.Extensions.DependencyInjection;
using SimRegisPortal.Application.Context;
using SimRegisPortal.Application.Mapper;
using SimRegisPortal.Application.Services;
using SimRegisPortal.Application.Services.Interfaces;
using SimRegisPortal.Core.AppSettings;
using SimRegisPortal.Core.AppSettings.Interfaces;
using SimRegisPortal.Core.Localization;
using SimRegisPortal.Core.Resources;
using SimRegisPortal.Mailing.Provider;
using SimRegisPortal.Persistence.Context;
using SimRegisPortal.Persistence.Extensions;
using SimRegisPortal.WebApi.Extensions;
using SimRegisPortal.WebApi.Middleware;


var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<AppSettings>(builder.Configuration);
var appSettings = builder.Configuration.Get<AppSettings>()!;

builder.AddSerilog(appSettings);
builder.Services.AddLocalization();

#region AddDbContext

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

#endregion

builder.Services.AddHttpContextAccessor();

#region AddAuthentication

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var jwtSettings = appSettings.AuthSettings.AccessToken;
        var key = Encoding.UTF8.GetBytes(jwtSettings.SecretKey);

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = !string.IsNullOrEmpty(jwtSettings.Issuer),
            ValidIssuer = jwtSettings.Issuer,
            ValidateAudience = !string.IsNullOrEmpty(jwtSettings.Audience),
            ValidAudience = jwtSettings.Audience,
            ClockSkew = TimeSpan.Zero
        };
    });

#endregion

builder.Services.AddAuthorization();

var assembly = Assembly.GetExecutingAssembly();

#region MediatR + FluentValidation + AutoMapper

builder.Services.AddSingleton(
    provider => new MapperConfiguration(
            c => c.AddProfile(new MappingProfile()))
        .CreateMapper());

// Skip remaining checks if validation fails early.
ValidatorOptions.Global.DefaultClassLevelCascadeMode = CascadeMode.Stop;
ValidatorOptions.Global.DefaultRuleLevelCascadeMode = CascadeMode.Stop;

builder.Services.AddValidatorsFromAssembly(assembly);

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(assembly);
    cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
});

#endregion

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

#region AddSwagger

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc(appSettings.SwaggerConfig.Name, new OpenApiInfo
    {
        Title = appSettings.SwaggerConfig.Title,
        Description = appSettings.SwaggerConfig.Description
    });
});

#endregion

builder.Services.AddSendGrid(options =>
{
    options.ApiKey = appSettings.ExternalServices.SendGrid.ApiKey;
});

#region Injections

#region Singleton

builder.Services.AddSingleton(serviceProvider =>
{
    var factory = serviceProvider.GetRequiredService<IStringLocalizerFactory>();
    return factory.Create(typeof(ErrorMessages));
});

builder.Services.AddSingleton<IAccessTokenService, AccessTokenService>();

builder.Services.AddSingleton(typeof(IAppSettingsProvider), typeof(AppSettingsProvider));
builder.Services.AddSingleton(typeof(IEmailProvider), typeof(EmailProvider));

#endregion

#region Scoped

builder.Services.AddScoped<IErrorLocalizer, ErrorLocalizer>();
builder.Services.AddScoped<IUserContext, UserContext>();

#endregion

#region Transient

#endregion

#endregion


var app = builder.Build();

app.Services.PrepareDatabase();

app.UseMiddleware<ExceptionHandlingMiddleware>();

#region UseSwagger

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

#endregion

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
