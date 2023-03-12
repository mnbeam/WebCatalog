using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebCatalog.Domain.Entities.BasketEntities;

namespace WebCatalog.Infrastructure.DataBase.Configurations.BasketConfigurations;

public class BasketConfiguration : IEntityTypeConfiguration<Basket>
{
    public void Configure(EntityTypeBuilder<Basket> builder)
    {
        builder.Property(b => b.UserId)
            .IsRequired();

        builder.HasMany(b => b.BasketItems)
            .WithOne(bi => bi.Basket)
            .HasForeignKey(bi => bi.BasketId);
    }
}