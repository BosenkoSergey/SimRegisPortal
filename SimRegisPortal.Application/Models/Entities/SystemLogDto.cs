namespace SimRegisPortal.Application.Models.Entities;

public sealed class SystemLogDto : BaseEntityDto<long>
{
    public DateTime? TimeStamp { get; private set; } = default!;
    public string Level { get; private set; } = default!;
    public string Message { get; private set; } = default!;
    public string? Exception { get; private set; } = default!;
    public string? Properties { get; private set; } = default!;
}
