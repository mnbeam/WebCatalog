namespace WebCatalog.Domain.Entities.ProductEntities;

public class Brand : BaseEntity
{
    public string Name { get; set; } = null!;

    public List<Product> Products { get; set; } = new();
}