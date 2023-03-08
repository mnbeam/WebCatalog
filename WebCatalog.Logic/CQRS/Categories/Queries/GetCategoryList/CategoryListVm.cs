using MediatR;
using WebCatalog.Logic.CQRS.Categories.Queries.GetCategory;

namespace WebCatalog.Logic.CQRS.Categories.Queries.GetCategoryList;

public class CategoryListVm : IRequest
{
    public List<GetCategoryVm>? CategoryList { get; set; }
}