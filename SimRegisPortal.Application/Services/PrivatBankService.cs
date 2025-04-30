using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using System.Globalization;
using SimRegisPortal.Application.Models.Banking;
using SimRegisPortal.Application.Services.Interfaces;
using SimRegisPortal.Application.Settings;

namespace SimRegisPortal.Application.Services;

public sealed class PrivatBankService : IPrivatBankService
{
    private readonly HttpClient _httpClient;
    private readonly AppSettings _appSettings;

    public PrivatBankService(HttpClient httpClient, IOptions<AppSettings> options)
    {
        _httpClient = httpClient;
        _appSettings = options.Value;
    }

    public async Task<IEnumerable<PrivatBankRateDto>> GetRatesAsync(DateTime date, CancellationToken ct = default)
    {
        var url = _appSettings.ExternalServices.PrivatBank.ExchangeRatesUrl
            + date.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture);
        var json = await _httpClient.GetStringAsync(url, ct);

        var parsed = JObject.Parse(json);
        var items = parsed["exchangeRate"]?.ToObject<List<PrivatBankRateDto>>() ?? [];

        return items
            .Where(x => x.Currency is not null && x.SaleRateNB > 0)
            .ToList();
    }
}
