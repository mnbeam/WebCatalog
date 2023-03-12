using MediatR;

namespace WebCatalog.Logic.WebCatalog.Baskets.Commands.AddBasketItem;

public class AddBasketItemCommand : IRequest
{
    public int ProductId { get; set; }

    public int Quantity { get; set; }
}