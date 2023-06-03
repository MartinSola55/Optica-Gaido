using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Optica_Gaido.Data.Migrations
{
    /// <inheritdoc />
    public partial class SaleGlassFormat_Changed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GlassFormatSale");

            migrationBuilder.DropTable(
                name: "SaleGlassFormats");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Sales",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "SaleID",
                table: "GlassFormats",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_GlassFormats_SaleID",
                table: "GlassFormats",
                column: "SaleID");

            migrationBuilder.AddForeignKey(
                name: "FK_GlassFormats_Sales_SaleID",
                table: "GlassFormats",
                column: "SaleID",
                principalTable: "Sales",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GlassFormats_Sales_SaleID",
                table: "GlassFormats");

            migrationBuilder.DropIndex(
                name: "IX_GlassFormats_SaleID",
                table: "GlassFormats");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "SaleID",
                table: "GlassFormats");

            migrationBuilder.CreateTable(
                name: "GlassFormatSale",
                columns: table => new
                {
                    GlassFormatsID = table.Column<long>(type: "bigint", nullable: false),
                    SalesID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GlassFormatSale", x => new { x.GlassFormatsID, x.SalesID });
                    table.ForeignKey(
                        name: "FK_GlassFormatSale_GlassFormats_GlassFormatsID",
                        column: x => x.GlassFormatsID,
                        principalTable: "GlassFormats",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GlassFormatSale_Sales_SalesID",
                        column: x => x.SalesID,
                        principalTable: "Sales",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SaleGlassFormats",
                columns: table => new
                {
                    SaleID = table.Column<long>(type: "bigint", nullable: false),
                    GlassFormatID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleGlassFormats", x => new { x.SaleID, x.GlassFormatID });
                    table.ForeignKey(
                        name: "FK_SaleGlassFormats_GlassFormats_GlassFormatID",
                        column: x => x.GlassFormatID,
                        principalTable: "GlassFormats",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SaleGlassFormats_Sales_SaleID",
                        column: x => x.SaleID,
                        principalTable: "Sales",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GlassFormatSale_SalesID",
                table: "GlassFormatSale",
                column: "SalesID");

            migrationBuilder.CreateIndex(
                name: "IX_SaleGlassFormats_GlassFormatID",
                table: "SaleGlassFormats",
                column: "GlassFormatID");
        }
    }
}
