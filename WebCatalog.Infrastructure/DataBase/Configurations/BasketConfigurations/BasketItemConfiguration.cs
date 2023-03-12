using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebCatalog.Domain.Entities.BasketEntities;

namespace WebCatalog.Infrastructure.DataBase.Configurations.BasketConfigurations;

public class BasketItemConfiguration : IEntityTypeConfiguration<BasketItem>
{
    public void Configure(EntityTypeBuilder<BasketItem> builder)
    {
        builder.Property(bi => bi.BasketId)
            .IsRequired();

        builder.Property(bi => bi.ProductId)
            .IsRequired();

        builder.Property(bi => bi.Quantity)
            .IsRequired();
    }
}