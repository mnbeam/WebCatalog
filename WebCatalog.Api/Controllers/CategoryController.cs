using Microsoft.AspNetCore.Mvc;
using WebCatalog.Logic.WebCatalog.Categories.Commands.CreateCategory;
using WebCatalog.Logic.WebCatalog.Categories.Commands.DeleteCategory;
using WebCatalog.Logic.WebCatalog.Categories.Commands.UpdateCategory;
using WebCatalog.Logic.WebCatalog.Categories.Queries.GetCategory;
using WebCatalog.Logic.WebCatalog.Categories.Queries.GetCategoryList;

namespace WebCatalog.Api.Controllers;

public class CategoryController : BaseController
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCategory(int id)
    {
        var getCategoryQuery = new GetCategoryQuery
        {
            CategoryId = id
        };

        var categoriesVm = await Mediator.Send(getCategoryQuery);

        return Ok(categoriesVm);
    }

    [HttpGet]
    public async Task<IActionResult> GetCategories()
    {
        var getCategoriesQuery = new GetCategoryListQuery();

        var categoriesVm = await Mediator.Send(getCategoriesQuery);

        return Ok(categoriesVm);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCategory(CreateCategoryCommand createCategoryCommand)
    {
        await Mediator.Send(createCategoryCommand);

        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> UpdateCategory(UpdateCategoryCommand updateCategoryCommand)
    {
        await Mediator.Send(updateCategoryCommand);

        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteCategory(DeleteCategoryCommand deleteCategoryCommand)
    {
        await Mediator.Send(deleteCategoryCommand);

        return Ok();
    }
}