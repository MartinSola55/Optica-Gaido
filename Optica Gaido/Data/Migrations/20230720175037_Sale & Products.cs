using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Optica_Gaido.Data.Migrations
{
    /// <inheritdoc />
    public partial class SaleProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Price = table.Column<decimal>(type: "money", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SimpleSales",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientID = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SimpleSales", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SimpleSales_Clients_ClientID",
                        column: x => x.ClientID,
                        principalTable: "Clients",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "SimpleSalePaymentMethod",
                columns: table => new
                {
                    SimpleSaleID = table.Column<long>(type: "bigint", nullable: false),
                    PaymentMethodID = table.Column<short>(type: "smallint", nullable: false),
                    Amount = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SimpleSalePaymentMethod", x => new { x.SimpleSaleID, x.PaymentMethodID });
                    table.ForeignKey(
                        name: "FK_SimpleSalePaymentMethod_PaymentMethods_PaymentMethodID",
                        column: x => x.PaymentMethodID,
                        principalTable: "PaymentMethods",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SimpleSalePaymentMethod_SimpleSales_SimpleSaleID",
                        column: x => x.SimpleSaleID,
                        principalTable: "SimpleSales",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SimpleSaleProducts",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SimpleSaleID = table.Column<long>(type: "bigint", nullable: false),
                    ProductID = table.Column<long>(type: "bigint", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    SettedPrice = table.Column<decimal>(type: "money", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SimpleSaleProducts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SimpleSaleProducts_SimpleSales_SimpleSaleID",
                        column: x => x.SimpleSaleID,
                        principalTable: "SimpleSales",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SimpleSalePaymentMethod_PaymentMethodID",
                table: "SimpleSalePaymentMethod",
                column: "PaymentMethodID");

            migrationBuilder.CreateIndex(
                name: "IX_SimpleSaleProducts_SimpleSaleID",
                table: "SimpleSaleProducts",
                column: "SimpleSaleID");

            migrationBuilder.CreateIndex(
                name: "IX_SimpleSales_ClientID",
                table: "SimpleSales",
                column: "ClientID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "SimpleSalePaymentMethod");

            migrationBuilder.DropTable(
                name: "SimpleSaleProducts");

            migrationBuilder.DropTable(
                name: "SimpleSales");
        }
    }
}
