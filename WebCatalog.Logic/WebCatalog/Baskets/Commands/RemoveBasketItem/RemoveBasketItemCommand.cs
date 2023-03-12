using MediatR;

namespace WebCatalog.Logic.WebCatalog.Baskets.Commands.RemoveBasketItem;

public class RemoveBasketItemCommand : IRequest
{
    public int ProductId { get; set; }

    public int Quantity { get; set; }
}