using System.Security.Claims;

namespace WebCatalog.Logic.Configurations;

public interface IUserAccessor
{
    public ClaimsPrincipal User { get; }

    public int UserId { get; }
}