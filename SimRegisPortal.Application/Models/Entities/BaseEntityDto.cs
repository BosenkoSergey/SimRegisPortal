namespace SimRegisPortal.Application.Models.Entities;

public abstract class BaseEntityDto
{
    public bool IsNew { get; set; } = false;
}

public abstract class BaseEntityDto<TKey> : BaseEntityDto
{
    public TKey? Id { get; set; } = default!;
}
