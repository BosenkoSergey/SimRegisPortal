using SimRegisPortal.WebApi.Controllers.Common;
using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace SimRegisPortal.WebApi.Controllers
{
    [Route("api/user")]
    public class UserController : BaseApiController
    {
        public UserController(IMediator mediator) : base(mediator) { }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout(CancellationToken cancellationToken)
        {
            //await Mediator.Send(new LogoutCommand(), cancellationToken);
            return NoContent();
        }
    }
}
