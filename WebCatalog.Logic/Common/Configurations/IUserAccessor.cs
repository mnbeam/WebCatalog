using System.Security.Claims;

namespace WebCatalog.Logic.Common.Configurations;

/// <summary>
/// Сервис для получения данных пользователя с http-запроса.
/// </summary>
public interface IUserAccessor
{
    public ClaimsPrincipal User { get; }

    public int UserId { get; }
}