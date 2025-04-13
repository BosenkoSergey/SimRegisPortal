﻿namespace SimRegisPortal.Application.Models.Mailing.Common;

public record CommonEmailDto
{
    public RecipientDto Recipient { get; init; } = null!;
    public string Subject { get; init; } = null!;
    public string PlainTextContent { get; init; } = null!;
    public string HtmlContent { get; init; } = null!;
}
