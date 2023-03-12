using MediatR;
using WebCatalog.Domain.Entities.BasketEntities;

namespace WebCatalog.Logic.WebCatalog.Baskets.Commands.CreateBasket;

internal class CreateBasketCommand : IRequest<Basket>
{
    public int UserId { get; set; }
}