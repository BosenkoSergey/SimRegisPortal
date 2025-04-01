using MediatR;
using SimRegisPortal.Application.Models.Auth.Requests;
using SimRegisPortal.Application.Models.Auth.Responses;

namespace SimRegisPortal.Application.Features.Auth.Commands.Requests
{
    public sealed record LoginCommand(
        LoginRequest Request)
        : IRequest<AuthResponse>;
}
