using WebCatalog.Logic.DataTransferObjects.CustomerDtos;
using WebCatalog.Logic.DataTransferObjects.TokenDtos;

namespace WebCatalog.Logic.Abstractions;

public interface ICustomerService
{
    Task Register(RegisterDto registerDto);

    Task<TokenDto> Login(LoginDto loginDto);
}