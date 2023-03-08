using Microsoft.AspNetCore.Identity;
using WebCatalog.Domain.Entities.BasketEntities;
using WebCatalog.Domain.Entities.OrderEntities;
using WebCatalog.Domain.Entities.ProductEntities;

namespace WebCatalog.Domain.Entities;

public class AppUser : IdentityUser<int>
{
    public Token? Token { get; set; }

    public Basket? Basket { get; set; }

    public Order? Order { get; set; }

    public List<Review>? Reviews { get; set; }
}