using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebCatalog.Domain.Enums;
using WebCatalog.Logic.Common.Extensions;

namespace WebCatalog.Infrastructure.DataBase.Configurations.BaseConfigurations;

public class IdentityRoleConfiguration : IEntityTypeConfiguration<IdentityRole<int>>
{
    public void Configure(EntityTypeBuilder<IdentityRole<int>> builder)
    {
        builder.HasData(new IdentityRole<int>
        {
            Id = 1,
            Name = Role.Admin.GetEnumDescription(),
            NormalizedName = Role.Admin.GetEnumDescription().ToUpper()
        }, new IdentityRole<int>
        {
            Id = 2,
            Name = Role.Customer.GetEnumDescription(),
            NormalizedName = Role.Customer.GetEnumDescription().ToUpper()
        }, new IdentityRole<int>
        {
            Id = 3,
            Name = Role.Seller.GetEnumDescription(),
            NormalizedName = Role.Seller.GetEnumDescription().ToUpper()
        });
    }
}