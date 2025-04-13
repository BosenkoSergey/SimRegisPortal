using AutoMapper;
using SimRegisPortal.Application.Features.Base.Commands;
using SimRegisPortal.Application.Models.Users;
using SimRegisPortal.Core.Entities;
using SimRegisPortal.Persistence.Context;

namespace SimRegisPortal.Application.Features.Users.Queries;

public record GetUserQuery(Guid Id)
    : GetByIdQuery<Guid, UserResponse>(Id);

internal abstract class GetByIdHandler
    : GetByIdHandler<GetUserQuery, User, Guid, UserResponse>
{
    protected GetByIdHandler(AppDbContext dbContext, IMapper mapper)
        : base(dbContext, mapper) { }

    // TODO: Include Permissions
}