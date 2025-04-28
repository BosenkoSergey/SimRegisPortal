using MediatR;
using Microsoft.EntityFrameworkCore;
using SimRegisPortal.Application.Models.Entities.Related;
using SimRegisPortal.Core.Entities;
using SimRegisPortal.Core.Exceptions;
using SimRegisPortal.Core.Helpers;
using SimRegisPortal.Persistence.Context;

namespace SimRegisPortal.Application.Features.Users.Commands;

public sealed record ChangePasswordCommand(Guid Id, UserPasswordRequest Request)
    : IRequest;

internal sealed class ChangePasswordHandler(AppDbContext DbContext)
    : IRequestHandler<ChangePasswordCommand>
{
    public async Task Handle(ChangePasswordCommand command, CancellationToken cancellationToken)
    {
        var user = await GetEntity(command, cancellationToken);
        user.PasswordHash = PasswordHelper.GetHash(command.Request.Password);

        await DbContext.SaveChangesAsync(cancellationToken);
    }

    private async Task<User> GetEntity(ChangePasswordCommand command, CancellationToken cancellationToken)
    {
        return await DbContext.Users
                .FirstOrDefaultAsync(u => u.Id == command.Id, cancellationToken)
            ?? throw new ResourceNotFoundException(nameof(User));
    }
}