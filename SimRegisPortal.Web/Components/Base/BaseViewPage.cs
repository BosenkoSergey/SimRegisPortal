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

    protected async Task LoadEntities()
    {
        var result = await SendSafeAsync(GetCommand());
        if (result.IsSuccess)
        {
            _entities = result.Value!.ToList();
        }
    }

    protected void Add()
    {
        NavManager.NavigateTo(EditPageUrl);
    }

    protected void Edit(Guid id)
    {
        NavManager.NavigateTo($"{EditPageUrl}/{id}");
    }
}