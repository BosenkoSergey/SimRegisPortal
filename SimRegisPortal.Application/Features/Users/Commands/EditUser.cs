using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SimRegisPortal.Application.Features.Base.Commands;
using SimRegisPortal.Application.Models.Users;
using SimRegisPortal.Core.Entities;
using SimRegisPortal.Core.Exceptions;
using SimRegisPortal.Persistence.Context;

namespace SimRegisPortal.Application.Features.Users.Commands;

public sealed record EditUserCommand(Guid Id, UserRequest Request)
    : EditCommand<Guid, UserRequest, UserResponse>(Id, Request);

internal sealed class EditUserHandler(AppDbContext dbContext, IMapper mapper)
    : EditHandler<EditUserCommand, UserRequest, User, Guid, UserResponse>(dbContext, mapper)
{
    protected override async Task<User> GetEntity(EditUserCommand command, CancellationToken cancellationToken)
    {
        // TODO: GetEntityQuery
        return await DbSet
                .Include(u => u.Permissions)
                .FirstOrDefaultAsync(u => u.Id == command.Id, cancellationToken)
            ?? throw new ResourceNotFoundException(nameof(User));
    }
}