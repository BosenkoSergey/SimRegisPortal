using MediatR;
using MudBlazor;
using SimRegisPortal.Application.Models.Entities;

namespace SimRegisPortal.Web.Components.Base;

public abstract class BaseViewPage<TKey, TDto> : BaseComponent
    where TKey : struct
    where TDto : BaseEntityDto<TKey>, new()
{
    protected string PageTitle { get; set; } = "";
    protected string EditPageUrl { get; set; } = "";

    protected MudDataGrid<TDto>? _grid;
    protected List<TDto> _entities = [];

    protected abstract IRequest<IEnumerable<TDto>> GetCommand();
    //protected abstract IRequest DeleteCommand(TKey Id);

    protected async Task LoadEntities()
    {
        var result = await SendSafeAsync(GetCommand());
        if (result.IsSuccess)
        {
            _entities = result.Value!.ToList();
        }
    }

    protected virtual Task Add()
    {
        NavManager.NavigateTo(EditPageUrl);
        return Task.CompletedTask;
    }

    protected virtual Task Edit(TKey id)
    {
        NavManager.NavigateTo($"{EditPageUrl}/{id}");
        return Task.CompletedTask;
    }

    //protected virtual async Task Delete(TKey id)
    //{
    //    var result = await SendSafeAsync(DeleteCommand(id));
    //    if (result)
    //    {
    //        await Notifier.Success("Deleted successfully.");
    //    }
    //}
}