namespace SimRegisPortal.Application.Models.Entities.Related;

public sealed class EmployeeActivityQueryParams
{
    public Guid? TimeReportId { get; set; }
    public DateTime? DateFrom { get; set; }
    public DateTime? DateTo { get; set; }
    public Guid? ProjectId { get; set; }
    public Guid? EmployeeId { get; set; }
    public bool IsLockedEmployee { get; set; }

    public EmployeeActivityQueryParams()
    {
        DateFrom = new DateTime(DateTime.Now.Year, 1, 1);
    }

    public EmployeeActivityQueryParams(Guid timeReportId)
    {
        TimeReportId = timeReportId;
    }

    public void LockEmployee(Guid employeeId)
    {
        EmployeeId = employeeId;
        IsLockedEmployee = true;
    }
}
