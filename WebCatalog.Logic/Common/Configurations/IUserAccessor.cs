using System.Security.Claims;

namespace WebCatalog.Logic.Common.Configurations;

public interface IUserAccessor
{
    public ClaimsPrincipal User { get; }

    public int UserId { get; }
}