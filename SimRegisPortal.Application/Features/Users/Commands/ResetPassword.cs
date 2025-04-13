using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SimRegisPortal.Application.Models.Mailing;
using SimRegisPortal.Application.Services.Interfaces;
using SimRegisPortal.Core.Entities;
using SimRegisPortal.Core.Exceptions;
using SimRegisPortal.Core.Helpers;
using SimRegisPortal.Persistence.Context;

namespace SimRegisPortal.Application.Features.Users.Commands;

public sealed record ResetPasswordCommand(Guid Id)
    : IRequest;

internal sealed class ResetPasswordHandler(AppDbContext DbContext, IEmailService EmailService, IMapper Mapper)
    : IRequestHandler<ResetPasswordCommand>
{
    public async Task Handle(ResetPasswordCommand command, CancellationToken cancellationToken)
    {
        var password = PasswordHelper.Generate();

        var user = await GetEntity(command.Id, cancellationToken);
        user.PasswordHash = PasswordHelper.GetHash(password);

        await DbContext.SaveChangesAsync(cancellationToken);

        var message = Mapper.Map<UserCredentialsEmailDto>(user, opt =>
            opt.AfterMap((src, dest) =>
            {
                dest.Password = password;
            }));

        await EmailService.SendUserCreatedEmailAsync(message);
    }

    private async Task<User> GetEntity(Guid id, CancellationToken cancellationToken)
    {
        return await DbContext.Users
                .FirstOrDefaultAsync(u => u.Id == id, cancellationToken)
            ?? throw new ResourceNotFoundException(nameof(User));
    }
}