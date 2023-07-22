using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Optica_Gaido.Data.Migrations
{
    /// <inheritdoc />
    public partial class ClientsDeletedAt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Clients",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SimpleSaleProducts_ProductID",
                table: "SimpleSaleProducts",
                column: "ProductID");

            migrationBuilder.AddForeignKey(
                name: "FK_SimpleSaleProducts_Products_ProductID",
                table: "SimpleSaleProducts",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SimpleSaleProducts_Products_ProductID",
                table: "SimpleSaleProducts");

            migrationBuilder.DropIndex(
                name: "IX_SimpleSaleProducts_ProductID",
                table: "SimpleSaleProducts");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Clients");
        }
    }
}
