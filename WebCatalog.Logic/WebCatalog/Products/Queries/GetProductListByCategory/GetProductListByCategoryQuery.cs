using MediatR;
using WebCatalog.Logic.WebCatalog.Products.Queries.GetProductList;

namespace WebCatalog.Logic.WebCatalog.Products.Queries.GetProductListByCategory;

public class GetProductListByCategoryQuery : IRequest<ProductListVm>
{
    public int CategoryId { get; set; }
}