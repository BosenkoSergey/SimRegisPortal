using MediatR;
using Microsoft.AspNetCore.Components;
using SimRegisPortal.Web.Services.Interfaces;

namespace SimRegisPortal.Web.Components.Base;

public abstract class BaseComponent : ComponentBase
{
    [Inject] protected IUiNotifier Notifier { get; set; } = default!;
    [Inject] protected ISender Mediator { get; set; } = default!;
}