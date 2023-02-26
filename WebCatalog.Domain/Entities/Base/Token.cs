using WebCatalog.Domain.Entities.CustomerEntities;

namespace WebCatalog.Domain.Entities.Base;

public class Token : BaseEntity
{
    public int CustomerId { get; set; }

    public Customer? Customer { get; set; }

    public string Client { get; set; } = null!;

    public string Value { get; set; } = null!;

    public DateTime ExpireTime { get; set; }

    public DateTime? UpdatedTime { get; set; }
}