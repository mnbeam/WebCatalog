using MediatR;

namespace WebCatalog.Logic.WebCatalog.Categories.Commands.DeleteCategory;

public class DeleteCategoryCommand : IRequest
{
    public int CategoryId { get; set; }
}