using WebCatalog.Domain.Entities.Base;
using WebCatalog.Domain.Entities.CustomerEntities;

namespace WebCatalog.Domain.Entities.BasketEntities;

public class Basket : BaseEntity
{
    public int CustomerId { get; set; }

    public Customer? Customer { get; set; }

    public List<BasketItem>? BasketItems { get; set; }
}