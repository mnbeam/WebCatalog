namespace WebCatalog.Domain.Entities.ProductEntities;

public class Review : BaseEntity
{
    public string? Content { get; set; }

    public string UserName { get; set; } = null!;

    public int Rating { get; set; }

    public int ProductId { get; set; }

    public Product? Product { get; set; }
}