namespace SimRegisPortal.Application.Models.Currency;

public sealed record CurrencyResponse
{
    public int Id { get; set; }
    public string Code { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Symbol { get; set; } = null!;

    public string DisplayName => $"{Code} ({Name})";
}
