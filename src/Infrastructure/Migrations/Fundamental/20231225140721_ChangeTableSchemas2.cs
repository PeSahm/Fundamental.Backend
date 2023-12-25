#nullable disable

using Fundamental.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Fundamental.Migrations.Fundamental
{
    [DbContext(typeof(FundamentalDbContext))]
    [Migration("20231225140721_ChangeTableSchemas2")]
    public class ChangeTableSchemas2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BalanceSheet",
                schema: "sort",
                table: "BalanceSheet");

            migrationBuilder.EnsureSchema(
                name: "fundamental");

            migrationBuilder.RenameTable(
                name: "BalanceSheet",
                schema: "sort",
                newName: "BalanceSheetSort",
                newSchema: "fundamental");

            migrationBuilder.RenameIndex(
                name: "IX_BalanceSheet_Order",
                schema: "fundamental",
                table: "BalanceSheetSort",
                newName: "IX_BalanceSheetSort_Order");

            migrationBuilder.RenameIndex(
                name: "IX_BalanceSheet_Id",
                schema: "fundamental",
                table: "BalanceSheetSort",
                newName: "IX_BalanceSheetSort_Id");

            migrationBuilder.RenameIndex(
                name: "IX_BalanceSheet_Category_CodalRow",
                schema: "fundamental",
                table: "BalanceSheetSort",
                newName: "IX_BalanceSheetSort_Category_CodalRow");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BalanceSheetSort",
                schema: "fundamental",
                table: "BalanceSheetSort",
                column: "_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_BalanceSheetSort",
                schema: "fundamental",
                table: "BalanceSheetSort");

            migrationBuilder.EnsureSchema(
                name: "sort");

            migrationBuilder.RenameTable(
                name: "BalanceSheetSort",
                schema: "fundamental",
                newName: "BalanceSheet",
                newSchema: "sort");

            migrationBuilder.RenameIndex(
                name: "IX_BalanceSheetSort_Order",
                schema: "sort",
                table: "BalanceSheet",
                newName: "IX_BalanceSheet_Order");

            migrationBuilder.RenameIndex(
                name: "IX_BalanceSheetSort_Id",
                schema: "sort",
                table: "BalanceSheet",
                newName: "IX_BalanceSheet_Id");

            migrationBuilder.RenameIndex(
                name: "IX_BalanceSheetSort_Category_CodalRow",
                schema: "sort",
                table: "BalanceSheet",
                newName: "IX_BalanceSheet_Category_CodalRow");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BalanceSheet",
                schema: "sort",
                table: "BalanceSheet",
                column: "_id");
        }
    }
}