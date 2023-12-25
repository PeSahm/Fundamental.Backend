#nullable disable

using Fundamental.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Fundamental.Migrations.Fundamental
{
    [DbContext(typeof(FundamentalDbContext))]
    [Migration("20231225071820_ChangeInBalanceSheetTable")]
    public class ChangeInBalanceSheetTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<short>(
                name: "Row",
                schema: "fs",
                table: "BalanceSheet",
                type: "SMALLINT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<short>(
                name: "CodalRow",
                schema: "fs",
                table: "BalanceSheet",
                type: "SMALLINT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<short>(
                name: "CodalCategory",
                schema: "fs",
                table: "BalanceSheet",
                type: "SMALLINT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Row",
                schema: "fs",
                table: "BalanceSheet",
                type: "int",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "SMALLINT");

            migrationBuilder.AlterColumn<int>(
                name: "CodalRow",
                schema: "fs",
                table: "BalanceSheet",
                type: "int",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "SMALLINT");

            migrationBuilder.AlterColumn<int>(
                name: "CodalCategory",
                schema: "fs",
                table: "BalanceSheet",
                type: "int",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "SMALLINT");
        }
    }
}