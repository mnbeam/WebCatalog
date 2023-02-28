namespace WebCatalog.Logic.Services.Orders;

public interface IOrderService
{
    Task CreateOrder(int basketId);
}