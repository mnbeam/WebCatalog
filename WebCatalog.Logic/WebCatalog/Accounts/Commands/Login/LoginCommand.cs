using MediatR;

namespace WebCatalog.Logic.WebCatalog.Accounts.Commands.Login;

public class LoginCommand : IRequest<LoginVm>
{
    public bool HaveRefreshToken { get; init; }

    public string? UserName { get; init; }

    public string? Password { get; init; }

    public string? RefreshToken { get; init; }
}