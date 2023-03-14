using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebCatalog.Logic.WebCatalog.Orders.Commands.CreateOrder;
using WebCatalog.Logic.WebCatalog.Orders.Queries.GetOrder;
using WebCatalog.Logic.WebCatalog.Orders.Queries.GetOrderList;

namespace WebCatalog.Api.Controllers;

public class OrderController : BaseController
{
    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetOrders()
    {
        var getOrdersCommand = new GetOrdersCommand();

        var ordersVm = await Mediator.Send(getOrdersCommand);

        return Ok(ordersVm);
    }
    
    [Authorize]
    [HttpGet("{orderId}")]
    public async Task<IActionResult> GetOrder(int orderId)
    {
        var getOrderCommand = new GetOrderCommand
        {
            OrderId = orderId
        };

        var orderVm = await Mediator.Send(getOrderCommand);

        return Ok(orderVm);
    }
    
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> CreateOrder()
    {
        var createOrderCommand = new CreateOrderCommand();
        
        await Mediator.Send(createOrderCommand);

        return Ok();
    }
}