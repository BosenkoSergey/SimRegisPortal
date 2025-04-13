namespace SimRegisPortal.Application.Settings.Components;

public record AuthSettings(
    AuthCodeSettings AuthCode,
    AccessTokenSettings AccessToken,
    RefreshTokenSettings RefreshToken
);

public record AuthCodeSettings(
    int BlockResetSeconds,
    int MaxAttempts,
    int ExpirationMinutes);

public record AccessTokenSettings(
    string SecretKey,
    string? Issuer,
    string? Audience,
    int ExpirationMinutes);

public record RefreshTokenSettings(
    int ExpirationDays);
