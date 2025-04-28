using AutoMapper;
using SimRegisPortal.Application.Features.Base.Commands;
using SimRegisPortal.Application.Models.Company;
using SimRegisPortal.Core.Entities;
using SimRegisPortal.Persistence.Context;

namespace SimRegisPortal.Application.Features.Companies.Commands;

public sealed record SaveCompanyCommand(CompanyDto Dto)
    : SaveCommand<CompanyDto, Guid>(Dto);

internal sealed class SaveCompanyHandler(AppDbContext dbContext, IMapper mapper)
    : SaveHandler<SaveCompanyCommand, CompanyDto, Company, Guid>(dbContext, mapper);