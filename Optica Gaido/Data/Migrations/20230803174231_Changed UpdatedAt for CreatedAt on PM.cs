using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Optica_Gaido.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangedUpdatedAtforCreatedAtonPM : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SalePaymentMethods",
                table: "SalePaymentMethods");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "SalePaymentMethods",
                newName: "CreatedAt");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SalePaymentMethods",
                table: "SalePaymentMethods",
                columns: new[] { "SaleID", "PaymentMethodID", "CreatedAt" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SalePaymentMethods",
                table: "SalePaymentMethods");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "SalePaymentMethods",
                newName: "UpdatedAt");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SalePaymentMethods",
                table: "SalePaymentMethods",
                columns: new[] { "SaleID", "PaymentMethodID" });
        }
    }
}
