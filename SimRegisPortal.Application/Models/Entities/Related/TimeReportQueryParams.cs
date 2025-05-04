using SimRegisPortal.Core.Enums;

namespace SimRegisPortal.Application.Models.Entities.Related;

public sealed class TimeReportQueryParams
{
    public int? Year { get; set; }
    public Month? Month { get; set; }
    public Guid? EmployeeId { get; set; }
    public TimeReportStatus? Status { get; set; }
    public bool IsLockedEmployee { get; set; }

    public TimeReportQueryParams()
    {
        Year = DateTime.UtcNow.Year;
        Month = (Month)DateTime.UtcNow.Month;
    }

    public TimeReportQueryParams(Guid employeeId) : this()
    {
        EmployeeId = employeeId;
        IsLockedEmployee = true;
    }
}
