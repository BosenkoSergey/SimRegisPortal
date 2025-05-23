﻿using System.Globalization;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using SimRegisPortal.Application.Constants;
using SimRegisPortal.Core.Enums;

namespace SimRegisPortal.Application.Context;

public class UserContext : IUserContext
{
    private readonly AuthenticationStateProvider _authStateProvider;
    private bool _initialized;
    private ClaimsPrincipal? _user;

    public bool IsAuthenticated { get; private set; }
    public bool IsAdmin { get; private set; }
    public Guid UserId { get; private set; }
    public Guid? EmployeeId { get; private set; }
    public string UserName { get; private set; }
    public HashSet<UserPermissionType> Permissions { get; private set; } = [];

    public UserContext(AuthenticationStateProvider authStateProvider)
    {
        _authStateProvider = authStateProvider;
    }

    public async Task InitializeAsync()
    {
        if (_initialized) return;

        var authState = await _authStateProvider.GetAuthenticationStateAsync();
        _user = authState.User;
        _initialized = true;

        IsAuthenticated = _user?.Identity?.IsAuthenticated ?? false;

        if (IsAuthenticated)
        {
            IsAdmin = GetClaimValue<bool>(CustomClaimTypes.IsAdmin);
            UserId = GetClaimValue<Guid>(CustomClaimTypes.UserId);
            UserName = GetClaimValue<string>(CustomClaimTypes.UserName);
            EmployeeId = GetClaimNullableValue<Guid>(CustomClaimTypes.EmployeeId);
            Permissions = GetClaimHashSet<UserPermissionType>(CustomClaimTypes.Permissions, Separators.UserPermissions);
        }
        else
        {
            IsAdmin = false;
            UserId = Guid.Empty;
            UserName = string.Empty;
            EmployeeId = null;
            Permissions = [];
        }
    }

    public bool HasPermission(UserPermissionType permission)
    {
        return HasAnyPermission(permission);
    }

    public bool HasAnyPermission(params UserPermissionType[] requiredPermissions)
    {
        return IsAdmin || Permissions.Overlaps(requiredPermissions);
    }

    private T? GetClaimNullableValue<T>(string claimType)
        where T : struct
    {
        try
        {
            return GetClaimValue<T>(claimType);
        }
        catch (Exception)
        {
            return null;
        }
    }

    private T GetClaimValue<T>(string claimType)
    {
        var claimValue = _user?.FindFirstValue(claimType);
        return ParseClaimPart<T>(claimValue);
    }

    private HashSet<T> GetClaimHashSet<T>(string claimType, char separator)
    {
        var value = GetClaimValue<string>(claimType);

        if (string.IsNullOrWhiteSpace(value))
        {
            return [];
        }

        return value
            .Split(separator, StringSplitOptions.RemoveEmptyEntries)
            .Select(ParseClaimPart<T>)
            .ToHashSet();
    }

    private static T ParseClaimPart<T>(string? claimPart)
    {
        if (typeof(T) == typeof(Guid))
        {
            if (Guid.TryParse(claimPart, out var guidValue))
            {
                return (T)(object)guidValue;
            }
            throw new InvalidCastException($"Invalid GUID value '{claimPart}' for claim '{claimPart}'.");
        }

        if (typeof(T).IsEnum)
        {
            if (Enum.TryParse(typeof(T), claimPart, true, out var enumValue))
            {
                return (T)enumValue;
            }
            throw new InvalidCastException($"Invalid enum value '{claimPart}' for claim '{claimPart}'.");
        }

        try
        {
            return (T)Convert.ChangeType(claimPart!, typeof(T), CultureInfo.InvariantCulture);
        }
        catch (Exception ex)
        {
            throw new InvalidCastException($"Failed to convert claim '{claimPart}' value '{claimPart}' to type {typeof(T)}.", ex);
        }
    }
}
