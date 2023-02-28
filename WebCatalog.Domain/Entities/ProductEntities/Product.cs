namespace WebCatalog.Domain.Entities.ProductEntities;

public class Product : BaseEntity
{
    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public int CategoryId { get; set; }

    public Category? Category { get; set; }

    public decimal Price { get; set; }

    public int BrandId { get; set; }

    public Brand? Brand { get; set; }

    public List<Review>? Reviews { get; set; }
}