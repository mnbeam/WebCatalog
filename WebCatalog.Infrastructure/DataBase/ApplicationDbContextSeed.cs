using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebCatalog.Domain.Entities;
using WebCatalog.Domain.Entities.ProductEntities;
using WebCatalog.Domain.Enums;
using WebCatalog.Logic.Common.Extensions;
using WebCatalog.Logic.Common.ExternalServices;

namespace WebCatalog.Infrastructure.DataBase;

public class ApplicationDbContextSeed
{
    public static async Task SeedAsync(AppDbContext dbContext,
        UserManager<AppUser> userManager,
        RoleManager<IdentityRole<int>> roleManager,
        ILogger logger,
        int retryForAvailable = 0)
    {
        try
        {
            if (dbContext.Database.IsSqlServer())
            {
                dbContext.Database.Migrate();
            }

            if (!await dbContext.Brands.AnyAsync())
            {
                await dbContext.Brands.AddRangeAsync(GetInitialBrands());
                await dbContext.SaveChangesAsync();
            }

            if (!await dbContext.Categories.AnyAsync())
            {
                await dbContext.Categories.AddRangeAsync(GetInitialCategories());
                await dbContext.SaveChangesAsync();
            }

            if (!await dbContext.Products.AnyAsync())
            {
                await dbContext.Products.AddRangeAsync(GetInitialProducts());
                await dbContext.SaveChangesAsync();
            }

            if (!await dbContext.Users.AnyAsync())
            {
                await SeedUsersAsync(userManager, roleManager);
                await dbContext.SaveChangesAsync();
            }
        }
        catch (Exception ex)
        {
            if (retryForAvailable >= 6)
            {
                throw;
            }

            retryForAvailable++;

            logger.LogError(ex.Message);
            await SeedAsync(dbContext, userManager, roleManager, logger, retryForAvailable);
            throw;
        }
    }

    private static async Task SeedUsersAsync(UserManager<AppUser> userManager,
        RoleManager<IdentityRole<int>> roleManager)
    {
        var admin = new AppUser
        {
            UserName = "admin",
            Email = "admin@gmail.com"
        };
        await userManager.CreateAsync(admin, "Admin$123");


        var customer = new AppUser
        {
            UserName = "customer",
            Email = "customer@gmail.com"
        };
        await userManager.CreateAsync(customer, "Customer$123");


        var seller = new AppUser
        {
            UserName = "seller",
            Email = "seller@gmail.com"
        };
        await userManager.CreateAsync(seller, "Seller$123");

        await roleManager.CreateAsync(new IdentityRole<int>(Role.Admin.GetEnumDescription()));
        await roleManager.CreateAsync(new IdentityRole<int>(Role.Customer.GetEnumDescription()));
        await roleManager.CreateAsync(new IdentityRole<int>(Role.Seller.GetEnumDescription()));

        var adminUser = await userManager.FindByNameAsync("admin");
        await userManager.AddToRoleAsync(adminUser, Role.Admin.GetEnumDescription());

        var customerUser = await userManager.FindByNameAsync("customer");
        await userManager.AddToRoleAsync(customerUser, Role.Customer.GetEnumDescription());

        var sellerUser = await userManager.FindByNameAsync("seller");
        await userManager.AddToRoleAsync(sellerUser, Role.Seller.GetEnumDescription());
    }

    private static IEnumerable<Brand> GetInitialBrands()
    {
        return new List<Brand>
        {
            new()
            {
                Name = "Apple"
            },
            new()
            {
                Name = "Samsung"
            },
            new()
            {
                Name = "Lenovo"
            },
            new()
            {
                Name = "Realme"
            },
            new()
            {
                Name = "Asus"
            }
        };
    }

    private static IEnumerable<Category> GetInitialCategories()
    {
        return new List<Category>
        {
            new()
            {
                Name = "Phones",
                Description = "For calls"
            },
            new()
            {
                Name = "Laptops",
                Description = "For work"
            },
            new()
            {
                Name = "Tablets",
                Description = "For reading"
            },
            new()
            {
                Name = "Headphones",
                Description = "For music"
            }
        };
    }

    private static IEnumerable<Product> GetInitialProducts()
    {
        return new List<Product>
        {
            new()
            {
                BrandId = 1,
                CategoryId = 1,
                Name = "iphone",
                Description = "smartphone",
                Price = 79999
            },
            new()
            {
                BrandId = 2,
                CategoryId = 4,
                Name = "cx700",
                Description = "random headphones",
                Price = 600
            },
            new()
            {
                BrandId = 3,
                CategoryId = 2,
                Name = "Yoga5000",
                Description = "ultra book",
                Price = 99999
            },
            new()
            {
                BrandId = 4,
                CategoryId = 1,
                Name = "7 pro",
                Description = "smartphone",
                Price = 18999
            },
            new()
            {
                BrandId = 1,
                CategoryId = 3,
                Name = "ipad",
                Description = "the best tablet",
                Price = 79999
            },
            new()
            {
                BrandId = 2,
                CategoryId = 2,
                Name = "galaxy",
                Description = "smartphone",
                Price = 79999
            },
            new()
            {
                BrandId = 3,
                CategoryId = 3,
                Name = "6 pro ultra plus",
                Description = "tablet for life",
                Price = 66666
            },
            new()
            {
                BrandId = 4,
                CategoryId = 3,
                Name = "5 pro",
                Description = "random tablet",
                Price = 45000
            },
            new()
            {
                BrandId = 5,
                CategoryId = 1,
                Name = "ROG 2",
                Description = "Mobile gaming",
                Price = 79999
            },
            new()
            {
                BrandId = 5,
                CategoryId = 2,
                Name = "Z275UKNH",
                Description = "random laptop",
                Price = 34000
            }
        };
    }
}