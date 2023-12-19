#nullable disable

using Fundamental.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Fundamental.Migrations.Fundamental
{
    [DbContext(typeof(FundamentalDbContext))]
    [Migration("20231218064313_SomeRowColumnsInBalanceSheet")]
    public class SomeRowColumnsInBalanceSheet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CodalCategory",
                schema: "fs",
                table: "BalanceSheet",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CodalRow",
                schema: "fs",
                table: "BalanceSheet",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CodalCategory",
                schema: "fs",
                table: "BalanceSheet");

            migrationBuilder.DropColumn(
                name: "CodalRow",
                schema: "fs",
                table: "BalanceSheet");
        }
    }
}