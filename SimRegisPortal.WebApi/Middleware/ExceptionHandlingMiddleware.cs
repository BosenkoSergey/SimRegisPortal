using System.Net;
using System.Text.Json;
using FluentValidation;
using Serilog.Context;
using SimRegisPortal.Application.Context;
using SimRegisPortal.Application.Extensions;
using SimRegisPortal.Core.Exceptions;
using SimRegisPortal.Core.Localization;

namespace SimRegisPortal.WebApi.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;
    private readonly IServiceScopeFactory _scopeFactory;

    public ExceptionHandlingMiddleware(
        RequestDelegate next,
        ILogger<ExceptionHandlingMiddleware> logger,
        IServiceScopeFactory scopeFactory)
    {
        _next = next;
        _logger = logger;
        _scopeFactory = scopeFactory;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception) when (context.RequestAborted.IsCancellationRequested)
        {
            return;
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        using var scope = _scopeFactory.CreateScope();
        var localizer = scope.ServiceProvider.GetRequiredService<IErrorLocalizer>();

        int statusCode;
        string message;

        switch (exception)
        {
            case TemplatedException templatedException:
                statusCode = exception switch
                {
                    CommonException => (int)HttpStatusCode.BadRequest,
                    SessionExpiredException => (int)HttpStatusCode.Unauthorized,
                    ResourceForbiddenException => (int)HttpStatusCode.Forbidden,
                    ResourceNotFoundException => (int)HttpStatusCode.NotFound,
                    _ => (int)HttpStatusCode.BadRequest
                };
                message = templatedException.GetLocalizedMessage(localizer);
                break;

            case ValidationException pipelineException:
                statusCode = (int)HttpStatusCode.BadRequest;
                message = pipelineException.Errors.First().GetLocalizedMessage(localizer);
                break;

            default:
                LogException(exception, scope);
#if DEBUG
                throw exception;
#endif
                statusCode = (int)HttpStatusCode.InternalServerError;
                message = localizer.Localize("Exception.Others");
                break;
        }

        var response = new
        {
            Error = message
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;
        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }

    private void LogException(Exception exception, IServiceScope scope)
    {
        var userContext = scope.ServiceProvider.GetRequiredService<IUserContext>();
        if (userContext.IsAuthenticated)
        {
            using (LogContext.PushProperty(nameof(userContext.UserId), userContext.UserId))
            using (LogContext.PushProperty(nameof(userContext.UserSessionId), userContext.UserSessionId))
            {
                _logger.LogError(exception, exception.Message);
            }
        }
        else
        {
            _logger.LogError(exception, exception.Message);
        }
    }
}
