namespace SimRegisPortal.Application.Models.Entities.Related;

public sealed class EmployeeActivityQueryParams
{
    public DateTime? DateFrom { get; set; }
    public DateTime? DateTo { get; set; }
    public Guid? ProjectId { get; set; }
    public Guid? EmployeeId { get; set; }
    public bool IsLockedEmployee { get; set; }

    public EmployeeActivityQueryParams()
    {
        DateFrom = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
    }

    public EmployeeActivityQueryParams(Guid employeeId) : this()
    {
        EmployeeId = employeeId;
        IsLockedEmployee = true;
    }
}
