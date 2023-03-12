namespace WebCatalog.Domain.Entities.BasketEntities;

public class Basket : BaseEntity
{
    public int UserId { get; set; }

    public AppUser? User { get; set; }

    public List<BasketItem>? BasketItems { get; set; }
}