using MediatR;

namespace WebCatalog.Logic.WebCatalog.Categories.Commands.UpdateCategory;

public class UpdateCategoryCommand : IRequest
{
    public int CategoryId { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
}