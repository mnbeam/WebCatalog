using WebCatalog.Domain.Entities.Base;
using WebCatalog.Domain.Entities.CustomerEntities;

namespace WebCatalog.Domain.Entities.OrderEntities;

public class Order : BaseEntity
{
    public int CustomerId { get; set; }

    public Customer? Customer { get; set; }

    public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;

    public List<OrderItem> OrderItems { get; set; } = null!;
}