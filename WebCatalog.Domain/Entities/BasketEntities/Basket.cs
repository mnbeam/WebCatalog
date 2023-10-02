namespace WebCatalog.Domain.Entities.BasketEntities;

/// <summary>
/// Корзина.
/// </summary>
public class Basket : BaseEntity
{
    public int UserId { get; set; }

    public AppUser? User { get; set; }

    /// <summary>
    /// Товары корзины.
    /// </summary>
    public List<BasketItem> Items { get; set; } = new();
}