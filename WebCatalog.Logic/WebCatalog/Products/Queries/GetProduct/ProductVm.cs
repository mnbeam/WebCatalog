using WebCatalog.Domain.Entities.ProductEntities;
using WebCatalog.Logic.Common.Mappings;

namespace WebCatalog.Logic.WebCatalog.Products.Queries.GetProduct;

public class ProductVm : IMapWith<Product>
{
    public int Id { get; set; }

    public int CategoryId { get; set; }

    public int BrandId { get; set; }

    public string Name { get; set; } = default!;

    public string? Description { get; set; }

    public decimal Price { get; set; }
}