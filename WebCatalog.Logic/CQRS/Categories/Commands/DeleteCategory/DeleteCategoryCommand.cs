using MediatR;

namespace WebCatalog.Logic.CQRS.Categories.Commands.DeleteCategory;

public class DeleteCategoryCommand : IRequest
{
    public int CategoryId { get; set; }
}