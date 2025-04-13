using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimRegisPortal.Application.Features.Auth.Commands;
using SimRegisPortal.Application.Models.Auth;
using SimRegisPortal.WebApi.Controllers.Common;

namespace SimRegisPortal.WebApi.Controllers;

[Route("api/auth")]
public class AuthController : BaseApiController
{
    public AuthController(IMediator mediator) : base(mediator) { }

    [HttpPost("login")]
    public async Task<IActionResult> Login(
        [FromBody] LoginRequest request,
        CancellationToken cancellationToken)
    {
        var response = await Mediator.Send(new LoginCommand(request), cancellationToken);
        return Ok(response);
    }

    [Authorize]
    [HttpPost("logout")]
    public async Task<IActionResult> Logout(CancellationToken cancellationToken)
    {
        await Mediator.Send(new LogoutCommand(), cancellationToken);
        return NoContent();
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshToken(
        [FromBody] RefreshTokenRequest request,
        CancellationToken cancellationToken)
    {
        var response = await Mediator.Send(new RefreshTokenCommand(request), cancellationToken);
        return Ok(response);
    }
}
