using MediatR;

namespace WebCatalog.Logic.WebCatalog.Accounts.Commands.Register;

public class RegisterCommand : IRequest
{
    public string UserName { get; set; }

    public string Password { get; set; }

    public string Email { get; set; }
}