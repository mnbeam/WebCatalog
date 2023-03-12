using MediatR;

namespace WebCatalog.Logic.WebCatalog.Orders.Queries.GetOrder;

public class GetOrderCommand : IRequest<OrderVm>
{
    public int OrderId { get; set; }
}