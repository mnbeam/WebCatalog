using WebCatalog.Logic.DataTransferObjects.BasketDtos;

namespace WebCatalog.Logic.Abstractions;

public interface IBasketService
{
    Task<List<BasketItemDto>> GetBasket();

    Task AddItemToBasket(int productId, int quantity = 1);

    Task RemoveItemFromBasket(int productId);

    Task ClearBasket();
}