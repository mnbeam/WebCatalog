using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebCatalog.Domain.Entities;
using WebCatalog.Domain.Entities.BasketEntities;
using WebCatalog.Domain.Entities.OrderEntities;
using WebCatalog.Domain.Entities.ProductEntities;

namespace WebCatalog.Logic.Common.ExternalServices;

/// <summary>
/// Контекст БД.
/// </summary>
public abstract class AppDbContext : IdentityDbContext<AppUser, IdentityRole<int>, int>
{
    protected AppDbContext(DbContextOptions options) : base(options)
    {
    }

    /// <summary>
    /// Продукты
    /// </summary>
    public DbSet<Product> Products { get; set; } = null!;
    
    /// <summary>
    /// Бренды.
    /// </summary>
    public DbSet<Brand> Brands { get; set; } = null!;
    
    /// <summary>
    /// Категории.
    /// </summary>
    public DbSet<Category> Categories { get; set; } = null!;
    
    /// <summary>
    /// Отзывы.
    /// </summary>
    public DbSet<Review> Reviews { get; set; } = null!;
    
    /// <summary>
    /// Корзины.
    /// </summary>
    public DbSet<Basket> Baskets { get; set; } = null!;
    
    /// <summary>
    /// Товары в корзине.
    /// </summary>
    public DbSet<BasketItem> BasketItems { get; set; } = null!;
    
    /// <summary>
    /// Заказы.
    /// </summary>
    public DbSet<Order> Orders { get; set; } = null!;
    
    /// <summary>
    /// Товары в заказе.
    /// </summary>
    public DbSet<OrderItem> OrderItems { get; set; } = null!;
    
    /// <summary>
    /// Токены.
    /// </summary>
    public DbSet<Token> Tokens { get; set; } = null!;
}