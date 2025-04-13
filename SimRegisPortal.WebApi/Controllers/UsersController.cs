using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimRegisPortal.WebApi.Controllers.Common;

namespace SimRegisPortal.WebApi.Controllers;

[Authorize]
[Route("api/users")]
public class UsersController : BaseApiController
{
    public UsersController(IMediator mediator) : base(mediator) { }
}
