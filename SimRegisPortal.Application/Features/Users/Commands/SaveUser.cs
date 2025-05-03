using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SimRegisPortal.Application.Features.Base.Commands;
using SimRegisPortal.Application.Models.Entities;
using SimRegisPortal.Core.Entities;
using SimRegisPortal.Core.Helpers;
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

    protected override async Task<User> CreateEntity(SaveUserCommand command)
    {
        var password = PasswordHelper.Generate();
        var entity = await base.CreateEntity(command);
        entity.PasswordHash = PasswordHelper.GetHash(password);
        return entity;
    }
}