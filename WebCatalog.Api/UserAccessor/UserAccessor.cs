using System.Security.Claims;
using WebCatalog.Logic.Common.Configurations;
using WebCatalog.Logic.Common.Exceptions;

namespace WebCatalog.Api.UserAccessor;

/// <inheritdoc />
public class UserAccessor : IUserAccessor
{
    private readonly IHttpContextAccessor _contextAccessor;

    public UserAccessor(IHttpContextAccessor contextAccessor)
    {
        _contextAccessor = contextAccessor ?? throw new ArgumentException();
    }

    public ClaimsPrincipal User => _contextAccessor.HttpContext!.User;

    public int UserId
    {
        get
        {
            if (User.Identity == null)
                throw new WebCatalogValidationException("User claims identity not found.");
        
            var isUserIdExist = int.TryParse(User.Identity.Name, out var userId);
            if (!isUserIdExist)
                throw new WebCatalogValidationException("Can not get userId.");

            return userId;
        }
    } 
}