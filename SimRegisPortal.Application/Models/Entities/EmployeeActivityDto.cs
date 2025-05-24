using SimRegisPortal.Core.Enums;

namespace SimRegisPortal.Application.Models.Entities;

public sealed class EmployeeActivityDto : BaseEntityDto<Guid>
{
    public Guid ProjectId { get; set; }
    public Guid EmployeeId { get; set; }
    public DateTime? Date { get; set; }
    public decimal Hours { get; set; }
    public PriorityType Priority { get; set; }
    public ComplexityType Complexity { get; set; }
    public string Description { get; set; } = null!;
}
