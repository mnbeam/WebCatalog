using WebCatalog.Domain.Entities.CustomerEntities;

namespace WebCatalog.Logic.Abstractions;

public interface ITokenService
{
    Task<string> CreateAccessToken(Customer customer);

    Task<string> CreateRefreshToken(int userId);
}