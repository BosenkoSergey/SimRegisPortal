using AutoMapper;
using SimRegisPortal.Application.Features.Base.Queries;
using SimRegisPortal.Application.Models.Entities;
using SimRegisPortal.Core.Entities;
using SimRegisPortal.Persistence.Context;

namespace SimRegisPortal.Application.Features.PaymentRequests.Queries;

public sealed record GetPaymentRequestsQuery
    : GetManyQuery<PaymentRequestDto>;

internal sealed class GetPaymentRequestsHandler(AppDbContext dbContext, IMapper mapper)
    : GetManyHandler<GetPaymentRequestsQuery, PaymentRequest, PaymentRequestDto>(dbContext, mapper);
