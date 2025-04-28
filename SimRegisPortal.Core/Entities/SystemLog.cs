namespace SimRegisPortal.Core.Entities;

public sealed class SystemLog : BaseEntity<long>
{
    public DateTime TimeStamp { get; private set; } = default!;
    public string Level { get; private set; } = default!;
    public string Message { get; private set; } = default!;
    public string? MessageTemplate { get; private set; } = default!;
    public string? Exception { get; private set; } = default!;
    public string? Properties { get; private set; } = default!;

    private SystemLog() { }
}
