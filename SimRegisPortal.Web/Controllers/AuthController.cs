using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using SimRegisPortal.Application.Constants;
using SimRegisPortal.Application.Features.Users.Commands;
using System.Security.Claims;
using SimRegisPortal.Application.Models.Auth;

namespace SimRegisPortal.Web.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromForm] LoginRequest request)
    {
        var response = await _mediator.Send(new LoginCommand(request));

        var claims = new List<Claim>
        {
            new(CustomClaimTypes.UserId, response.UserId.ToString()),
            new(CustomClaimTypes.IsAdmin, response.IsAdmin.ToString()),
            new(CustomClaimTypes.Permissions, string.Join(Separators.UserPermissions, response.Permissions))
        };

        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            principal,
            new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTimeOffset.UtcNow.AddHours(8)
            });

        return Redirect("/");
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return Redirect("/auth/login");
    }
}
