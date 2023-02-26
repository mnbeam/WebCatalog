using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebCatalog.Domain.Entities.OrderEntities;

namespace WebCatalog.Infrastructure.DataBase.Configurations.OrderConfigurations;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.Property(oi => oi.UnitPrice)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.OwnsOne(oi => oi.ProductOrdered, ba =>
        {
            ba.WithOwner();

            ba.Property(po => po.ProductName)
                .HasMaxLength(50)
                .IsRequired();

            ba.Property(po => po.ProductId)
                .IsRequired();
        });
    }
}