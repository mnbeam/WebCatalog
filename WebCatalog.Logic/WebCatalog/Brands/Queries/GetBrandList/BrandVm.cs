using WebCatalog.Domain.Entities.ProductEntities;
using WebCatalog.Logic.Common.Mappings;

namespace WebCatalog.Logic.WebCatalog.Brands.Queries.GetBrandList;

public class BrandVm : IMapWith<Brand>
{
    public int Id { get; set; }

    public string Name { get; set; } = default!;
}