using WebCatalog.Domain.Entities.Base;

namespace WebCatalog.Domain.Entities.ProductEntities;

public class Category : BaseEntity
{
    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public List<Product>? Products { get; set; }
}