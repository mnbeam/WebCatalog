using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebCatalog.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveUnitPriceFromBasketItemTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UnitPrice",
                table: "BasketItems");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "UnitPrice",
                table: "BasketItems",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
