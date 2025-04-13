using AutoMapper;
using MediatR;
using Microsoft.Extensions.Options;
using SimRegisPortal.Application.Models.Auth.Requests;
using SimRegisPortal.Application.Models.Auth.Responses;
using SimRegisPortal.Application.Services.Interfaces;
using SimRegisPortal.Core.Entities;
using SimRegisPortal.Core.Exceptions;
using SimRegisPortal.Core.Settings;
using SimRegisPortal.Persistence.Context;
using SimRegisPortal.Persistence.Extensions.DbSetExtensions;

namespace SimRegisPortal.Application.Features.Auth.Commands;

public sealed record LoginCommand(LoginRequest Request)
    : IRequest<AuthResponse>;

internal sealed class LoginHandler(
        AppDbContext DbContext,
        IMapper Mapper,
        IAccessTokenService AccessTokenService,
        IOptions<AppSettings> Options)
    : IRequestHandler<LoginCommand, AuthResponse>
{
    public async Task<AuthResponse> Handle(LoginCommand command, CancellationToken cancellationToken)
    {
        var login = command.Request.Login.Trim();
        var user = await DbContext.Users.GetForAuth(u => u.Login == login || u.Email == login)
            ?? throw new CommonException("Validation.Login.InvalidCredentials");

        if (!BCrypt.Net.BCrypt.Verify(command.Request.Password, user.PasswordHash))
        {
            throw new CommonException("Validation.Login.InvalidCredentials");
        }

        var userSession = new UserSession(user.Id, Options.Value.AuthSettings.RefreshToken.ExpirationDays);
        await DbContext.UserSessions.AddAsync(userSession, cancellationToken);

        await DbContext.SaveChangesAsync(cancellationToken);

        var accessToken = AccessTokenService.GenerateToken(userSession);

        return Mapper.Map<AuthResponse>(userSession, opt =>
            opt.AfterMap((src, dest) =>
            {
                dest.AccessToken = accessToken;
            }));
    }
}
