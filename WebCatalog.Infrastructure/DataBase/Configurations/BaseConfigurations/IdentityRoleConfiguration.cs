using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebCatalog.Domain.Consts;

namespace WebCatalog.Infrastructure.DataBase.Configurations.BaseConfigurations;

public class IdentityRoleConfiguration : IEntityTypeConfiguration<IdentityRole<int>>
{
    public void Configure(EntityTypeBuilder<IdentityRole<int>> builder)
    {
        builder.HasData(new IdentityRole<int>
        {
            Id = 1,
            Name = Roles.Customer,
            NormalizedName = Roles.Customer.ToUpper()
        }, new IdentityRole<int>
        {
            Id = 2,
            Name = Roles.Seller,
            NormalizedName = Roles.Seller.ToUpper()
        }, new IdentityRole<int>
        {
            Id = 3,
            Name = Roles.Admin,
            NormalizedName = Roles.Admin.ToUpper()
        });
    }
}