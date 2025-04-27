using MediatR;
using Microsoft.AspNetCore.Mvc;
using SimRegisPortal.Application.Features.Users.Commands;
using SimRegisPortal.Application.Features.Users.Queries;
using SimRegisPortal.Application.Models.User;
using SimRegisPortal.WebApi.Controllers.Common;

namespace SimRegisPortal.WebApi.Controllers;

[Route("api/users")]
public class UsersController(IMediator mediator) : BaseApiController(mediator)
{
    [HttpGet]
    public async Task<IActionResult> GetUsers(CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new GetUsersQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser(Guid id, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new GetUserQuery(id), cancellationToken);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> AddUser(
        [FromBody] UserDto request,
        CancellationToken cancellationToken)
    {
        var response = await Mediator.Send(new AddUserCommand(request), cancellationToken);
        return Ok(response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> EditUser(
        Guid id,
        [FromBody] UserDto request,
        CancellationToken cancellationToken)
    {
        var response = await Mediator.Send(new EditUserCommand(id, request), cancellationToken);
        return Ok(response);
    }

    [HttpPost("{id}/reset-password")]
    public async Task<IActionResult> ResetPassword(Guid id, CancellationToken cancellationToken)
    {
        await Mediator.Send(new ResetPasswordCommand(id), cancellationToken);
        return NoContent();
    }

    [HttpPost("{id}/change-password")]
    public async Task<IActionResult> ChangeOwnPassword(
        Guid id,
        [FromBody] UserPasswordRequest request,
        CancellationToken cancellationToken)
    {
        await Mediator.Send(new ChangePasswordCommand(id, request), cancellationToken);
        return NoContent();
    }
}
