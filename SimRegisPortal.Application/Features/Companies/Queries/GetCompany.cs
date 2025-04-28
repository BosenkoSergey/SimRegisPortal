using AutoMapper;
using SimRegisPortal.Application.Features.Base.Queries;
using SimRegisPortal.Application.Models.Entities;
using SimRegisPortal.Core.Entities;
using SimRegisPortal.Persistence.Context;

namespace SimRegisPortal.Application.Features.Companies.Queries;

public sealed record GetCompanyQuery(Guid Id)
    : GetByIdQuery<Guid, CompanyDto>(Id);

internal sealed class GetCompanyHandler(AppDbContext dbContext, IMapper mapper)
    : GetByIdHandler<GetCompanyQuery, Company, Guid, CompanyDto>(dbContext, mapper);