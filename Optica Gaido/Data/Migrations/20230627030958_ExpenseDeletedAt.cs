using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Optica_Gaido.Data.Migrations
{
    /// <inheritdoc />
    public partial class ExpenseDeletedAt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Expenses",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Expenses");
        }
    }
}
