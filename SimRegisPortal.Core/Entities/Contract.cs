using SimRegisPortal.Core.Entities.Base;

namespace SimRegisPortal.Core.Entities;

public sealed class Contract : BaseEntity<Guid>
{
    public Guid ProjectId { get; set; }
    public Guid EmployeeId { get; set; }
    public string Number { get; set; } = null!;
    public string? Notes { get; set; }
    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }

    public Project Project { get; set; } = default!;
    public Employee Employee { get; set; } = default!;
}
