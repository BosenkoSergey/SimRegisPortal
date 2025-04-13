using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimRegisPortal.Application.Features.Users.Commands;
using SimRegisPortal.WebApi.Controllers.Common;

namespace SimRegisPortal.WebApi.Controllers;

[Authorize]
[Route("api/user")]
public class UserController : BaseApiController
{
    public UserController(IMediator mediator) : base(mediator) { }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout(CancellationToken cancellationToken)
    {
        await Mediator.Send(new LogoutCommand(), cancellationToken);
        return NoContent();
    }
}
