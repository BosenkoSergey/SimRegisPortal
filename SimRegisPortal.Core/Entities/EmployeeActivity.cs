namespace SimRegisPortal.Core.Entities;

public sealed class EmployeeActivity : BaseEntity<Guid>
{
    public Guid ProjectId { get; set; }
    public Guid EmployeeId { get; set; }
    public Guid? TimeReportId { get; set; }
    public DateTime Date { get; set; }
    public decimal Hours { get; set; }
    public string Description { get; set; } = null!;

    public Project Project { get; set; } = default!;
    public Employee Employee { get; set; } = default!;
    public TimeReport? TimeReport { get; set; }
}