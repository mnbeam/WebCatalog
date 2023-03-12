using Microsoft.AspNetCore.Mvc;
using WebCatalog.Logic.WebCatalog.Brands.Commands.CreateBrand;
using WebCatalog.Logic.WebCatalog.Brands.Commands.DeleteBrand;
using WebCatalog.Logic.WebCatalog.Brands.Commands.UpdateBrand;
using WebCatalog.Logic.WebCatalog.Brands.Queries.GetBrandList;

namespace WebCatalog.Api.Controllers;

public class BrandController : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetBrands()
    {
        var getBrandListQuery = new GetBrandListQuery();

        var brandsVm = await Mediator.Send(getBrandListQuery);

        return Ok(brandsVm);
    }

    [HttpPost]
    public async Task<IActionResult> CreateBrand(CreateBrandCommand createBrandCommand)
    {
        await Mediator.Send(createBrandCommand);

        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> UpdateBrand(UpdateBrandCommand updateBrandCommand)
    {
        await Mediator.Send(updateBrandCommand);

        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteBrand(DeleteBrandCommand deleteBrandCommand)
    {
        await Mediator.Send(deleteBrandCommand);

        return Ok();
    }
}