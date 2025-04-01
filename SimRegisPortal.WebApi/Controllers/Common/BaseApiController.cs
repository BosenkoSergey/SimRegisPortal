using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SimRegisPortal.WebApi.Controllers.Common
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseApiController : ControllerBase
    {
        protected readonly IMediator Mediator;

        protected BaseApiController(IMediator mediator)
        {
            Mediator = mediator;
        }
    }
}
