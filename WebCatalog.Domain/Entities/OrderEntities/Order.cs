namespace WebCatalog.Domain.Entities.OrderEntities;

public class Order : BaseEntity
{
    public int UserId { get; set; }

    public AppUser? User { get; set; }

    public DateTimeOffset OrderDate { get; set; }

    public List<OrderItem> OrderItems { get; set; } = null!;
}