using MediatR;

namespace WebCatalog.Logic.CQRS.Categories.Queries.GetCategory;

public class GetCategoryQuery : IRequest<GetCategoryVm>
{
    public int CategoryId { get; set; }
}