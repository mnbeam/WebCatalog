using MediatR;
using WebCatalog.Logic.WebCatalog.Products.Queries.GetProductList;

namespace WebCatalog.Logic.WebCatalog.Products.Queries.GetProductListByBrand;

public class GetProductListByBrandQuery : IRequest<ProductListVm>
{
    public int BrandId { get; set; }
}