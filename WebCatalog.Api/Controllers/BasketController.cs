using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebCatalog.Logic.WebCatalog.Baskets.Commands.AddBasketItem;
using WebCatalog.Logic.WebCatalog.Baskets.Commands.RemoveBasketItem;
using WebCatalog.Logic.WebCatalog.Baskets.Queries.GetBasket;

namespace WebCatalog.Api.Controllers;

public class BasketController : BaseController
{
    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetBasket()
    {
        var getBasketQuery = new GetBasketQuery();

        var basketVm = await Mediator.Send(getBasketQuery);

        return Ok(basketVm);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> AddBasketItem(AddBasketItemCommand addBasketItemCommand)
    {
        await Mediator.Send(addBasketItemCommand);

        return Ok();
    }

    [Authorize]
    [HttpDelete]
    public async Task<IActionResult> RemoveBasketItem(
        RemoveBasketItemCommand removeBasketItemCommand)
    {
        await Mediator.Send(removeBasketItemCommand);

        return Ok();
    }
}