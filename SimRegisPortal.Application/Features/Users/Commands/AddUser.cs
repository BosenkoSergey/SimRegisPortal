﻿using AutoMapper;
using SimRegisPortal.Application.Features.Base.Commands;
using SimRegisPortal.Application.Models.Mailing;
using SimRegisPortal.Application.Models.Users;
using SimRegisPortal.Application.Services.Interfaces;
using SimRegisPortal.Core.Entities;
using SimRegisPortal.Core.Helpers;
using SimRegisPortal.Persistence.Context;

namespace SimRegisPortal.Application.Features.Users.Commands;

public sealed record AddUserCommand(UserRequest Request)
    : AddCommand<UserRequest, UserResponse>(Request);

internal sealed class AddUserHandler(AppDbContext dbContext, IMapper mapper, IEmailService emailService)
    : AddHandler<AddUserCommand, UserRequest, User, UserResponse>(dbContext, mapper)
{
    protected override async Task AddOrEditEntity(User entity, AddUserCommand command, CancellationToken cancellationToken)
    {
        var password = PasswordHelper.Generate();

        entity.PasswordHash = PasswordHelper.GetHash(password);

        await base.AddOrEditEntity(entity, command, cancellationToken);
        await DbContext.SaveChangesAsync(cancellationToken);

        var message = Mapper.Map<UserCredentialsEmailDto>(entity, opt =>
            opt.AfterMap((src, dest) =>
            {
                dest.Password = password;
            }));

        await emailService.SendUserCreatedEmailAsync(message);
    }
}