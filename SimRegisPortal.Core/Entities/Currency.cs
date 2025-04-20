using SimRegisPortal.Core.Entities.Base;

namespace SimRegisPortal.Core.Entities;

public sealed class Currency : BaseEntity<int>
{
    public string Code { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Symbol { get; set; } = null!;
}
