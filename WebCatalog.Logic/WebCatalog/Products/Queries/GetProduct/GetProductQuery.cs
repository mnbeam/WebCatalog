using MediatR;

namespace WebCatalog.Logic.WebCatalog.Products.Queries.GetProduct;

public class GetProductQuery : IRequest<ProductVm>
{
    public int ProductId { get; set; }
}