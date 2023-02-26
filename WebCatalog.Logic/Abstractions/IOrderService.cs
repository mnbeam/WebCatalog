namespace WebCatalog.Logic.Abstractions;

public interface IOrderService
{
    Task CreateOrder(int basketId);
}