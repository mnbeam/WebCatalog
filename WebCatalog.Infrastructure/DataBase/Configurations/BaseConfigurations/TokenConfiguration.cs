using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebCatalog.Domain.Entities;

namespace WebCatalog.Infrastructure.DataBase.Configurations.BaseConfigurations;

public class TokenConfiguration : IEntityTypeConfiguration<Token>
{
    public void Configure(EntityTypeBuilder<Token> builder)
    {
        builder.Property(t => t.Client)
            .IsRequired();

        builder.Property(t => t.Value)
            .IsRequired();
    }
}