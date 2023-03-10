using MediatR;
using WebCatalog.Logic.WebCatalog.Products.Queries.GetProductList;

namespace WebCatalog.Logic.WebCatalog.Products.Queries.GetProductListByBrandId;

public class GetProductListByBrandQuery : IRequest<ProductListVm>
{
    public int BrandId { get; set; }
}