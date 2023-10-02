namespace WebCatalog.Logic.WebCatalog.Accounts.Commands.Login;

public class LoginVm
{
    public string AccessToken { get; set; } = default!;

    public string RefreshToken { get; set; } = default!;
}