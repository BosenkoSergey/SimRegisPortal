using MediatR;
using Microsoft.AspNetCore.Mvc;
using SimRegisPortal.Application.Features.ExchangeRates.Commands;
using SimRegisPortal.Application.Features.ExchangeRates.Queries;
using SimRegisPortal.Application.Models.Entities;
using SimRegisPortal.Application.Models.Entities.Related;
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
    public async Task<IActionResult> SaveExchangeRate(
        [FromBody] ExchangeRateDto request,
        CancellationToken cancellationToken)
    {
        var response = await Mediator.Send(new SaveExchangeRateCommand(request), cancellationToken);
        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteExchangeRate(
        int id,
        CancellationToken cancellationToken)
    {
        await Mediator.Send(new DeleteExchangeRateCommand(id), cancellationToken);
        return NoContent();
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
