namespace SimRegisPortal.Application.Models.Entities.Related;

public sealed class SystemLogQueryParams
{
    public DateTime? DateFrom { get; set; }
    public DateTime? DateTo { get; set; }

    public SystemLogQueryParams()
    {
        DateFrom = DateTime.UtcNow.AddMonths(-1).Date;
    }
}
