using MediatR;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using SimRegisPortal.Application.Models.Entities;

namespace SimRegisPortal.Web.Components.Base;

public abstract class BaseEditPage<TKey, TDto> : BaseComponent
    where TKey : struct
    where TDto : BaseEntityDto<TKey>, new()
{
    [Parameter] public TKey? Id { get; set; }
    [Parameter] public string PageTitle { get; set; } = "";

    protected MudForm? _form;
    protected TDto? _model;

    protected string PageTitleText => $"{(_model?.IsNew is true ? "Add" : "Edit")} {PageTitle}";

    protected abstract IRequest<TDto> GetCommand(TKey Id);
    protected abstract IRequest<TDto> SaveCommand(TDto model);

    protected virtual TDto CreateNew()
    {
        return new TDto() { IsNew = true };
    }

    protected virtual async Task LoadModelAsync()
    {
        if (Id.HasValue)
        {
            var result = await SendSafeAsync(GetCommand(Id.Value));
            if (result.IsSuccess)
            {
                _model = result.Value;
            }
        }
        else
        {
            _model = CreateNew();
        }
    }

    protected async Task Save()
    {
        if (!await ValidateForm(_form)) return;
        if (_model is null) return;

        var result = await SendSafeAsync(SaveCommand(_model));
        if (result.IsSuccess)
        {
            _model = result.Value;
            await Notifier.Success("Saved successfully.");
        }
    }
}