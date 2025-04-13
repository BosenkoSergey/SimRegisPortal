using SimRegisPortal.WebApi.Controllers.Common;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using SimRegisPortal.Application.Models.Auth.Requests;
using SimRegisPortal.Application.Features.Auth.Commands;

namespace SimRegisPortal.WebApi.Controllers
{
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

        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken(
            [FromBody] RefreshTokenRequest request,
            CancellationToken cancellationToken)
        {
            var response = await Mediator.Send(new RefreshTokenCommand(request), cancellationToken);
            return Ok(response);
        }
    }
}
