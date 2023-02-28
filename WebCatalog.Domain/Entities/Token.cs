namespace WebCatalog.Domain.Entities;

public class Token : BaseEntity
{
    public int UserId { get; set; }

    public AppUser? AppUser { get; set; }

    public string Client { get; set; } = null!;

    public string Value { get; set; } = null!;

    public DateTime ExpireTime { get; set; }

    public DateTime? UpdatedTime { get; set; }
}