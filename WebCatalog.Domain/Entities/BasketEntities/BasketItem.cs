using WebCatalog.Domain.Entities.ProductEntities;

namespace WebCatalog.Domain.Entities.BasketEntities;

public class BasketItem : BaseEntity
{
    public int Quantity { get; set; }

    public int ProductId { get; set; }

    public Product? Product { get; set; }

    public int BasketId { get; set; }

    public Basket? Basket { get; set; }
}