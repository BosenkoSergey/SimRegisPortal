﻿namespace SimRegisPortal.Core.AppSettings.Components
{
    public record ExternalServices(
        SendGrid SendGrid
    );

    public record SendGrid(
        string ApiKey,
        string SenderEmail,
        string SenderName
    );
}
