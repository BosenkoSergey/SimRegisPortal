using MediatR;
using Microsoft.AspNetCore.Mvc;
using SimRegisPortal.Application.Features.Currencies.Queries;
using SimRegisPortal.WebApi.Controllers.Common;

namespace SimRegisPortal.WebApi.Controllers;

[Route("api/currencies")]
public class CurrenciesController(IMediator mediator) : BaseApiController(mediator)
{
    [HttpGet]
    public async Task<IActionResult> GetCurrencies(CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new GetCurrenciesQuery(), cancellationToken);
        return Ok(result);
    }
}
