using FluentValidation;
using MudBlazor;
using SimRegisPortal.Application.Extensions;
using SimRegisPortal.Core.Exceptions;
using SimRegisPortal.Core.Localization;
using SimRegisPortal.Web.Services.Interfaces;

namespace SimRegisPortal.Web.Services;

public class UiNotifier : IUiNotifier
{
    private readonly ISnackbar _snackbar;
    private readonly IErrorLocalizer _localizer;
    private readonly ILogger<UiNotifier> _logger;

    public UiNotifier(
        ISnackbar snackbar,
        IErrorLocalizer localizer,
        ILogger<UiNotifier> logger)
    {
        _snackbar = snackbar;
        _localizer = localizer;
        _logger = logger;
    }

    public Task Success(string message)
    {
        _snackbar.Add(message, MudBlazor.Severity.Success);
        return Task.CompletedTask;
    }

    public Task Info(string message)
    {
        _snackbar.Add(message, MudBlazor.Severity.Normal);
        return Task.CompletedTask;
    }

    public Task Warning(string message)
    {
        _snackbar.Add(message, MudBlazor.Severity.Warning);
        return Task.CompletedTask;
    }

    public Task Error(string message)
    {
        _snackbar.Add(message, MudBlazor.Severity.Error);
        return Task.CompletedTask;
    }

    public async Task Exception(Exception exception)
    {
        string message;

        switch (exception)
        {
            case TemplatedException templatedException:
                message = templatedException.GetLocalizedMessage(_localizer);
                break;

            case ValidationException pipelineException:
                message = pipelineException.Errors.First().GetLocalizedMessage(_localizer);
                break;

            default:
                _logger.LogError(exception, exception.Message);
                message = _localizer.Localize("Exception.Others");
                break;
        }

        await Error(message);
    }
}
