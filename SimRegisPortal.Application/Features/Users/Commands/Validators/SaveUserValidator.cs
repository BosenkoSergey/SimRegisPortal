using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SimRegisPortal.Application.Extensions;
using SimRegisPortal.Core.Exceptions;
using SimRegisPortal.Persistence.Context;

namespace SimRegisPortal.Application.Features.Users.Commands.Validators;

public sealed class SaveUserValidator
        : AbstractValidator<SaveUserCommand>
{
    private readonly AppDbContext _dbContext;

    public SaveUserValidator(AppDbContext dbContext)
    {
        _dbContext = dbContext;

        RuleFor(x => x.Dto.FullName)
            .NotEmpty().WithTemplate("Validation.Field.Required", "Повне ім'я");

        RuleFor(x => x.Dto.Login)
            .NotEmpty().WithTemplate("Validation.Field.Required", "Логін");

        RuleFor(x => x.Dto.Email)
            .EmailAddress().WithTemplate("Validation.Email.Invalid");

        RuleFor(x => x)
            .CustomAsync(CustomCheckAsync);
    }

    private async Task CustomCheckAsync(
        SaveUserCommand command,
        ValidationContext<SaveUserCommand> context,
        CancellationToken cancellationToken)
    {
        var isLoginExists = await _dbContext.Users
            .AnyAsync(u => u.Login == command.Dto.Login
                        && u.Id != command.Dto.Id, cancellationToken);
        if (isLoginExists)
        {
            throw new CommonException("Validation.Login.AlreadyExists");
        }

        var isEmailExists = await _dbContext.Users
            .AnyAsync(u => u.Email == command.Dto.Email
                        && u.Id != command.Dto.Id, cancellationToken);
        if (isEmailExists)
        {
            throw new CommonException("Validation.Email.AlreadyExists");
        }
    }
}