using MediatR;

namespace WebCatalog.Logic.WebCatalog.Categories.Queries.GetCategory;

public class GetCategoryQuery : IRequest<GetCategoryVm>
{
    public int CategoryId { get; set; }
}