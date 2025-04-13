using System.Security.Cryptography;

namespace SimRegisPortal.Core.Helpers;

public static class PasswordHelper
{
    private const int PasswordLength = 6;
    private const string ValidChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz01234567890123456789";

    public static string Generate()
    {
        string password;
        do
        {
            password = string.Concat(
                Enumerable
                    .Range(0, PasswordLength)
                    .Select(_ => ValidChars[RandomNumberGenerator.GetInt32(ValidChars.Length)]));
        }
        while (!IsStrongEnough(password));

        return password;
    }

    public static string GetHash(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public static bool Verify(string password, string passwordHash)
    {
        return BCrypt.Net.BCrypt.Verify(password, passwordHash);
    }

    public static bool IsValid(string password)
    {
        return password.All(char.IsLetterOrDigit);
    }

    public static bool IsStrongEnough(string password)
    {
        return password.Length >= PasswordLength
            && password.Any(char.IsLower)
            && password.Any(char.IsUpper)
            && password.Any(char.IsDigit);
    }
}