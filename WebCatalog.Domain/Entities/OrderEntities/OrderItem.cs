using WebCatalog.Domain.Entities.Base;

namespace WebCatalog.Domain.Entities.OrderEntities;

public class OrderItem : BaseEntity
{
    public ProductOrdered ProductOrdered { get; set; } = null!;

    public decimal UnitPrice { get; set; }

    public int Units { get; set; }
}