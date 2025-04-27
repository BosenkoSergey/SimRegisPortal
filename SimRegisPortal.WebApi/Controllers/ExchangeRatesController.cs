using MediatR;
using Microsoft.AspNetCore.Mvc;
using SimRegisPortal.Application.Features.ExchangeRates.Commands;
using SimRegisPortal.Application.Features.ExchangeRates.Queries;
using SimRegisPortal.Application.Models.ExchangeRate;
using SimRegisPortal.WebApi.Controllers.Common;

namespace SimRegisPortal.WebApi.Controllers;

[Route("api/exchange-rates")]
public class ExchangeRatesController(IMediator mediator) : BaseApiController(mediator)
{
    [HttpGet]
    public async Task<IActionResult> GetExchangeRates(
        [FromQuery] ExchangeRateQueryParams queryParams,
        CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new GetExchangeRatesQuery(queryParams), cancellationToken);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> AddExchangeRate(
        [FromBody] ExchangeRateDto request,
        CancellationToken cancellationToken)
    {
        var response = await Mediator.Send(new AddExchangeRateCommand(request), cancellationToken);
        return Ok(response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> EditExchangeRate(
        int id,
        [FromBody] ExchangeRateDto request,
        CancellationToken cancellationToken)
    {
        var response = await Mediator.Send(new EditExchangeRateCommand(id, request), cancellationToken);
        return Ok(response);
    }

    [HttpPost("import")]
    public async Task<IActionResult> ImportExchangeRates(
        [FromBody] ExchangeRatesImportRequest request,
        CancellationToken cancellationToken)
    {
        await Mediator.Send(new ImportExchangeRatesCommand(request), cancellationToken);
        return NoContent();
    }
}
