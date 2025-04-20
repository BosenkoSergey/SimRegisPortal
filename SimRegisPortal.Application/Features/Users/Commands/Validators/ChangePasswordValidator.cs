using FluentValidation;
using SimRegisPortal.Application.Extensions;
using SimRegisPortal.Core.Helpers;

namespace SimRegisPortal.Application.Features.Users.Commands.Validators;

public sealed class ChangePasswordValidator
        : AbstractValidator<ChangePasswordCommand>
{
    public ChangePasswordValidator()
    {
        RuleFor(x => x.Request.Password)
            .Must(PasswordHelper.IsValid).WithTemplate("Validation.Password.Invalid")
            .Must(PasswordHelper.IsStrongEnough).WithTemplate("Validation.Password.Weak");
    }
}