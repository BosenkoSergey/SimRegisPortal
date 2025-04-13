﻿using SimRegisPortal.Application.Models.Mailing.Common;

namespace SimRegisPortal.Application.Models.Mailing;

public sealed record UserCredentialsEmailDto
{
    public RecipientDto Recipient { get; init; } = null!;
    public string Login { get; init; } = null!;
    public string Password { get; set; } = null!;
}
