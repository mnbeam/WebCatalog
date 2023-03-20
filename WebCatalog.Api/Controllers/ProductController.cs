using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebCatalog.Logic.WebCatalog.Products.Commands.CreateProduct;
using WebCatalog.Logic.WebCatalog.Products.Commands.DeleteProduct;
using WebCatalog.Logic.WebCatalog.Products.Commands.UpdateProduct;
using WebCatalog.Logic.WebCatalog.Products.Queries.GetProduct;
using WebCatalog.Logic.WebCatalog.Products.Queries.GetProductList;
using WebCatalog.Logic.WebCatalog.Products.Queries.GetProductListByBrand;
using WebCatalog.Logic.WebCatalog.Products.Queries.GetProductListByCategory;

namespace WebCatalog.Api.Controllers;

public class ProductController : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetProductList()
    {
        var getProductListQuery = new GetProductListQuery();

        var vm = await Mediator.Send(getProductListQuery);

        return Ok(vm);
    }

    [HttpGet("{productId}")]
    public async Task<IActionResult> GetProduct(int productId)
    {
        var getProductQuery = new GetProductQuery
        {
            ProductId = productId
        };

        var vm = await Mediator.Send(getProductQuery);

        return Ok(vm);
    }

    [HttpGet("bycategoryId/{categoryId}")]
    public async Task<IActionResult> GetProductListByCategory([FromRoute] int categoryId)
    {
        var getProductListByCategoryQuery = new GetProductListByCategoryQuery
        {
            CategoryId = categoryId
        };

        var vm = await Mediator.Send(getProductListByCategoryQuery);

        return Ok(vm);
    }

    [HttpGet("bybrandId/{brandId}")]
    public async Task<IActionResult> GetProductListByBrand([FromRoute] int brandId)
    {
        var getProductListByBrandQuery = new GetProductListByBrandQuery
        {
            BrandId = brandId
        };

        var vm = await Mediator.Send(getProductListByBrandQuery);

        return Ok(vm);
    }

    [Authorize(Policy = "ForSeller")]
    [HttpPost]
    public async Task<IActionResult> CreateProduct(CreateProductCommand createProductCommand)
    {
        await Mediator.Send(createProductCommand);

        return Ok();
    }

    [Authorize(Policy = "ForSeller")]
    [HttpPut]
    public async Task<IActionResult> UpdateProduct(UpdateProductCommand updateProductCommand)
    {
        await Mediator.Send(updateProductCommand);

        return Ok();
    }

    [Authorize(Policy = "ForSeller")]
    [HttpDelete]
    public async Task<IActionResult> DeleteProduct(DeleteProductCommand deleteProductCommand)
    {
        await Mediator.Send(deleteProductCommand);

        return Ok();
    }
}