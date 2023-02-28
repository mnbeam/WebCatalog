namespace WebCatalog.Domain.Entities.BasketEntities;

public class Basket : BaseEntity
{
    public int AppUserId { get; set; }

    public AppUser? AppUser { get; set; }

    public List<BasketItem>? BasketItems { get; set; }
}