using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Optica_Gaido.Data.Migrations
{
    /// <inheritdoc />
    public partial class IsActiveAndDeletedAt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Sellers",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Providers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "PaymentMethods",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Materials",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "HealthInsurances",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "GlassFormats",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Frames",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Doctors",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Clients",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Brands",
                type: "bit",
                nullable: false,
                defaultValue: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Sellers");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Providers");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "PaymentMethods");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Materials");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "HealthInsurances");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "GlassFormats");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Frames");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Brands");
        }
    }
}
