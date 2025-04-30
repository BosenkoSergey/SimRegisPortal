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
using SimRegisPortal.Application.Extensions;
using SimRegisPortal.Application.Factories;
using SimRegisPortal.Application.Mapper;
using SimRegisPortal.Application.Services;
using SimRegisPortal.Application.Services.Interfaces;
using SimRegisPortal.Application.Services.SalaryCalculators;
using SimRegisPortal.Application.Settings;
using SimRegisPortal.Core.Localization;
using SimRegisPortal.Core.Resources;
using SimRegisPortal.Persistence.Context;
using SimRegisPortal.Persistence.Extensions;
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

builder.Services.AddAuthorization();

#region MediatR + FluentValidation + AutoMapper

var applicationAssembly = typeof(MappingProfile).Assembly;

builder.Services.AddSingleton(
    provider => new MapperConfiguration(
            c => c.AddProfile(new MappingProfile()))
        .CreateMapper());

// Skip remaining checks if validation fails early.
ValidatorOptions.Global.DefaultClassLevelCascadeMode = CascadeMode.Stop;
ValidatorOptions.Global.DefaultRuleLevelCascadeMode = CascadeMode.Stop;

builder.Services.AddValidatorsFromAssembly(applicationAssembly);

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(applicationAssembly);
    cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
});

#endregion

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

#region AddSwagger

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc(appSettings.SwaggerConfig.Name, new OpenApiInfo
    {
        Title = appSettings.SwaggerConfig.Title,
        Description = appSettings.SwaggerConfig.Description
    });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

#endregion

builder.Services.AddSendGrid(options =>
{
    options.ApiKey = appSettings.ExternalServices.SendGrid.ApiKey;
});

#region Injections

#region Singleton

#region IStringLocalizerFactory

builder.Services.AddSingleton(serviceProvider =>
{
    var factory = serviceProvider.GetRequiredService<IStringLocalizerFactory>();
    return factory.Create(typeof(ErrorMessages));
});

builder.Services.AddSingleton(serviceProvider =>
{
    var factory = serviceProvider.GetRequiredService<IStringLocalizerFactory>();
    return factory.Create(typeof(EmailTemplates));
});

#endregion

#endregion

#region Scoped

builder.Services.AddScoped<IErrorLocalizer, ErrorLocalizer>();
builder.Services.AddScoped<IUserContext, UserContext>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<ICurrencyConverter, CurrencyConverter>();
builder.Services.AddScoped<ISalaryCalculatorFactory, SalaryCalculatorFactory>();

builder.Services.AddScoped<Fop2SalaryCalculator>();
builder.Services.AddScoped<Fop3SalaryCalculator>();
builder.Services.AddScoped<GigSalaryCalculator>();

#endregion

#region Transient

builder.Services.AddHttpClient<IPrivatBankService, PrivatBankService>();

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
