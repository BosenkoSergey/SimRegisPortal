using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SimRegisPortal.Application.Models.Auth.Requests;
using SimRegisPortal.Application.Models.Auth.Responses;
using SimRegisPortal.Application.Services.Interfaces;
using SimRegisPortal.Core.Exceptions;
using SimRegisPortal.Core.Settings;
using SimRegisPortal.Persistence.Context;
using SimRegisPortal.Persistence.Extensions.DbSetExtensions;

namespace SimRegisPortal.Application.Features.Auth.Commands;

public sealed record RefreshTokenCommand(RefreshTokenRequest Request)
    : IRequest<AuthResponse>;

internal sealed class RefreshTokenHandler(
        AppDbContext DbContext,
        IMapper Mapper,
        IAccessTokenService AccessTokenService,
        IOptions<AppSettings> Options)
    : IRequestHandler<RefreshTokenCommand, AuthResponse>
{
    public async Task<AuthResponse> Handle(RefreshTokenCommand command, CancellationToken cancellationToken)
    {
        var userSession = await DbContext.UserSessions
            .FirstOrDefaultAsync(s => s.RefreshToken == command.Request.RefreshToken, cancellationToken);

        if (userSession == null)
        {
            throw new CommonException("Exception.UserSession.NotFound");
        }
        if (!userSession.IsActive)
        {
            throw new SessionExpiredException();
        }

        userSession.Refresh(Options.Value.AuthSettings.RefreshToken.ExpirationDays);

        await DbContext.SaveChangesAsync(cancellationToken);

        var user = await DbContext.Users.GetForAuth(u => u.Id == userSession.UserId, cancellationToken);

        var accessToken = AccessTokenService.GenerateToken(userSession);

        return Mapper.Map<AuthResponse>(userSession, opt =>
            opt.AfterMap((src, dest) =>
            {
                dest.AccessToken = accessToken;
            }));
    }
}
