namespace WebCatalog.Logic.DataTransferObjects.CustomerDtos;

public class LoginDto
{
    public bool HaveRefreshToken { get; set; }

    public string UserName { get; set; }

    public string Password { get; set; }

    public string RefreshToken { get; set; }
}