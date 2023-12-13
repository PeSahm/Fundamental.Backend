#nullable disable

using Fundamental.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Fundamental.Migrations.Fundamental
{
    [DbContext(typeof(FundamentalDbContext))]
    [Migration("20231210103544_IncreaseDecimalScaleForStatement")]
    public class IncreaseDecimalScaleForStatement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Receivables",
                schema: "fs",
                table: "FinancialStatements",
                type: "decimal(36,10)",
                precision: 36,
                scale: 10,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,3)",
                oldPrecision: 18,
                oldScale: 3);

            migrationBuilder.AlterColumn<decimal>(
                name: "OwnersEquity",
                schema: "fs",
                table: "FinancialStatements",
                type: "decimal(36,10)",
                precision: 36,
                scale: 10,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,3)",
                oldPrecision: 18,
                oldScale: 3);

            migrationBuilder.AlterColumn<decimal>(
                name: "OperatingProfit",
                schema: "fs",
                table: "FinancialStatements",
                type: "decimal(36,10)",
                precision: 36,
                scale: 10,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,3)",
                oldPrecision: 18,
                oldScale: 3);

            migrationBuilder.AlterColumn<decimal>(
                name: "OperatingIncome",
                schema: "fs",
                table: "FinancialStatements",
                type: "decimal(36,10)",
                precision: 36,
                scale: 10,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,3)",
                oldPrecision: 18,
                oldScale: 3);

            migrationBuilder.AlterColumn<decimal>(
                name: "NetProfit",
                schema: "fs",
                table: "FinancialStatements",
                type: "decimal(36,10)",
                precision: 36,
                scale: 10,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,3)",
                oldPrecision: 18,
                oldScale: 3);

            migrationBuilder.AlterColumn<decimal>(
                name: "InvestmentIncome",
                schema: "fs",
                table: "FinancialStatements",
                type: "decimal(36,10)",
                precision: 36,
                scale: 10,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,3)",
                oldPrecision: 18,
                oldScale: 3);

            migrationBuilder.AlterColumn<decimal>(
                name: "GrossProfit",
                schema: "fs",
                table: "FinancialStatements",
                type: "decimal(36,10)",
                precision: 36,
                scale: 10,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,3)",
                oldPrecision: 18,
                oldScale: 3);

            migrationBuilder.AlterColumn<decimal>(
                name: "Expense",
                schema: "fs",
                table: "FinancialStatements",
                type: "decimal(36,10)",
                precision: 36,
                scale: 10,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,3)",
                oldPrecision: 18,
                oldScale: 3);

            migrationBuilder.AlterColumn<decimal>(
                name: "BankInterestIncome",
                schema: "fs",
                table: "FinancialStatements",
                type: "decimal(36,10)",
                precision: 36,
                scale: 10,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,3)",
                oldPrecision: 18,
                oldScale: 3);

            migrationBuilder.AlterColumn<decimal>(
                name: "Asset",
                schema: "fs",
                table: "FinancialStatements",
                type: "decimal(36,10)",
                precision: 36,
                scale: 10,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,3)",
                oldPrecision: 18,
                oldScale: 3);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Receivables",
                schema: "fs",
                table: "FinancialStatements",
                type: "decimal(18,3)",
                precision: 18,
                scale: 3,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(36,10)",
                oldPrecision: 36,
                oldScale: 10);

            migrationBuilder.AlterColumn<decimal>(
                name: "OwnersEquity",
                schema: "fs",
                table: "FinancialStatements",
                type: "decimal(18,3)",
                precision: 18,
                scale: 3,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(36,10)",
                oldPrecision: 36,
                oldScale: 10);

            migrationBuilder.AlterColumn<decimal>(
                name: "OperatingProfit",
                schema: "fs",
                table: "FinancialStatements",
                type: "decimal(18,3)",
                precision: 18,
                scale: 3,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(36,10)",
                oldPrecision: 36,
                oldScale: 10);

            migrationBuilder.AlterColumn<decimal>(
                name: "OperatingIncome",
                schema: "fs",
                table: "FinancialStatements",
                type: "decimal(18,3)",
                precision: 18,
                scale: 3,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(36,10)",
                oldPrecision: 36,
                oldScale: 10);

            migrationBuilder.AlterColumn<decimal>(
                name: "NetProfit",
                schema: "fs",
                table: "FinancialStatements",
                type: "decimal(18,3)",
                precision: 18,
                scale: 3,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(36,10)",
                oldPrecision: 36,
                oldScale: 10);

            migrationBuilder.AlterColumn<decimal>(
                name: "InvestmentIncome",
                schema: "fs",
                table: "FinancialStatements",
                type: "decimal(18,3)",
                precision: 18,
                scale: 3,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(36,10)",
                oldPrecision: 36,
                oldScale: 10);

            migrationBuilder.AlterColumn<decimal>(
                name: "GrossProfit",
                schema: "fs",
                table: "FinancialStatements",
                type: "decimal(18,3)",
                precision: 18,
                scale: 3,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(36,10)",
                oldPrecision: 36,
                oldScale: 10);

            migrationBuilder.AlterColumn<decimal>(
                name: "Expense",
                schema: "fs",
                table: "FinancialStatements",
                type: "decimal(18,3)",
                precision: 18,
                scale: 3,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(36,10)",
                oldPrecision: 36,
                oldScale: 10);

            migrationBuilder.AlterColumn<decimal>(
                name: "BankInterestIncome",
                schema: "fs",
                table: "FinancialStatements",
                type: "decimal(18,3)",
                precision: 18,
                scale: 3,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(36,10)",
                oldPrecision: 36,
                oldScale: 10);

            migrationBuilder.AlterColumn<decimal>(
                name: "Asset",
                schema: "fs",
                table: "FinancialStatements",
                type: "decimal(18,3)",
                precision: 18,
                scale: 3,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(36,10)",
                oldPrecision: 36,
                oldScale: 10);
        }
    }
}