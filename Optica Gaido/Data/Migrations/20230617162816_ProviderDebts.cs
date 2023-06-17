using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Optica_Gaido.Data.Migrations
{
    /// <inheritdoc />
    public partial class ProviderDebts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Debts",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProviderID = table.Column<long>(type: "bigint", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Price = table.Column<decimal>(type: "money", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Debts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Debts_Providers_ProviderID",
                        column: x => x.ProviderID,
                        principalTable: "Providers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DebtPayments",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DebtID = table.Column<long>(type: "bigint", nullable: false),
                    Amount = table.Column<decimal>(type: "money", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DebtPayments", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DebtPayments_Debts_DebtID",
                        column: x => x.DebtID,
                        principalTable: "Debts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "GlassColors",
                keyColumn: "ID",
                keyValue: 4L,
                column: "Name",
                value: "Hih H. Lite Blanco");

            migrationBuilder.UpdateData(
                table: "GlassColors",
                keyColumn: "ID",
                keyValue: 5L,
                column: "Name",
                value: "Hih H. Lite Sepia");

            migrationBuilder.CreateIndex(
                name: "IX_DebtPayments_DebtID",
                table: "DebtPayments",
                column: "DebtID");

            migrationBuilder.CreateIndex(
                name: "IX_Debts_ProviderID",
                table: "Debts",
                column: "ProviderID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DebtPayments");

            migrationBuilder.DropTable(
                name: "Debts");

            migrationBuilder.UpdateData(
                table: "GlassColors",
                keyColumn: "ID",
                keyValue: 4L,
                column: "Name",
                value: "Hig H. Lite Blanco");

            migrationBuilder.UpdateData(
                table: "GlassColors",
                keyColumn: "ID",
                keyValue: 5L,
                column: "Name",
                value: "Hig H. Lite Sepia");
        }
    }
}
