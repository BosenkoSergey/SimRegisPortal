using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SimRegisPortal.Application.Features.Base.Queries;
using SimRegisPortal.Application.Models.Entities;
using SimRegisPortal.Core.Entities;
using SimRegisPortal.Persistence.Context;

namespace SimRegisPortal.Application.Features.Users.Queries;

public sealed record GetUserQuery(Guid Id)
    : GetByIdQuery<Guid, UserDto>(Id);

internal sealed class GetUserHandler(AppDbContext dbContext, IMapper mapper)
    : GetByIdHandler<GetUserQuery, User, Guid, UserDto>(dbContext, mapper)
{
    protected override IQueryable<User> GetEntitiesQuery()
    {
        return Repository
            .Include(u => u.Permissions);
    }
}