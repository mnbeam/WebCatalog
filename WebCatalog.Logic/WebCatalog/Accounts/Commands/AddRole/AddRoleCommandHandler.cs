using MediatR;
using Microsoft.AspNetCore.Identity;
using WebCatalog.Domain.Entities;
using WebCatalog.Logic.Common.Exceptions;
using WebCatalog.Logic.Common.Extensions;

namespace WebCatalog.Logic.WebCatalog.Accounts.Commands.AddRole;

public class AddRoleCommandHandler : IRequestHandler<AddRoleCommand>
{
    private readonly UserManager<AppUser> _userManager;

    public AddRoleCommandHandler(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task Handle(AddRoleCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId.ToString());

        if (user == null)
        {
            throw new WebCatalogNotFoundException(nameof(AppUser), request.UserId);
        }

        var roleResult = await _userManager.AddToRoleAsync(user, request.Role.GetEnumDescription());

        if (!roleResult.Succeeded)
        {
            throw new ArgumentException("error while adding role");
        }
    }
}