namespace WebCatalog.Domain.Entities.OrderEntities;

public class ProductOrdered
{
    public int ProductId { get; set; }

    public string ProductName { get; set; } = null!;
}