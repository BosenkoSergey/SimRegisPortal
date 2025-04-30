using AutoMapper;
using FluentValidation;
using MediatR.Extensions.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using MudBlazor;
using MudBlazor.Services;
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
using SimRegisPortal.Web.Components;
using SimRegisPortal.Web.Controllers.Middleware;
using SimRegisPortal.Web.Services;
using SimRegisPortal.Web.Services.Interfaces;


var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<AppSettings>(builder.Configuration);
var appSettings = builder.Configuration.Get<AppSettings>()!;

builder.AddSerilog(appSettings);
builder.Services.AddLocalization();

#region DbContext

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

builder.Services.AddControllersWithViews();
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

#region Auth

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
    {
        options.LoginPath = "/auth/login";
        options.LogoutPath = "/auth/logout";
        options.AccessDeniedPath = "/auth/denied";
        options.ExpireTimeSpan = TimeSpan.FromHours(8);
        options.SlidingExpiration = true;
        options.Cookie.SameSite = SameSiteMode.Lax;
        options.Cookie.SecurePolicy = CookieSecurePolicy.None;
        options.Cookie.HttpOnly = true;
        options.Cookie.Path = "/";
    });
builder.Services.AddAuthorization();
builder.Services.AddCascadingAuthenticationState();

#endregion

builder.Services.AddHttpContextAccessor();

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

builder.Services.AddMudServices(config =>
{
    config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.TopCenter;
});

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

#region SendGrid

builder.Services.AddSendGrid(options =>
{
    options.ApiKey = appSettings.ExternalServices.SendGrid.ApiKey;
});

#endregion

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

builder.Services.AddScoped(sp =>
{
    var nav = sp.GetRequiredService<NavigationManager>();
    return new HttpClient { BaseAddress = new Uri(nav.BaseUri) };
});

builder.Services.AddScoped<IErrorLocalizer, ErrorLocalizer>();
builder.Services.AddScoped<IUserContext, UserContext>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<ICurrencyConverter, CurrencyConverter>();
builder.Services.AddScoped<ISalaryCalculatorFactory, SalaryCalculatorFactory>();

builder.Services.AddScoped<Fop2SalaryCalculator>();
builder.Services.AddScoped<Fop3SalaryCalculator>();
builder.Services.AddScoped<GigSalaryCalculator>();

builder.Services.AddScoped<IUiNotifier, UiNotifier>();

#endregion

#region Transient

builder.Services.AddHttpClient<IPrivatBankService, PrivatBankService>();

#endregion

#endregion

builder.Services.AddControllers();


var app = builder.Build();

app.Services.PrepareDatabase();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseRouting();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseAntiforgery();
app.UseStaticFiles();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapControllers();

app.Run();
