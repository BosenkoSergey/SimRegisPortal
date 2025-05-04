using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SimRegisPortal.Application.Features.Base.Queries;
using SimRegisPortal.Application.Models.Entities;
using SimRegisPortal.Application.Models.Entities.Related;
using SimRegisPortal.Core.Entities;
using SimRegisPortal.Persistence.Context;

namespace SimRegisPortal.Application.Features.PaymentRequests.Queries;

public sealed record GetPaymentRequestsQuery(PaymentRequestQueryParams QueryParams)
    : GetManyQuery<PaymentRequestDto>;

internal sealed class GetPaymentRequestsHandler(AppDbContext dbContext, IMapper mapper)
    : GetManyHandler<GetPaymentRequestsQuery, PaymentRequest, PaymentRequestDto>(dbContext, mapper)
{
    protected override async Task<IEnumerable<PaymentRequest>> GetEntities(GetPaymentRequestsQuery query, CancellationToken cancellationToken)
    {
        var entitiesQuery = GetEntitiesQuery();

        if (query.QueryParams.TimeReportId.HasValue)
        {
            entitiesQuery = entitiesQuery.Where(r =>
                r.TimeReportId == query.QueryParams.TimeReportId.Value);
        }
        if (query.QueryParams.Year.HasValue)
        {
            entitiesQuery = entitiesQuery.Where(r =>
                r.TimeReport.Year >= query.QueryParams.Year.Value);
        }
        if (query.QueryParams.Month.HasValue)
        {
            entitiesQuery = entitiesQuery.Where(r =>
                r.TimeReport.Month <= query.QueryParams.Month.Value);
        }
        if (query.QueryParams.EmployeeId.HasValue)
        {
            entitiesQuery = entitiesQuery.Where(r =>
                r.TimeReport.EmployeeId == query.QueryParams.EmployeeId.Value);
        }
        if (query.QueryParams.Type.HasValue)
        {
            entitiesQuery = entitiesQuery.Where(r =>
                r.Type == query.QueryParams.Type.Value);
        }
        if (query.QueryParams.IsPaid.HasValue)
        {
            entitiesQuery = entitiesQuery.Where(r =>
                r.IsPaid == query.QueryParams.IsPaid.Value);
        }

        return await entitiesQuery
            .OrderByDescending(r => r.TimeReport.Year)
                .ThenByDescending(r => r.TimeReport.Month)
                .ThenByDescending(r => r.TimeReport.EmployeeId)
                .ThenBy(r => r.Type)
            .ToListAsync(cancellationToken);
    }

    protected override IQueryable<PaymentRequest> GetEntitiesQuery()
    {
        return Repository
            .Include(pr => pr.TimeReport).ThenInclude(tr => tr.Employee);
    }
}
