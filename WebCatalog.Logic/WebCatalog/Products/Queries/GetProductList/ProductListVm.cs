﻿using WebCatalog.Logic.WebCatalog.Products.Queries.GetProduct;

namespace WebCatalog.Logic.WebCatalog.Products.Queries.GetProductList;

public class ProductListVm
{
    public List<ProductVm> Products { get; set; } = new();
}