using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Optica_Gaido.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddFrame : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "FrameID",
                table: "Sales",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

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
                name: "IX_Sales_FrameID",
                table: "Sales",
                column: "FrameID");

            migrationBuilder.CreateIndex(
                name: "IX_SaleGlassFormats_GlassFormatID",
                table: "SaleGlassFormats",
                column: "GlassFormatID");

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_Frames_FrameID",
                table: "Sales",
                column: "FrameID",
                principalTable: "Frames",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sales_Frames_FrameID",
                table: "Sales");

            migrationBuilder.DropTable(
                name: "SaleGlassFormats");

            migrationBuilder.DropIndex(
                name: "IX_Sales_FrameID",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "FrameID",
                table: "Sales");
        }
    }
}
