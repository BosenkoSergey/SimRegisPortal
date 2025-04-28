using AutoMapper;
using SimRegisPortal.Application.Features.Base.Queries;
using SimRegisPortal.Application.Models.Entities;
using SimRegisPortal.Core.Entities;
using SimRegisPortal.Persistence.Context;

namespace SimRegisPortal.Application.Features.Employees.Queries;

public sealed record GetEmployeesQuery
    : GetManyQuery<EmployeeDto>;

internal sealed class GetEmployeesHandler(AppDbContext dbContext, IMapper mapper)
    : GetManyHandler<GetEmployeesQuery, Employee, EmployeeDto>(dbContext, mapper);