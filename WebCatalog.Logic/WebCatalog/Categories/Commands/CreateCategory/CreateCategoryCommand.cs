using MediatR;

namespace WebCatalog.Logic.WebCatalog.Categories.Commands.CreateCategory;

public class CreateCategoryCommand : IRequest
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
}