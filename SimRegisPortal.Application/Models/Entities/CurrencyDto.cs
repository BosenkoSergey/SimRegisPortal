namespace SimRegisPortal.Application.Models.Entities;

public sealed class CurrencyDto : BaseEntityDto<int>
{
    public string Code { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Symbol { get; set; } = null!;

    public string DisplayName => $"{Code} ({Name})";
}
