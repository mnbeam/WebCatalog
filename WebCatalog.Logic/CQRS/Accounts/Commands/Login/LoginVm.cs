namespace WebCatalog.Logic.CQRS.Accounts.Commands.Login;

public class LoginVm
{
    public string AccessToken { get; set; }

    public string RefreshToken { get; set; }
}