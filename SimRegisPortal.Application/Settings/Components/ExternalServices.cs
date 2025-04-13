namespace SimRegisPortal.Application.Settings.Components;

public record ExternalServices(
    SendGrid SendGrid
);

public record SendGrid(
    string ApiKey,
    string SenderEmail,
    string SenderName
);
