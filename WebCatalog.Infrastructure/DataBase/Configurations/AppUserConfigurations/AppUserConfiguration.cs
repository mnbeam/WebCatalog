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
        builder.HasOne(c => c.Basket)
            .WithOne(b => b.AppUser)
            .HasForeignKey<Basket>(b => b.AppUserId);

        builder.HasOne(c => c.Order)
            .WithOne(o => o.AppUser)
            .HasForeignKey<Order>(o => o.AppUserId);

        builder.HasOne(c => c.Token)
            .WithOne(t => t.AppUser)
            .HasForeignKey<Token>(t => t.UserId);
    }
}