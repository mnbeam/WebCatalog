using WebCatalog.Domain.Entities;
using WebCatalog.Logic.Common.Mappings;

namespace WebCatalog.Logic.WebCatalog.Accounts.Queries.GetAppUserList;

public class AppUserVm : IMapWith<AppUser>
{
    public int Id { get; set; }

    public string? UserName { get; set; }

    public string? Email { get; set; }
}