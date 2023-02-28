using WebCatalog.Domain.Entities;

namespace WebCatalog.Logic.Services.Tokens;

public interface ITokenService
{
    string CreateAccessToken(AppUser appUser);

    Task<string> CreateRefreshToken(int userId);
}