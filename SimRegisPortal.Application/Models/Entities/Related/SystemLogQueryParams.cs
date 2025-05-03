namespace SimRegisPortal.Application.Models.Entities.Related;

public sealed class SystemLogQueryParams
{
    public DateTime? DateFrom { get; set; }
    public DateTime? DateTo { get; set; }

    public SystemLogQueryParams()
    {
        DateFrom = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
    }
}
