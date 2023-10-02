using MediatR;

namespace WebCatalog.Logic.WebCatalog.Products.Commands.CreateProduct;

public class CreateProductCommand : IRequest
{
    public int CategoryId { get; set; }

    public int BrandId { get; set; }

    public string Name { get; set; } = default!;

    public string? Description { get; set; }

    public decimal Price { get; set; }
}