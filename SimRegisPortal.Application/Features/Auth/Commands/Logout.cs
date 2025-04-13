using MediatR;
using Microsoft.EntityFrameworkCore;
using SimRegisPortal.Application.Context;
using SimRegisPortal.Persistence.Context;

namespace SimRegisPortal.Application.Features.Auth.Commands;

public sealed record LogoutCommand
    : IRequest;

internal sealed class LogoutHandler(
        AppDbContext DbContext,
        IUserContext UserContext)
    : IRequestHandler<LogoutCommand>
{
    public async Task Handle(LogoutCommand command, CancellationToken cancellationToken)
    {
        var userSession = await DbContext.UserSessions
                .FirstOrDefaultAsync(s => s.Id == UserContext.UserSessionId, cancellationToken);

        if (userSession is { IsActive: true })
        {
            userSession.FinishSession();
        }

        await DbContext.SaveChangesAsync(cancellationToken);
    }
}
