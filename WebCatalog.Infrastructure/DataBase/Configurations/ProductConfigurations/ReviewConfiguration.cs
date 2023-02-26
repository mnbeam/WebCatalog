using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebCatalog.Domain.Entities.ProductEntities;

namespace WebCatalog.Infrastructure.DataBase.Configurations.ProductConfigurations;

public class ReviewConfiguration : IEntityTypeConfiguration<Review>
{
    public void Configure(EntityTypeBuilder<Review> builder)
    {
        builder.Property(r => r.Rating)
            .IsRequired();

        builder.Property(r => r.UserName)
            .IsRequired();
    }
}