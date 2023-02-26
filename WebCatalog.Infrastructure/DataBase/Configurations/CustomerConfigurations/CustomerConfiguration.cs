using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebCatalog.Domain.Entities.Base;
using WebCatalog.Domain.Entities.BasketEntities;
using WebCatalog.Domain.Entities.CustomerEntities;
using WebCatalog.Domain.Entities.OrderEntities;

namespace WebCatalog.Infrastructure.DataBase.Configurations.CustomerConfigurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasOne(c => c.Basket)
            .WithOne(b => b.Customer)
            .HasForeignKey<Basket>(b => b.CustomerId);
        
        builder.HasOne(c => c.Order)
            .WithOne(o => o.Customer)
            .HasForeignKey<Order>(o => o.CustomerId);
        
        builder.HasOne(c => c.Token)
            .WithOne(t => t.Customer)
            .HasForeignKey<Token>(t => t.CustomerId);
    }
}