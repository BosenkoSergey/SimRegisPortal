using SimRegisPortal.Core.Enums;

namespace SimRegisPortal.Core.Entities;

public sealed class PaymentRequest : BaseEntity<Guid>
{
    public Guid TimeReportId { get; set; }
    public PaymentRequestType Type { get; set; }
    public decimal Amount { get; set; }
    public int CurrencyId { get; set; }
    public bool IsPaid { get; set; }

    public TimeReport TimeReport { get; set; } = default!;
    public Currency Currency { get; set; } = default!;
}
