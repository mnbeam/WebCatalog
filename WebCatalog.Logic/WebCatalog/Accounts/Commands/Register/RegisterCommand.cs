using MediatR;

namespace WebCatalog.Logic.WebCatalog.Accounts.Commands.Register;

public class RegisterCommand : IRequest
{
    public string UserName { get; set; } = default!;

    public string Password { get; set; } = default!;

    public string Email { get; set; } = default!;
}