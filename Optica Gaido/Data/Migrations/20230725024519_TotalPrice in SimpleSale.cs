using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Optica_Gaido.Data.Migrations
{
    /// <inheritdoc />
    public partial class TotalPriceinSimpleSale : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SettedPrice",
                table: "SimpleSaleProducts");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Products");

            migrationBuilder.AddColumn<decimal>(
                name: "TotalPrice",
                table: "SimpleSales",
                type: "money",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "SimpleSales");

            migrationBuilder.AddColumn<decimal>(
                name: "SettedPrice",
                table: "SimpleSaleProducts",
                type: "money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Products",
                type: "money",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
