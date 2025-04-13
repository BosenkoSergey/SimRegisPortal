using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SimRegisPortal.Application.Extensions;
using SimRegisPortal.Core.Exceptions;
using SimRegisPortal.Persistence.Context;

namespace SimRegisPortal.Application.Features.Users.Commands.Validators;

public sealed class AddUserValidator
        : AbstractValidator<AddUserCommand>
{
    private readonly AppDbContext _dbContext;

    public AddUserValidator(AppDbContext dbContext)
    {
        _dbContext = dbContext;

        RuleFor(x => x.Request.Email)
            .EmailAddress().WithTemplate("Validation.Email.Invalid");

        RuleFor(x => x)
            .CustomAsync(ValidateAsync);
    }

    private async Task ValidateAsync(
        AddUserCommand command,
        ValidationContext<AddUserCommand> context,
        CancellationToken cancellationToken)
    {
        var isLoginExists = await _dbContext.Users
            .AnyAsync(u => u.Login == command.Request.Login, cancellationToken);
        if (isLoginExists)
        {
            throw new CommonException("Validation.Login.AlreadyExists");
        }

        var isEmailExists = await _dbContext.Users
            .AnyAsync(u => u.Email == command.Request.Email, cancellationToken);
        if (isEmailExists)
        {
            throw new CommonException("Validation.Email.AlreadyExists");
        }
    }
}