using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pustok.Data.Migrations
{
    /// <inheritdoc />
    public partial class PriceAfterDiscountAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "PriceAfterDiscount",
                table: "OrderItems",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PriceAfterDiscount",
                table: "Books",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PriceAfterDiscount",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "PriceAfterDiscount",
                table: "Books");
        }
    }
}
