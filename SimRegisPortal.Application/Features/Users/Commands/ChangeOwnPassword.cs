using MediatR;
using Microsoft.EntityFrameworkCore;
using SimRegisPortal.Application.Context;
using SimRegisPortal.Application.Models.Users;
using SimRegisPortal.Core.Entities;
using SimRegisPortal.Core.Exceptions;
using SimRegisPortal.Core.Helpers;
using SimRegisPortal.Persistence.Context;

namespace SimRegisPortal.Application.Features.Users.Commands;

public sealed record ChangeOwnPasswordCommand(UserPasswordRequest Request)
    : IRequest;

internal sealed class ChangeOwnPasswordHandler(AppDbContext DbContext, IUserContext UserContext)
    : IRequestHandler<ChangeOwnPasswordCommand>
{
    public async Task Handle(ChangeOwnPasswordCommand command, CancellationToken cancellationToken)
    {
        var user = await GetEntity(cancellationToken);
        user.PasswordHash = PasswordHelper.GetHash(command.Request.Password);

        await DbContext.SaveChangesAsync(cancellationToken);
    }

    private async Task<User> GetEntity(CancellationToken cancellationToken)
    {
        return await DbContext.Users
                .FirstOrDefaultAsync(u => u.Id == UserContext.UserId, cancellationToken)
            ?? throw new ResourceNotFoundException(nameof(User));
    }
}