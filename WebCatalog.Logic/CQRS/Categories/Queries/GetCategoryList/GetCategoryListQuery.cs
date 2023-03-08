using MediatR;

namespace WebCatalog.Logic.CQRS.Categories.Queries.GetCategoryList;

public class GetCategoryListQuery : IRequest<CategoryListVm>
{
}