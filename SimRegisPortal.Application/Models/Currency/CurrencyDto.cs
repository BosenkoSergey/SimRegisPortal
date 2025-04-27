using SimRegisPortal.Application.Models.Base;

namespace SimRegisPortal.Application.Models.Currency;

public sealed class CurrencyDto : BaseEntityDto<int>
{
    public string Code { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Symbol { get; set; } = null!;

    public string DisplayName => $"{Code} ({Name})";
}
