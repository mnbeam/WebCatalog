﻿using Microsoft.AspNetCore.Identity;
using WebCatalog.Domain.Entities.Base;
using WebCatalog.Domain.Entities.BasketEntities;
using WebCatalog.Domain.Entities.OrderEntities;

namespace WebCatalog.Domain.Entities.CustomerEntities;

public class Customer : IdentityUser<int>
{
    public Token? Token { get; set; }

    public Basket? Basket { get; set; }

    public Order? Order { get; set; }
}