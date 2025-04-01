using AutoMapper;
using MediatR;
using Microsoft.Extensions.Options;
using SimRegisPortal.Application.Features.Auth.Commands.Requests;
using SimRegisPortal.Application.Models.Auth.Responses;
using SimRegisPortal.Application.Services.Interfaces;
using SimRegisPortal.Core.AppSettings;
using SimRegisPortal.Core.Exceptions;
using SimRegisPortal.Domain.Entities;
using SimRegisPortal.Persistence.Context;
using SimRegisPortal.Persistence.Extensions;

namespace SimRegisPortal.Application.Features.Auth.Commands.Handlers
{
    internal sealed class LoginHandler
        : IRequestHandler<LoginCommand, AuthResponse>
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IAccessTokenService _accessTokenService;
        private readonly int _refreshTokenExpirationDays;

        public LoginHandler(
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

        public async Task<AuthResponse> Handle(LoginCommand command, CancellationToken cancellationToken)
        {
            var login = command.Request.Login.Trim();
            var user = await _dbContext.Users.GetForAuth(u => u.Login == login || u.Email == login)
                ?? throw new CommonException("Validation.Login.InvalidCredentials");

            if (!BCrypt.Net.BCrypt.Verify(command.Request.Password, user.PasswordHash))
            {
                throw new CommonException("Validation.Login.InvalidCredentials");
            }

            var userSession = new UserSession(user.Id, _refreshTokenExpirationDays);
            await _dbContext.UserSessions.AddAsync(userSession, cancellationToken);

            await _dbContext.SaveChangesAsync(cancellationToken);

            var accessToken = _accessTokenService.GenerateToken(userSession);

            return _mapper.Map<AuthResponse>(userSession, opt =>
                opt.AfterMap((src, dest) =>
                {
                    dest.AccessToken = accessToken;
                }));
        }
    }
}
