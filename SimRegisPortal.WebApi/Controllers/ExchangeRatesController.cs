using MediatR;
using Microsoft.AspNetCore.Mvc;
using SimRegisPortal.Application.Features.Auth.Commands;
using SimRegisPortal.Application.Models.ExchangeRate;
using SimRegisPortal.WebApi.Controllers.Common;

namespace SimRegisPortal.WebApi.Controllers;

[Route("api/exchange-rates")]
public class ExchangeRatesController : BaseApiController
{
    public ExchangeRatesController(IMediator mediator) : base(mediator) { }

    [HttpPost("import")]
    public async Task<IActionResult> ChangeOwnPassword(
        [FromBody] ImportExchangeRatesRequest request,
        CancellationToken cancellationToken)
    {
        await Mediator.Send(new ImportExchangeRatesCommand(request), cancellationToken);
        return NoContent();
    }
}
