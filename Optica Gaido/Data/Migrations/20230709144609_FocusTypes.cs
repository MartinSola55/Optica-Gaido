using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Optica_Gaido.Data.Migrations
{
    /// <inheritdoc />
    public partial class FocusTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "GlassFocusTypeID",
                table: "Sales",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "GlassFocusTypes",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GlassFocusTypes", x => x.ID);
                });

            migrationBuilder.InsertData(
                table: "GlassFocusTypes",
                columns: new[] { "ID", "Name" },
                values: new object[,]
                {
                    { 1L, "Monofocal" },
                    { 2L, "Bifocal" },
                    { 3L, "Multifocal" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sales_GlassFocusTypeID",
                table: "Sales",
                column: "GlassFocusTypeID");

            migrationBuilder.AddForeignKey(
                name: "FK_Sales_GlassFocusTypes_GlassFocusTypeID",
                table: "Sales",
                column: "GlassFocusTypeID",
                principalTable: "GlassFocusTypes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sales_GlassFocusTypes_GlassFocusTypeID",
                table: "Sales");

            migrationBuilder.DropTable(
                name: "GlassFocusTypes");

            migrationBuilder.DropIndex(
                name: "IX_Sales_GlassFocusTypeID",
                table: "Sales");

            migrationBuilder.DropColumn(
                name: "GlassFocusTypeID",
                table: "Sales");
        }
    }
}
