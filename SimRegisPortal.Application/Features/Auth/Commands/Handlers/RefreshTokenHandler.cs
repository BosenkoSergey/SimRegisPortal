using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SimRegisPortal.Application.Features.Auth.Commands.Requests;
using SimRegisPortal.Application.Models.Auth.Responses;
using SimRegisPortal.Application.Services.Interfaces;
using SimRegisPortal.Core.AppSettings;
using SimRegisPortal.Core.Exceptions;
using SimRegisPortal.Persistence.Context;
using SimRegisPortal.Persistence.Extensions;

namespace SimRegisPortal.Application.Features.Auth.Commands.Handlers
{
    internal sealed class RefreshTokenHandler
        : IRequestHandler<RefreshTokenCommand, AuthResponse>
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IAccessTokenService _accessTokenService;
        private readonly int _refreshTokenExpirationDays;

        public RefreshTokenHandler(
            AppDbContext dbContext,
            IMapper mapper,
            IAccessTokenService accessTokenService,
            IOptions<AppSettings> options)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _accessTokenService = accessTokenService;
            _refreshTokenExpirationDays = options.Value.AuthSettings.RefreshToken.ExpirationDays;
        }

        public async Task<AuthResponse> Handle(RefreshTokenCommand command, CancellationToken cancellationToken)
        {
            var userSession = await _dbContext.UserSessions
                .FirstOrDefaultAsync(s => s.RefreshToken == command.Request.RefreshToken, cancellationToken);

            if (userSession == null)
            {
                throw new CommonException("Exception.UserSession.NotFound");
            }
            if (!userSession.IsActive)
            {
                throw new SessionExpiredException();
            }

            userSession.Refresh(_refreshTokenExpirationDays);

            await _dbContext.SaveChangesAsync(cancellationToken);

            var user = await _dbContext.Users.GetForAuth(u => u.Id == userSession.UserId, cancellationToken);

            var accessToken = _accessTokenService.GenerateToken(userSession);

            return _mapper.Map<AuthResponse>(userSession, opt =>
                opt.AfterMap((src, dest) =>
                {
                    dest.AccessToken = accessToken;
                }));
        }
    }
}
