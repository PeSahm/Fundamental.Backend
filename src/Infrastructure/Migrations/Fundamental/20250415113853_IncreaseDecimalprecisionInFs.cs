using System.Collections.Generic;
using Fundamental.Domain.Codals.Manufacturing.Enums;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fundamental.Migrations.Fundamental
{
    /// <inheritdoc />
    public partial class IncreaseDecimalprecisionInFs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<List<NoneOperationalIncomeTag>>(
                name: "tags",
                schema: "manufacturing",
                table: "non-operation-income-expense",
                type: "none_operational_income_tag[]",
                nullable: false,
                defaultValue: new List<NoneOperationalIncomeTag>(),
                oldClrType: typeof(List<NoneOperationalIncomeTag>),
                oldType: "none_operational_income_tag[]",
                oldDefaultValue: new List<NoneOperationalIncomeTag>());

            migrationBuilder.AlterColumn<decimal>(
                name: "this_period_sale_ratio_with_last_year",
                schema: "manufacturing",
                table: "financial-statement",
                type: "numeric(36,10)",
                precision: 36,
                scale: 10,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)",
                oldPrecision: 18,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "this_period_sale_ratio",
                schema: "manufacturing",
                table: "financial-statement",
                type: "numeric(36,10)",
                precision: 36,
                scale: 10,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)",
                oldPrecision: 18,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "target_price",
                schema: "manufacturing",
                table: "financial-statement",
                type: "numeric(36,10)",
                precision: 36,
                scale: 10,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)",
                oldPrecision: 18,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "target_market_value",
                schema: "manufacturing",
                table: "financial-statement",
                type: "numeric(36,10)",
                precision: 36,
                scale: 10,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)",
                oldPrecision: 18,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "receivable_ratio",
                schema: "manufacturing",
                table: "financial-statement",
                type: "numeric(36,10)",
                precision: 36,
                scale: 10,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)",
                oldPrecision: 18,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "ps",
                schema: "manufacturing",
                table: "financial-statement",
                type: "numeric(36,10)",
                precision: 36,
                scale: 10,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)",
                oldPrecision: 18,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "peg",
                schema: "manufacturing",
                table: "financial-statement",
                type: "numeric(36,10)",
                precision: 36,
                scale: 10,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)",
                oldPrecision: 18,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "pe",
                schema: "manufacturing",
                table: "financial-statement",
                type: "numeric(36,10)",
                precision: 36,
                scale: 10,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)",
                oldPrecision: 18,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "pb",
                schema: "manufacturing",
                table: "financial-statement",
                type: "numeric(36,10)",
                precision: 36,
                scale: 10,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)",
                oldPrecision: 18,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "pa",
                schema: "manufacturing",
                table: "financial-statement",
                type: "numeric(36,10)",
                precision: 36,
                scale: 10,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)",
                oldPrecision: 18,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "owners_equity_ratio",
                schema: "manufacturing",
                table: "financial-statement",
                type: "numeric(36,10)",
                precision: 36,
                scale: 10,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)",
                oldPrecision: 18,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "optimal_buy_price",
                schema: "manufacturing",
                table: "financial-statement",
                type: "numeric(36,10)",
                precision: 36,
                scale: 10,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)",
                oldPrecision: 18,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "operational_margin",
                schema: "manufacturing",
                table: "financial-statement",
                type: "numeric(36,10)",
                precision: 36,
                scale: 10,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)",
                oldPrecision: 18,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "net_profit_growth_ratio",
                schema: "manufacturing",
                table: "financial-statement",
                type: "numeric(36,10)",
                precision: 36,
                scale: 10,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)",
                oldPrecision: 18,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "net_margin",
                schema: "manufacturing",
                table: "financial-statement",
                type: "numeric(36,10)",
                precision: 36,
                scale: 10,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,4)",
                oldPrecision: 18,
                oldScale: 4);

            migrationBuilder.AlterColumn<decimal>(
                name: "market_value",
                schema: "manufacturing",
                table: "financial-statement",
                type: "numeric(36,10)",
                precision: 36,
                scale: 10,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)",
                oldPrecision: 18,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "market_cap",
                schema: "manufacturing",
                table: "financial-statement",
                type: "numeric(36,10)",
                precision: 36,
                scale: 10,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)",
                oldPrecision: 18,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "last_close_price",
                schema: "manufacturing",
                table: "financial-statement",
                type: "numeric(36,10)",
                precision: 36,
                scale: 10,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)",
                oldPrecision: 18,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "gross_margin",
                schema: "manufacturing",
                table: "financial-statement",
                type: "numeric(36,10)",
                precision: 36,
                scale: 10,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)",
                oldPrecision: 18,
                oldScale: 2);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<List<NoneOperationalIncomeTag>>(
                name: "tags",
                schema: "manufacturing",
                table: "non-operation-income-expense",
                type: "none_operational_income_tag[]",
                nullable: false,
                defaultValue: new List<NoneOperationalIncomeTag>(),
                oldClrType: typeof(List<NoneOperationalIncomeTag>),
                oldType: "none_operational_income_tag[]",
                oldDefaultValue: new List<NoneOperationalIncomeTag>());

            migrationBuilder.AlterColumn<decimal>(
                name: "this_period_sale_ratio_with_last_year",
                schema: "manufacturing",
                table: "financial-statement",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(36,10)",
                oldPrecision: 36,
                oldScale: 10);

            migrationBuilder.AlterColumn<decimal>(
                name: "this_period_sale_ratio",
                schema: "manufacturing",
                table: "financial-statement",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(36,10)",
                oldPrecision: 36,
                oldScale: 10);

            migrationBuilder.AlterColumn<decimal>(
                name: "target_price",
                schema: "manufacturing",
                table: "financial-statement",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(36,10)",
                oldPrecision: 36,
                oldScale: 10);

            migrationBuilder.AlterColumn<decimal>(
                name: "target_market_value",
                schema: "manufacturing",
                table: "financial-statement",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(36,10)",
                oldPrecision: 36,
                oldScale: 10);

            migrationBuilder.AlterColumn<decimal>(
                name: "receivable_ratio",
                schema: "manufacturing",
                table: "financial-statement",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(36,10)",
                oldPrecision: 36,
                oldScale: 10);

            migrationBuilder.AlterColumn<decimal>(
                name: "ps",
                schema: "manufacturing",
                table: "financial-statement",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(36,10)",
                oldPrecision: 36,
                oldScale: 10);

            migrationBuilder.AlterColumn<decimal>(
                name: "peg",
                schema: "manufacturing",
                table: "financial-statement",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(36,10)",
                oldPrecision: 36,
                oldScale: 10);

            migrationBuilder.AlterColumn<decimal>(
                name: "pe",
                schema: "manufacturing",
                table: "financial-statement",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(36,10)",
                oldPrecision: 36,
                oldScale: 10);

            migrationBuilder.AlterColumn<decimal>(
                name: "pb",
                schema: "manufacturing",
                table: "financial-statement",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(36,10)",
                oldPrecision: 36,
                oldScale: 10);

            migrationBuilder.AlterColumn<decimal>(
                name: "pa",
                schema: "manufacturing",
                table: "financial-statement",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(36,10)",
                oldPrecision: 36,
                oldScale: 10);

            migrationBuilder.AlterColumn<decimal>(
                name: "owners_equity_ratio",
                schema: "manufacturing",
                table: "financial-statement",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(36,10)",
                oldPrecision: 36,
                oldScale: 10);

            migrationBuilder.AlterColumn<decimal>(
                name: "optimal_buy_price",
                schema: "manufacturing",
                table: "financial-statement",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(36,10)",
                oldPrecision: 36,
                oldScale: 10);

            migrationBuilder.AlterColumn<decimal>(
                name: "operational_margin",
                schema: "manufacturing",
                table: "financial-statement",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(36,10)",
                oldPrecision: 36,
                oldScale: 10);

            migrationBuilder.AlterColumn<decimal>(
                name: "net_profit_growth_ratio",
                schema: "manufacturing",
                table: "financial-statement",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(36,10)",
                oldPrecision: 36,
                oldScale: 10);

            migrationBuilder.AlterColumn<decimal>(
                name: "net_margin",
                schema: "manufacturing",
                table: "financial-statement",
                type: "numeric(18,4)",
                precision: 18,
                scale: 4,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(36,10)",
                oldPrecision: 36,
                oldScale: 10);

            migrationBuilder.AlterColumn<decimal>(
                name: "market_value",
                schema: "manufacturing",
                table: "financial-statement",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(36,10)",
                oldPrecision: 36,
                oldScale: 10);

            migrationBuilder.AlterColumn<decimal>(
                name: "market_cap",
                schema: "manufacturing",
                table: "financial-statement",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(36,10)",
                oldPrecision: 36,
                oldScale: 10);

            migrationBuilder.AlterColumn<decimal>(
                name: "last_close_price",
                schema: "manufacturing",
                table: "financial-statement",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(36,10)",
                oldPrecision: 36,
                oldScale: 10);

            migrationBuilder.AlterColumn<decimal>(
                name: "gross_margin",
                schema: "manufacturing",
                table: "financial-statement",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(36,10)",
                oldPrecision: 36,
                oldScale: 10);
        }
    }
}
