using MediatR;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using SimRegisPortal.Application.Context;
using SimRegisPortal.Core.Enums;
using SimRegisPortal.Web.Services.Interfaces;

namespace SimRegisPortal.Web.Components.Base;

public abstract class BaseComponent : ComponentBase
{
    [Inject] protected IServiceScopeFactory ScopeFactory { get; set; } = default!;
    [Inject] protected IUserContext UserContext { get; set; } = default!;
    [Inject] protected IUiNotifier Notifier { get; set; } = default!;
    [Inject] protected IDialogService DialogService { get; set; } = default!;
    [Inject] protected NavigationManager NavManager { get; set; } = default!;

    private bool _initialized;

    protected override async Task OnInitializedAsync()
    {
        if (_initialized)
        {
            return;
        }

        _initialized = true;

        await UserContext.InitializeAsync();

        await ExecuteSafeAsync(OnFirstInitializedAsync);
    }

    protected virtual Task OnFirstInitializedAsync() => Task.CompletedTask;

    protected async Task<bool> ExecuteSafeAsync(Func<Task> action)
    {
        try
        {
            await action();
            return true;
        }
        catch (Exception ex)
        {
            await Notifier.Exception(ex);
            return false;
        }
    }

    protected async Task<(bool IsSuccess, T? Value)> ExecuteSafeAsync<T>(Func<Task<T>> action)
    {
        try
        {
            var value = await action();
            return (true, value);
        }
        catch (Exception ex)
        {
            await Notifier.Exception(ex);
            return (false, default);
        }
    }

    protected async Task<bool> SendSafeAsync(IRequest request)
    {
        return await ExecuteSafeAsync(async () =>
        {
            using var scope = ScopeFactory.CreateScope();
            var scopedSender = scope.ServiceProvider.GetRequiredService<ISender>();
            await scopedSender.Send(request);
        });
    }

    protected async Task<(bool IsSuccess, T? Value)> SendSafeAsync<T>(IRequest<T> request)
    {
        return await ExecuteSafeAsync(async () =>
        {
            using var scope = ScopeFactory.CreateScope();
            var scopedSender = scope.ServiceProvider.GetRequiredService<ISender>();
            return await scopedSender.Send(request);
        });
    }

    protected void CheckPermissions(params UserPermissionType[] requiredPermissions)
    {
        if (!UserContext.HasAnyPermission(requiredPermissions))
        {
            NavManager.NavigateTo("/auth/denied", forceLoad: true);
            return;
        }
    }

    protected void GoBack()
    {
        NavManager.NavigateTo("javascript:history.back()");
    }

    protected async Task<bool> ValidateForm(MudForm? form)
    {
        if (form is null)
        {
            return false;
        }

        await form.Validate();

        if (!form.IsValid)
        {
            await Notifier.Error("Form is not valid.");
            return false;
        }

        return true;
    }
}