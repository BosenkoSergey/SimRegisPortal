using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SimRegisPortal.Application.Features.Base.Commands;
using SimRegisPortal.Application.Models.User;
using SimRegisPortal.Core.Entities;
using SimRegisPortal.Persistence.Context;

namespace SimRegisPortal.Application.Features.Users.Commands;

public sealed record EditUserCommand(Guid Id, UserRequest Request)
    : EditCommand<Guid, UserRequest, UserResponse>(Id, Request);

internal sealed class EditUserHandler(AppDbContext dbContext, IMapper mapper)
    : EditHandler<EditUserCommand, UserRequest, User, Guid, UserResponse>(dbContext, mapper)
{
    protected override IQueryable<User> GetEntityQuery()
    {
        return DbSet
            .Include(u => u.Permissions);
    }
}