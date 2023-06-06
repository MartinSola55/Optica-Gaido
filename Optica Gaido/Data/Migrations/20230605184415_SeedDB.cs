using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Optica_Gaido.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "ID", "IsActive", "Name" },
                values: new object[,]
                {
                    { 1L, true, "RayBan" },
                    { 2L, true, "Vulk" },
                    { 3L, true, "UP!" },
                    { 4L, true, "Oakley" }
                });

            migrationBuilder.InsertData(
                table: "Doctors",
                columns: new[] { "ID", "IsActive", "License", "Name", "Surname" },
                values: new object[,]
                {
                    { 1L, true, "123123", "Fernando", "Sola" },
                    { 2L, true, "456456", "Vanina", "Chalita" },
                    { 3L, true, "789789", "Ana María", "Bertinat" }
                });

            migrationBuilder.InsertData(
                table: "GlassColors",
                columns: new[] { "ID", "Name" },
                values: new object[,]
                {
                    { 1L, "Blanco" },
                    { 2L, "Tinte 50" },
                    { 3L, "Foto Grey" },
                    { 4L, "Hig H. Lite Blanco" },
                    { 5L, "Hig H. Lite Sepia" }
                });

            migrationBuilder.InsertData(
                table: "GlassTypes",
                columns: new[] { "ID", "Name" },
                values: new object[,]
                {
                    { 1L, "Orgánico" },
                    { 2L, "Mineral" }
                });

            migrationBuilder.InsertData(
                table: "HealthInsurances",
                columns: new[] { "ID", "IsActive", "Name" },
                values: new object[,]
                {
                    { (short)1, true, "IAPOS" },
                    { (short)2, true, "OSDE" },
                    { (short)3, true, "PAMI" },
                    { (short)4, true, "OSECAC" }
                });

            migrationBuilder.InsertData(
                table: "Materials",
                columns: new[] { "ID", "Description", "IsActive" },
                values: new object[,]
                {
                    { 1L, "Metal", true },
                    { 2L, "Plástico", true },
                    { 3L, "Madera", true }
                });

            migrationBuilder.InsertData(
                table: "PaymentMethods",
                columns: new[] { "ID", "IsActive", "Name" },
                values: new object[,]
                {
                    { (short)1, true, "Efectivo" },
                    { (short)2, true, "Transferencia" },
                    { (short)3, true, "Débito" },
                    { (short)4, true, "Débito (local)" },
                    { (short)5, true, "Crédito" },
                    { (short)6, true, "Crédito (local)" }
                });

            migrationBuilder.InsertData(
                table: "Providers",
                columns: new[] { "ID", "DeletedAt", "Name", "Surname" },
                values: new object[,]
                {
                    { 1L, null, "Lionel Andrés", "Messi" },
                    { 2L, null, "Andrés", "Iniesta" },
                    { 3L, null, "Gabriel", "Batistuta" },
                    { 4L, null, "Diego Armando", "Maradona" }
                });

            migrationBuilder.InsertData(
                table: "Sellers",
                columns: new[] { "ID", "IsActive", "Name", "Surname" },
                values: new object[,]
                {
                    { (short)1, true, "Gina", "Gaido" },
                    { (short)2, true, "Lucy", "Gaido" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "ID",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "ID",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "ID",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "ID",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "ID",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "ID",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "ID",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "GlassColors",
                keyColumn: "ID",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "GlassColors",
                keyColumn: "ID",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "GlassColors",
                keyColumn: "ID",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "GlassColors",
                keyColumn: "ID",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "GlassColors",
                keyColumn: "ID",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "GlassTypes",
                keyColumn: "ID",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "GlassTypes",
                keyColumn: "ID",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "HealthInsurances",
                keyColumn: "ID",
                keyValue: (short)1);

            migrationBuilder.DeleteData(
                table: "HealthInsurances",
                keyColumn: "ID",
                keyValue: (short)2);

            migrationBuilder.DeleteData(
                table: "HealthInsurances",
                keyColumn: "ID",
                keyValue: (short)3);

            migrationBuilder.DeleteData(
                table: "HealthInsurances",
                keyColumn: "ID",
                keyValue: (short)4);

            migrationBuilder.DeleteData(
                table: "Materials",
                keyColumn: "ID",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Materials",
                keyColumn: "ID",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Materials",
                keyColumn: "ID",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "PaymentMethods",
                keyColumn: "ID",
                keyValue: (short)1);

            migrationBuilder.DeleteData(
                table: "PaymentMethods",
                keyColumn: "ID",
                keyValue: (short)2);

            migrationBuilder.DeleteData(
                table: "PaymentMethods",
                keyColumn: "ID",
                keyValue: (short)3);

            migrationBuilder.DeleteData(
                table: "PaymentMethods",
                keyColumn: "ID",
                keyValue: (short)4);

            migrationBuilder.DeleteData(
                table: "PaymentMethods",
                keyColumn: "ID",
                keyValue: (short)5);

            migrationBuilder.DeleteData(
                table: "PaymentMethods",
                keyColumn: "ID",
                keyValue: (short)6);

            migrationBuilder.DeleteData(
                table: "Providers",
                keyColumn: "ID",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Providers",
                keyColumn: "ID",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Providers",
                keyColumn: "ID",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "Providers",
                keyColumn: "ID",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "Sellers",
                keyColumn: "ID",
                keyValue: (short)1);

            migrationBuilder.DeleteData(
                table: "Sellers",
                keyColumn: "ID",
                keyValue: (short)2);
        }
    }
}
