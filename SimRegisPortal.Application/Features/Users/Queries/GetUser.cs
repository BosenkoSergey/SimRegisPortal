using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SimRegisPortal.Application.Features.Base.Queries;
using SimRegisPortal.Application.Models.User;
using SimRegisPortal.Core.Entities;
using SimRegisPortal.Persistence.Context;

namespace SimRegisPortal.Application.Features.Users.Queries;

public sealed record GetUserQuery(Guid Id)
    : GetByIdQuery<Guid, UserResponse>(Id);

internal sealed class GetUserHandler(AppDbContext dbContext, IMapper mapper)
    : GetByIdHandler<GetUserQuery, User, Guid, UserResponse>(dbContext, mapper)
{
    protected override IQueryable<User> GetEntitiesQuery()
    {
        return Repository
            .Include(u => u.Permissions);
    }
}