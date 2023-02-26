﻿namespace WebCatalog.Logic.DataTransferObjects.ProductDtos;

public class ProductDto
{
    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public int CategoryId { get; set; }

    public decimal Price { get; set; }

    public int BrandId { get; set; }
}