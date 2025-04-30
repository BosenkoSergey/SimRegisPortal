using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SimRegisPortal.Application.Context;
using SimRegisPortal.Application.Models.Auth;
using SimRegisPortal.Core.Enums;
using SimRegisPortal.Core.Exceptions;
using SimRegisPortal.Core.Helpers;
using SimRegisPortal.Persistence.Context;

namespace SimRegisPortal.Application.Features.Users.Commands;

public sealed record LoginCommand(LoginRequest Request)
    : IRequest;

internal sealed class LoginHandler(AppDbContext DbContext, IMapper Mapper, IUserContext UserContext)
    : IRequestHandler<LoginCommand>
{
    public async Task Handle(LoginCommand command, CancellationToken cancellationToken)
    {
        var login = command.Request.Login.Trim();
        var user = await DbContext.Users
                .Include(u => u.Permissions)
                .Where(u => u.Status == UserStatus.Active)
                .Where(u => u.Login == login || u.Email == login)
                .FirstOrDefaultAsync(cancellationToken)
            ?? throw new CommonException("Validation.Login.InvalidCredentials");

        if (!PasswordHelper.Verify(command.Request.Password, user.PasswordHash))
        {
            throw new CommonException("Validation.Login.InvalidCredentials");
        }

        var authResponse = Mapper.Map<AuthResponse>(user);
        await UserContext.SignInAsync(authResponse);
    }
}