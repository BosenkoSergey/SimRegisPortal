namespace SimRegisPortal.Application.Models.Banking;

public sealed class PrivatBankRateDto
{
    public string BaseCurrency { get; set; } = null!;
    public string Currency { get; set; } = null!;
    public decimal SaleRateNB { get; set; }
    public decimal PurchaseRateNB { get; set; }
}
