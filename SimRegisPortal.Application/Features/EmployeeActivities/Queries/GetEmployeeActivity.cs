using AutoMapper;
using SimRegisPortal.Application.Features.Base.Queries;
using SimRegisPortal.Application.Models.Entities;
using SimRegisPortal.Core.Entities;
using SimRegisPortal.Persistence.Context;

namespace SimRegisPortal.Application.Features.EmployeeActivities.Queries;

public sealed record GetEmployeeActivityQuery(Guid Id)
    : GetByIdQuery<Guid, EmployeeActivityDto>(Id);

internal sealed class GetEmployeeActivityHandler(AppDbContext dbContext, IMapper mapper)
    : GetByIdHandler<GetEmployeeActivityQuery, EmployeeActivity, Guid, EmployeeActivityDto>(dbContext, mapper);