using AutoMapper;
using SimRegisPortal.Application.Features.Base.Queries;
using SimRegisPortal.Application.Models.Entities;
using SimRegisPortal.Core.Entities;
using SimRegisPortal.Persistence.Context;

namespace SimRegisPortal.Application.Features.EmployeeActivities.Queries;

public sealed record GetEmployeeActivitiesQuery
    : GetManyQuery<EmployeeActivityDto>;

internal sealed class GetEmployeeActivitiesHandler(AppDbContext dbContext, IMapper mapper)
    : GetManyHandler<GetEmployeeActivitiesQuery, EmployeeActivity, EmployeeActivityDto>(dbContext, mapper);
