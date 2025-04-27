using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SimRegisPortal.Application.Features.Base.Commands;
using SimRegisPortal.Application.Models.User;
using SimRegisPortal.Core.Entities;
using SimRegisPortal.Persistence.Context;

namespace SimRegisPortal.Application.Features.Users.Commands;

public sealed record EditUserCommand(Guid Id, UserDto Request)
    : EditCommand<Guid, UserDto, UserDto>(Id, Request);

internal sealed class EditUserHandler(AppDbContext dbContext, IMapper mapper)
    : EditHandler<EditUserCommand, UserDto, User, Guid, UserDto>(dbContext, mapper)
{
    protected override IQueryable<User> GetEntityQuery()
    {
        return DbSet
            .Include(u => u.Permissions);
    }
}