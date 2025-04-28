using AutoMapper;
using SimRegisPortal.Application.Features.Base.Queries;
using SimRegisPortal.Application.Models.Entities;
using SimRegisPortal.Core.Entities;
using SimRegisPortal.Persistence.Context;

namespace SimRegisPortal.Application.Features.Companies.Queries;

public sealed record GetCompaniesQuery
    : GetManyQuery<CompanyDto>;

internal sealed class GetCompaniesHandler(AppDbContext dbContext, IMapper mapper)
    : GetManyHandler<GetCompaniesQuery, Company, CompanyDto>(dbContext, mapper);
