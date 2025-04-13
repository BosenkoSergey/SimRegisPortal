using FluentValidation;
using SimRegisPortal.Application.Extensions;
using SimRegisPortal.Core.Helpers;

namespace SimRegisPortal.Application.Features.Users.Commands.Validators;

public sealed class ChangeOwnPasswordValidator
        : AbstractValidator<ChangeOwnPasswordCommand>
{
    public ChangeOwnPasswordValidator()
    {
        RuleFor(x => x.Request.Password)
            .Must(PasswordHelper.IsValid).WithTemplate("Validation.Password.Invalid")
            .Must(PasswordHelper.IsStrongEnough).WithTemplate("Validation.Password.Weak");
    }
}