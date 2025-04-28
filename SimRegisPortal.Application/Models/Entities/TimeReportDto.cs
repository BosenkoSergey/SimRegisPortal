using SimRegisPortal.Core.Enums;

namespace SimRegisPortal.Application.Models.Entities;

public sealed class TimeReportDto : BaseEntityDto<Guid>
{
    public Guid EmployeeId { get; set; }
    public int Year { get; set; }
    public int Month { get; set; }
    public TimeReportStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public decimal Hours { get; set; }
}
