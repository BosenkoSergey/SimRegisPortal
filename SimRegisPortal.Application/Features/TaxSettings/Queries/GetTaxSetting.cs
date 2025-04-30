using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SimRegisPortal.Application.Features.Base.Queries;
using SimRegisPortal.Application.Models.Entities;
using SimRegisPortal.Core.Entities;
using SimRegisPortal.Core.Exceptions;
using SimRegisPortal.Persistence.Context;

namespace SimRegisPortal.Application.Features.TaxSettings.Queries;

public sealed record GetTaxSettingQuery()
    : GetOneQuery<TaxSettingDto>();

internal sealed class GetTaxSettingHandler(AppDbContext dbContext, IMapper mapper)
    : GetOneHandler<GetTaxSettingQuery, TaxSetting, TaxSettingDto>(dbContext, mapper)
{
    protected override async Task<TaxSetting> GetEntity(GetTaxSettingQuery query, CancellationToken cancellationToken)
    {
        return await GetEntitiesQuery()
                .FirstOrDefaultAsync(cancellationToken)
            ?? throw new ResourceNotFoundException(nameof(TaxSetting));
    }
}