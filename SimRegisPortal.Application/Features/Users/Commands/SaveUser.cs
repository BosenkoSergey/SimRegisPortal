using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SimRegisPortal.Application.Features.Base.Commands;
using SimRegisPortal.Application.Models.Entities;
using SimRegisPortal.Core.Entities;
using SimRegisPortal.Persistence.Context;

namespace SimRegisPortal.Application.Features.Users.Commands;

public sealed record SaveUserCommand(UserDto Dto)
    : SaveCommand<UserDto, Guid>(Dto);

internal sealed class SaveUserHandler(AppDbContext dbContext, IMapper mapper)
    : SaveHandler<SaveUserCommand, UserDto, User, Guid>(dbContext, mapper)
{
    protected override IQueryable<User> GetEntityQuery(SaveUserCommand command)
    {
        return DbSet
            .Include(u => u.Permissions);
    }
}