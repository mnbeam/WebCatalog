﻿using MediatR;
using Microsoft.AspNetCore.Identity;
using WebCatalog.Domain.Entities;
using WebCatalog.Domain.Enums;
using WebCatalog.Logic.Extensions;

namespace WebCatalog.Logic.CQRS.Accounts.Commands.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand>
{
    private readonly UserManager<AppUser> _userManager;

    public RegisterCommandHandler(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var user = new AppUser
        {
            UserName = request.UserName,
            Email = request.Email.ToLower()
        };

        var createResult = await _userManager.CreateAsync(user, request.Password);

        if (!createResult.Succeeded)
        {
            throw new Exception("Customer creation error");
        }

        var roleResult = await _userManager.AddToRoleAsync(user, Role.Customer.GetEnumDescription());

        if (!roleResult.Succeeded)
        {
            await _userManager.DeleteAsync(user);

            throw new Exception("Adding role error");
        }
    }
}