using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebCatalog.Domain.Entities;
using WebCatalog.Domain.Entities.BasketEntities;
using WebCatalog.Domain.Entities.OrderEntities;

namespace WebCatalog.Infrastructure.DataBase.Configurations.AppUserConfigurations;

public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.HasOne(u => u.Basket)
            .WithOne(b => b.AppUser)
            .HasForeignKey<Basket>(b => b.AppUserId);

        builder.HasOne(u => u.Order)
            .WithOne(o => o.AppUser)
            .HasForeignKey<Order>(o => o.AppUserId);

        builder.HasOne(u => u.Token)
            .WithOne(t => t.AppUser)
            .HasForeignKey<Token>(t => t.UserId);

        builder.HasMany(u => u.Reviews)
            .WithOne(r => r.AppUser)
            .HasForeignKey(r => r.UserId);
    }
}