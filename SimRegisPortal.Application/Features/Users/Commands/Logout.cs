using MediatR;
using SimRegisPortal.Application.Context;

namespace SimRegisPortal.Application.Features.Users.Commands;

public sealed record LogoutCommand
    : IRequest;

internal sealed class LogoutHandler(IUserContext UserContext)
    : IRequestHandler<LogoutCommand>
{
    public async Task Handle(LogoutCommand command, CancellationToken cancellationToken)
    {
        await UserContext.SignOutAsync();
    }
}