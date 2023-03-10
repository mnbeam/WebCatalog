using WebCatalog.Domain.Entities.ProductEntities;
using WebCatalog.Logic.Common.Mappings;

namespace WebCatalog.Logic.WebCatalog.Categories.Queries.GetCategory;

public class GetCategoryVm : IMapWith<Category>
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
}