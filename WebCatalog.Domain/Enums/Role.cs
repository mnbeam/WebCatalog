using System.ComponentModel;

namespace WebCatalog.Domain.Enums;

public enum Role
{
    [Description("admin")]
    Admin = 2,

    [Description("customer")]
    Customer = 4,

    [Description("seller")]
    Seller = 8
}