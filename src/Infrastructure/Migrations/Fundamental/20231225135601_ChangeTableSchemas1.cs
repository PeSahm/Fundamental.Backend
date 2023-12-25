#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace Fundamental.Migrations.Fundamental
{
    /// <inheritdoc />
    public partial class ChangeTableSchemas1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "manufacturing");

            migrationBuilder.RenameTable(
                name: "NonOperationIncomeAndExpense",
                schema: "fs",
                newName: "NonOperationIncomeAndExpense",
                newSchema: "manufacturing");

            migrationBuilder.RenameTable(
                name: "MonthlyActivities",
                schema: "fs",
                newName: "MonthlyActivities",
                newSchema: "manufacturing");

            migrationBuilder.RenameTable(
                name: "IncomeStatement",
                schema: "fs",
                newName: "IncomeStatement",
                newSchema: "manufacturing");

            migrationBuilder.RenameTable(
                name: "BalanceSheet",
                schema: "fs",
                newName: "BalanceSheet",
                newSchema: "manufacturing");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "NonOperationIncomeAndExpense",
                schema: "manufacturing",
                newName: "NonOperationIncomeAndExpense",
                newSchema: "fs");

            migrationBuilder.RenameTable(
                name: "MonthlyActivities",
                schema: "manufacturing",
                newName: "MonthlyActivities",
                newSchema: "fs");

            migrationBuilder.RenameTable(
                name: "IncomeStatement",
                schema: "manufacturing",
                newName: "IncomeStatement",
                newSchema: "fs");

            migrationBuilder.RenameTable(
                name: "BalanceSheet",
                schema: "manufacturing",
                newName: "BalanceSheet",
                newSchema: "fs");
        }
    }
}