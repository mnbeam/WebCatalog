using MediatR;
using WebCatalog.Logic.WebCatalog.Categories.Queries.GetCategory;

namespace WebCatalog.Logic.WebCatalog.Categories.Queries.GetCategoryList;

public class CategoryListVm : IRequest
{
    public List<GetCategoryVm>? Categories { get; set; }
}