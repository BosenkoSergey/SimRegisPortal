using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimRegisPortal.Application.Features.Users.Commands;
using SimRegisPortal.Application.Features.Users.Queries;
using SimRegisPortal.Application.Models.Users;
using SimRegisPortal.Core.Enums;
using SimRegisPortal.WebApi.Attributes;
using SimRegisPortal.WebApi.Controllers.Common;

namespace SimRegisPortal.WebApi.Controllers;

[Authorize]
[Route("api/users")]
public class UsersController : BaseApiController
{
    public UsersController(IMediator mediator) : base(mediator) { }

    [AuthorizeByPermissions(UserPermissionType.UsersRead)]
    [HttpGet]
    public async Task<IActionResult> GetUsers(CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new GetUsersQuery(), cancellationToken);
        return Ok(result);
    }

    [AuthorizeByPermissions(UserPermissionType.UsersRead)]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser(Guid id, CancellationToken cancellationToken)
    {
        var result = await Mediator.Send(new GetUserQuery(id), cancellationToken);
        return Ok(result);
    }

    [AuthorizeByPermissions(UserPermissionType.UsersWrite)]
    [HttpPost]
    public async Task<IActionResult> AddUser(
        [FromBody] UserRequest request,
        CancellationToken cancellationToken)
    {
        var response = await Mediator.Send(new AddUserCommand(request), cancellationToken);
        return Ok(response);
    }

    [AuthorizeByPermissions(UserPermissionType.UsersWrite)]
    [HttpPut("{id}")]
    public async Task<IActionResult> EditUser(
        Guid id,
        [FromBody] UserRequest request,
        CancellationToken cancellationToken)
    {
        var response = await Mediator.Send(new EditUserCommand(id, request), cancellationToken);
        return Ok(response);
    }

    [AuthorizeByPermissions(UserPermissionType.UsersWrite)]
    [HttpPost("{id}/reset-password")]
    public async Task<IActionResult> ResetPassword(Guid id, CancellationToken cancellationToken)
    {
        await Mediator.Send(new ResetPasswordCommand(id), cancellationToken);
        return NoContent();
    }

    [HttpPost("current/change-password")]
    public async Task<IActionResult> ChangeOwnPassword(
        [FromBody] UserPasswordRequest request,
        CancellationToken cancellationToken)
    {
        await Mediator.Send(new ChangeOwnPasswordCommand(request), cancellationToken);
        return NoContent();
    }
}
