namespace WebCatalog.Domain.Entities;

public interface IAuditable
{
    public DateTime CreatedTime { get; set; }
    
    public DateTime? EditedTime { get; set; }
}