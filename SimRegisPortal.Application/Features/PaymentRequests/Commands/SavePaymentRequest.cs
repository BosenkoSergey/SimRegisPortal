using AutoMapper;
using SimRegisPortal.Application.Features.Base.Commands;
using SimRegisPortal.Application.Models.Entities;
using SimRegisPortal.Core.Entities;
using SimRegisPortal.Persistence.Context;

namespace SimRegisPortal.Application.Features.PaymentRequests.Commands;

public sealed record SavePaymentRequestCommand(PaymentRequestDto Dto)
    : SaveCommand<PaymentRequestDto, Guid>(Dto);

internal sealed class SavePaymentRequestHandler(AppDbContext dbContext, IMapper mapper)
    : SaveHandler<SavePaymentRequestCommand, PaymentRequestDto, PaymentRequest, Guid>(dbContext, mapper)
{
    protected override Task<PaymentRequest> CreateEntity(SavePaymentRequestCommand command)
    {
        throw new NotSupportedException();
    }
}