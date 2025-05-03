using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SimRegisPortal.Application.Features.Base.Queries;
using SimRegisPortal.Application.Models.Entities;
using SimRegisPortal.Core.Entities;
using SimRegisPortal.Persistence.Context;

namespace SimRegisPortal.Application.Features.Users.Queries;

public sealed record GetUsersQuery
    : GetManyQuery<UserDto>;

internal sealed class GetUsersHandler(AppDbContext dbContext, IMapper mapper)
    : GetManyHandler<GetUsersQuery, User, UserDto>(dbContext, mapper)
{
    protected override IQueryable<User> GetEntitiesQuery()
    {
        return Repository
            .Include(u => u.Permissions)
            .OrderBy(u => u.FullName);
    }
}
