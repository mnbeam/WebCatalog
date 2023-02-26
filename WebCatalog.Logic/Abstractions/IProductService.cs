using WebCatalog.Logic.DataTransferObjects.ProductDtos;

namespace WebCatalog.Logic.Abstractions;

public interface IProductService
{
    Task<List<ProductDto>> GetSellerProducts();

    Task<List<ProductDto>> GetProductsByCategory(int categoryId);

    Task<List<ProductDto>> GetProductsByBrand(int brandId);

    Task CreateProduct(ProductDto productDto);

    Task DeleteProduct(int productId);
}