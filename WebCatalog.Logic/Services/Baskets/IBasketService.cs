using WebCatalog.Logic.Services.Baskets.Dtos;

namespace WebCatalog.Logic.Services.Baskets;

public interface IBasketService
{
    Task<List<BasketItemDto>> GetBasket();

    Task AddItemToBasket(int productId, int quantity = 1);

    Task RemoveItemFromBasket(int productId);

    Task ClearBasket();
}