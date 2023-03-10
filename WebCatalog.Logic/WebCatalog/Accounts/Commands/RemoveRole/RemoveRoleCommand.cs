using MediatR;
using WebCatalog.Domain.Enums;

namespace WebCatalog.Logic.WebCatalog.Accounts.Commands.RemoveRole;

public class RemoveRoleCommand : IRequest
{
    public int UserId { get; set; }

    public Role Role { get; set; }
}