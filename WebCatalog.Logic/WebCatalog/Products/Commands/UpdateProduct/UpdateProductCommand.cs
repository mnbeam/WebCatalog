using MediatR;

namespace WebCatalog.Logic.WebCatalog.Products.Commands.UpdateProduct;

public class UpdateProductCommand : IRequest
{
    public int ProductId { get; set; }

    public int CategoryId { get; set; }

    public int BrandId { get; set; }

    public string Name { get; set; }

    public string? Description { get; set; }

    public decimal Price { get; set; }
}