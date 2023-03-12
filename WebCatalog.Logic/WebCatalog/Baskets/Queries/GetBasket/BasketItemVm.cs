using WebCatalog.Domain.Entities.BasketEntities;
using WebCatalog.Logic.Common.Mappings;

namespace WebCatalog.Logic.WebCatalog.Baskets.Queries.GetBasket;

public class BasketItemVm : IMapWith<BasketItem>
{
    public int ProductId { get; set; }

    public int Quantity { get; set; }
}