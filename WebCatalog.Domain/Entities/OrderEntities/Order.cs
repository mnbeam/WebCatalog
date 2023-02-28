namespace WebCatalog.Domain.Entities.OrderEntities;

public class Order : BaseEntity
{
    public int AppUserId { get; set; }

    public AppUser? AppUser { get; set; }

    public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;

    public List<OrderItem> OrderItems { get; set; } = null!;
}