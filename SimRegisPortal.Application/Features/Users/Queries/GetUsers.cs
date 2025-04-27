using AutoMapper;
using SimRegisPortal.Application.Features.Base.Queries;
using SimRegisPortal.Application.Models.User;
using SimRegisPortal.Core.Entities;
using SimRegisPortal.Persistence.Context;

namespace SimRegisPortal.Application.Features.Users.Queries;

public sealed record GetUsersQuery
    : GetManyQuery<UserDto>;

internal sealed class GetUsersHandler(AppDbContext dbContext, IMapper mapper)
    : GetManyHandler<GetUsersQuery, User, UserDto>(dbContext, mapper);
