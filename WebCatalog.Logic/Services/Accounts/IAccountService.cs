using WebCatalog.Logic.Services.Accounts.Dtos;
using WebCatalog.Logic.Services.Tokens.Dtos;

namespace WebCatalog.Logic.Services.Accounts;

public interface IAccountService
{
    Task Register(RegisterDto registerDto);

    Task<TokenDto> Login(LoginDto loginDto);
}