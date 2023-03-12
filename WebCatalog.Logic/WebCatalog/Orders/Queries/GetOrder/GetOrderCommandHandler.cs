using MediatR;

namespace WebCatalog.Logic.WebCatalog.Orders.Queries.GetOrder;

public class GetOrderCommandHandler : IRequestHandler<GetOrderCommand, OrderVm>
{
    public async Task<OrderVm> Handle(GetOrderCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}