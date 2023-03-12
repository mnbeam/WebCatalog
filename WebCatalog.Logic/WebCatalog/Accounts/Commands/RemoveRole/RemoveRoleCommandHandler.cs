using MediatR;
using Microsoft.AspNetCore.Identity;
using WebCatalog.Domain.Entities;
using WebCatalog.Logic.Common.Exceptions;
using WebCatalog.Logic.Common.Extensions;

namespace WebCatalog.Logic.WebCatalog.Accounts.Commands.RemoveRole;

public class RemoveRoleCommandHandler : IRequestHandler<RemoveRoleCommand>
{
    private readonly UserManager<AppUser> _userManager;

    public RemoveRoleCommandHandler(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task Handle(RemoveRoleCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId.ToString());

        if (user == null)
        {
            throw new WebCatalogNotFoundException(nameof(AppUser), request.UserId);
        }

        var roleResult =
            await _userManager.RemoveFromRoleAsync(user, request.Role.GetEnumDescription());

        if (!roleResult.Succeeded)
        {
            throw new Exception("error while removing role");
        }
    }
}