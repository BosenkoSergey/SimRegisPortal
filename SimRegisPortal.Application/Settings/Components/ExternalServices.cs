namespace SimRegisPortal.Application.Settings.Components;

public record ExternalServices(
    SendGrid SendGrid,
    PrivatBank PrivatBank
);

public record SendGrid(
    string ApiKey,
    string SenderEmail,
    string SenderName
);

public record PrivatBank(
    string ExchangeRatesUrl
);