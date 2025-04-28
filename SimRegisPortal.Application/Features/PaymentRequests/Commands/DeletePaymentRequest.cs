using SimRegisPortal.Application.Features.Base.Commands;
using SimRegisPortal.Core.Entities;
using SimRegisPortal.Persistence.Context;

namespace SimRegisPortal.Application.Features.PaymentRequests.Commands;

public sealed record DeletePaymentRequestCommand(Guid Id)
    : DeleteCommand<Guid>(Id);

internal sealed class DeletePaymentRequestHandler(AppDbContext dbContext)
    : DeleteHandler<DeletePaymentRequestCommand, PaymentRequest, Guid>(dbContext);