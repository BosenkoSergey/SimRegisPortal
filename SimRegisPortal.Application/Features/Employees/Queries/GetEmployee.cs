using AutoMapper;
using SimRegisPortal.Application.Features.Base.Queries;
using SimRegisPortal.Application.Models.Entities;
using SimRegisPortal.Core.Entities;
using SimRegisPortal.Persistence.Context;

namespace SimRegisPortal.Application.Features.Employees.Queries;

public sealed record GetEmployeeQuery(Guid Id)
    : GetByIdQuery<Guid, EmployeeDto>(Id);

internal sealed class GetEmployeeHandler(AppDbContext dbContext, IMapper mapper)
    : GetByIdHandler<GetEmployeeQuery, Employee, Guid, EmployeeDto>(dbContext, mapper);