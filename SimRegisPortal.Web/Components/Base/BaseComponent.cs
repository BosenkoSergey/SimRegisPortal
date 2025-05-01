using MediatR;
using Microsoft.AspNetCore.Components;
using SimRegisPortal.Application.Context;
using SimRegisPortal.Web.Services.Interfaces;

namespace SimRegisPortal.Web.Components.Base;

public abstract class BaseComponent : ComponentBase
{
    [Inject] protected ISender Mediator { get; set; } = default!;
    [Inject] protected IUserContext UserContext { get; set; } = default!;
    [Inject] protected IUiNotifier Notifier { get; set; } = default!;
    [Inject] protected NavigationManager NavManager { get; set; } = default!;

    private bool _initialized;

    protected override async Task OnInitializedAsync()
    {
        if (_initialized)
        {
            return;
        }

        _initialized = true;

        try
        {
            await OnFirstInitializedAsync();
        }
        catch (Exception ex)
        {
            await Notifier.Exception(ex);
        }
    }

    protected virtual Task OnFirstInitializedAsync() => Task.CompletedTask;
}