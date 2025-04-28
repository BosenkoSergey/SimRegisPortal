using AutoMapper;
using SimRegisPortal.Application.Features.Base.Queries;
using SimRegisPortal.Application.Models.Entities;
using SimRegisPortal.Core.Entities;
using SimRegisPortal.Persistence.Context;

namespace SimRegisPortal.Application.Features.PaymentRequests.Queries;

public sealed record GetPaymentRequestQuery(Guid Id)
    : GetByIdQuery<Guid, PaymentRequestDto>(Id);

internal sealed class GetPaymentRequestHandler(AppDbContext dbContext, IMapper mapper)
    : GetByIdHandler<GetPaymentRequestQuery, PaymentRequest, Guid, PaymentRequestDto>(dbContext, mapper);