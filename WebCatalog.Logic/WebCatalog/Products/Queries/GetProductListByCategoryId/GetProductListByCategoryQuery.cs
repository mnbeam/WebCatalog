using MediatR;
using WebCatalog.Logic.WebCatalog.Products.Queries.GetProductList;

namespace WebCatalog.Logic.WebCatalog.Products.Queries.GetProductListByCategoryId;

public class GetProductListByCategoryQuery : IRequest<ProductListVm>
{
    public int CategoryId { get; set; }
}