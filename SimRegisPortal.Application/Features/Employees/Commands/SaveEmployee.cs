using AutoMapper;
using SimRegisPortal.Application.Features.Base.Commands;
using SimRegisPortal.Application.Models.Entities;
using SimRegisPortal.Core.Entities;
using SimRegisPortal.Persistence.Context;

namespace SimRegisPortal.Application.Features.Employees.Commands;

public sealed record SaveEmployeeCommand(EmployeeDto Dto)
    : SaveCommand<EmployeeDto, Guid>(Dto);

internal sealed class SaveEmployeeHandler(AppDbContext dbContext, IMapper mapper)
    : SaveHandler<SaveEmployeeCommand, EmployeeDto, Employee, Guid>(dbContext, mapper);