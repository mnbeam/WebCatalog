namespace WebCatalog.Logic.Services.Baskets.Dtos;

public class BasketItemDto
{
    public int ProductId { get; set; }

    public decimal UnitPrice { get; set; }

    public int Quantity { get; set; }
}