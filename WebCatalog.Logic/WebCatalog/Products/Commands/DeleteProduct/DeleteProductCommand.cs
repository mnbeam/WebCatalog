using MediatR;

namespace WebCatalog.Logic.WebCatalog.Products.Commands.DeleteProduct;

public class DeleteProductCommand : IRequest
{
    public int ProductId { get; set; }
}