using MediatR;
using WebCatalog.Domain.Enums;

namespace WebCatalog.Logic.WebCatalog.Accounts.Commands.AddRole;

public class AddRoleCommand : IRequest
{
    public int UserId { get; set; }

    public Role Role { get; set; }
}