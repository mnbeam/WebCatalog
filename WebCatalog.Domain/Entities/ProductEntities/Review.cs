﻿namespace WebCatalog.Domain.Entities.ProductEntities;

public class Review : BaseAuditableEntity
{
    public int UserId { get; set; }
    public AppUser? User { get; set; }

    public int ProductId { get; set; }
    public Product? Product { get; set; }

    public string? Content { get; set; }

    public int Rating { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? EditedAt { get; set; }
}