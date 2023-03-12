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
            .WithOne(b => b.User)
            .HasForeignKey<Basket>(b => b.UserId);

        builder.HasOne(u => u.Order)
            .WithOne(o => o.User)
            .HasForeignKey<Order>(o => o.UserId);

        builder.HasOne(u => u.Token)
            .WithOne(t => t.AppUser)
            .HasForeignKey<Token>(t => t.UserId);

        builder.HasMany(u => u.Reviews)
            .WithOne(r => r.User)
            .HasForeignKey(r => r.UserId);
    }
}