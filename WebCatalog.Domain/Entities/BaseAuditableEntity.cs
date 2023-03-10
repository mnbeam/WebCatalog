namespace WebCatalog.Domain.Entities;

public abstract class BaseAuditableEntity : BaseEntity
{
    public DateTime CreatedTime { get; set; }

    public DateTime? EditedTime { get; set; }
}