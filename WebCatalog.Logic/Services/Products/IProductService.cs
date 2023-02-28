using WebCatalog.Logic.Services.Orders.Dtos;

namespace WebCatalog.Logic.Services.Products;

public interface IProductService
{
    Task<List<ProductDto>> GetSellerProducts();

    Task<List<ProductDto>> GetProductsByCategory(int categoryId);

    Task<List<ProductDto>> GetProductsByBrand(int brandId);

    Task CreateProduct(ProductDto productDto);

    Task DeleteProduct(int productId);
}