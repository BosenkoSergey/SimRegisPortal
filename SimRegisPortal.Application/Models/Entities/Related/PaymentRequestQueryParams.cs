using SimRegisPortal.Core.Enums;

namespace SimRegisPortal.Application.Models.Entities.Related;

public sealed class PaymentRequestQueryParams
{
    public int? Year { get; set; }
    public Month? Month { get; set; }
    public Guid? EmployeeId { get; set; }
    public PaymentRequestType? Type { get; set; }

    public PaymentRequestQueryParams()
    {
        Year = DateTime.UtcNow.Year;
        Month = (Month)DateTime.UtcNow.Month;
    }
}
