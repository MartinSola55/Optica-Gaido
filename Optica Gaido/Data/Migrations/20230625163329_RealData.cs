using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Optica_Gaido.Data.Migrations
{
    /// <inheritdoc />
    public partial class RealData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PaymentMethods",
                keyColumn: "ID",
                keyValue: (short)6);

            migrationBuilder.DropColumn(
                name: "Surname",
                table: "Providers");

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "ID",
                keyValue: 1L,
                column: "Name",
                value: "Humah");

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "ID",
                keyValue: 3L,
                column: "Name",
                value: "Rusty");

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "ID",
                keyValue: 4L,
                column: "Name",
                value: "Hardem");

            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "ID", "IsActive", "Name" },
                values: new object[,]
                {
                    { 5L, true, "Nivel Uno" },
                    { 6L, true, "Mistral" },
                    { 7L, true, "Prototype" },
                    { 8L, true, "Prune" },
                    { 9L, true, "Dotan Vision" },
                    { 10L, true, "Union Pacific - Tiffany" }
                });

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "ID",
                keyValue: 1L,
                columns: new[] { "License", "Surname" },
                values: new object[] { "5981", "Roman" });

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "ID",
                keyValue: 2L,
                columns: new[] { "License", "Name", "Surname" },
                values: new object[] { "11095", "Fabiani", "Rosana" });

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "ID",
                keyValue: 3L,
                columns: new[] { "License", "Name", "Surname" },
                values: new object[] { "4165", "Casabianca", "Gonzalo" });

            migrationBuilder.InsertData(
                table: "Doctors",
                columns: new[] { "ID", "IsActive", "License", "Name", "Surname" },
                values: new object[,]
                {
                    { 4L, true, "8440", "Lopez", "German" },
                    { 5L, true, "22251", "Dominguez", "Jose Luis" }
                });

            migrationBuilder.UpdateData(
                table: "HealthInsurances",
                keyColumn: "ID",
                keyValue: (short)4,
                column: "Name",
                value: "JERÁRQUICO SALUD");

            migrationBuilder.InsertData(
                table: "HealthInsurances",
                columns: new[] { "ID", "IsActive", "Name" },
                values: new object[,]
                {
                    { (short)5, true, "SANCOR" },
                    { (short)6, true, "CAJA INGENIERÍA" },
                    { (short)7, true, "OSPAC" },
                    { (short)8, true, "OSPIL - CAJA AYUDA MUTTUA" },
                    { (short)9, true, "UNL" }
                });

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "ID",
                keyValue: (short)3,
                column: "Name",
                value: "Tarjetas Nacionales");

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "ID",
                keyValue: (short)4,
                column: "Name",
                value: "Tarjeta Mutual Central");

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "ID",
                keyValue: (short)5,
                column: "Name",
                value: "Tarjeta Mutual Argentino");

            migrationBuilder.UpdateData(
                table: "Providers",
                keyColumn: "ID",
                keyValue: 1L,
                column: "Name",
                value: "Humah");

            migrationBuilder.UpdateData(
                table: "Providers",
                keyColumn: "ID",
                keyValue: 2L,
                column: "Name",
                value: "Vulk");

            migrationBuilder.UpdateData(
                table: "Providers",
                keyColumn: "ID",
                keyValue: 3L,
                column: "Name",
                value: "Rusty");

            migrationBuilder.UpdateData(
                table: "Providers",
                keyColumn: "ID",
                keyValue: 4L,
                column: "Name",
                value: "Hardem");

            migrationBuilder.InsertData(
                table: "Providers",
                columns: new[] { "ID", "DeletedAt", "Name" },
                values: new object[,]
                {
                    { 5L, null, "Nivel Uno" },
                    { 6L, null, "Vision Planet" },
                    { 7L, null, "Dotan Vision" },
                    { 8L, null, "Union Pacific - Tiffany" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "ID",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "ID",
                keyValue: 6L);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "ID",
                keyValue: 7L);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "ID",
                keyValue: 8L);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "ID",
                keyValue: 9L);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "ID",
                keyValue: 10L);

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "ID",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "Doctors",
                keyColumn: "ID",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "HealthInsurances",
                keyColumn: "ID",
                keyValue: (short)5);

            migrationBuilder.DeleteData(
                table: "HealthInsurances",
                keyColumn: "ID",
                keyValue: (short)6);

            migrationBuilder.DeleteData(
                table: "HealthInsurances",
                keyColumn: "ID",
                keyValue: (short)7);

            migrationBuilder.DeleteData(
                table: "HealthInsurances",
                keyColumn: "ID",
                keyValue: (short)8);

            migrationBuilder.DeleteData(
                table: "HealthInsurances",
                keyColumn: "ID",
                keyValue: (short)9);

            migrationBuilder.DeleteData(
                table: "Providers",
                keyColumn: "ID",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "Providers",
                keyColumn: "ID",
                keyValue: 6L);

            migrationBuilder.DeleteData(
                table: "Providers",
                keyColumn: "ID",
                keyValue: 7L);

            migrationBuilder.DeleteData(
                table: "Providers",
                keyColumn: "ID",
                keyValue: 8L);

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "ID",
                keyValue: 1L,
                column: "Name",
                value: "RayBan");

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "ID",
                keyValue: 3L,
                column: "Name",
                value: "UP!");

            migrationBuilder.UpdateData(
                table: "Brands",
                keyColumn: "ID",
                keyValue: 4L,
                column: "Name",
                value: "Oakley");

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "ID",
                keyValue: 1L,
                columns: new[] { "License", "Surname" },
                values: new object[] { "123123", "Sola" });

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "ID",
                keyValue: 2L,
                columns: new[] { "License", "Name", "Surname" },
                values: new object[] { "456456", "Vanina", "Chalita" });

            migrationBuilder.UpdateData(
                table: "Doctors",
                keyColumn: "ID",
                keyValue: 3L,
                columns: new[] { "License", "Name", "Surname" },
                values: new object[] { "789789", "Ana María", "Bertinat" });

            migrationBuilder.UpdateData(
                table: "HealthInsurances",
                keyColumn: "ID",
                keyValue: (short)4,
                column: "Name",
                value: "OSECAC");

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "ID",
                keyValue: (short)3,
                column: "Name",
                value: "Débito");

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "ID",
                keyValue: (short)4,
                column: "Name",
                value: "Débito (local)");

            migrationBuilder.UpdateData(
                table: "PaymentMethods",
                keyColumn: "ID",
                keyValue: (short)5,
                column: "Name",
                value: "Crédito");

            migrationBuilder.InsertData(
                table: "PaymentMethods",
                columns: new[] { "ID", "IsActive", "Name" },
                values: new object[] { (short)6, true, "Crédito (local)" });

            migrationBuilder.UpdateData(
                table: "Providers",
                keyColumn: "ID",
                keyValue: 1L,
                columns: new[] { "Name", "Surname" },
                values: new object[] { "Lionel Andrés", "Messi" });

            migrationBuilder.UpdateData(
                table: "Providers",
                keyColumn: "ID",
                keyValue: 2L,
                columns: new[] { "Name", "Surname" },
                values: new object[] { "Andrés", "Iniesta" });

            migrationBuilder.UpdateData(
                table: "Providers",
                keyColumn: "ID",
                keyValue: 3L,
                columns: new[] { "Name", "Surname" },
                values: new object[] { "Gabriel", "Batistuta" });

            migrationBuilder.UpdateData(
                table: "Providers",
                keyColumn: "ID",
                keyValue: 4L,
                columns: new[] { "Name", "Surname" },
                values: new object[] { "Diego Armando", "Maradona" });
        }
    }
}
