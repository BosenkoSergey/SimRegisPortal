using SimRegisPortal.Core.Enums;

namespace SimRegisPortal.Application.Models.Entities;

public sealed class PaymentRequestDto : BaseEntityDto<Guid>
{
    public PaymentRequestType Type { get; set; }
    public decimal Amount { get; set; }
    public int CurrencyId { get; set; }
    public bool IsPaid { get; set; }
}
