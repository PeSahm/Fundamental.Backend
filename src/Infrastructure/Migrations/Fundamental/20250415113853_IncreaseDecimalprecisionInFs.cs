#nullable disable

using Fundamental.Domain.Codals.Manufacturing.Enums;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Fundamental.Migrations.Fundamental;

/// <inheritdoc />
public partial class IncreaseDecimalprecisionInFs : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<List<NoneOperationalIncomeTag>>(
            "tags",
            schema: "manufacturing",
            table: "non-operation-income-expense",
            type: "none_operational_income_tag[]",
            nullable: false,
            defaultValue: new List<NoneOperationalIncomeTag>(),
            oldClrType: typeof(List<NoneOperationalIncomeTag>),
            oldType: "none_operational_income_tag[]",
            oldDefaultValue: new List<NoneOperationalIncomeTag>());

        migrationBuilder.AlterColumn<decimal>(
            "this_period_sale_ratio_with_last_year",
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
            "this_period_sale_ratio",
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
            "target_price",
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
            "target_market_value",
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
            "receivable_ratio",
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
            "ps",
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
            "peg",
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
            "pe",
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
            "pb",
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
            "pa",
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
            "owners_equity_ratio",
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
            "optimal_buy_price",
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
            "operational_margin",
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
            "net_profit_growth_ratio",
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
            "net_margin",
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
            "market_value",
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
            "market_cap",
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
            "last_close_price",
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
            "gross_margin",
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
            "tags",
            schema: "manufacturing",
            table: "non-operation-income-expense",
            type: "none_operational_income_tag[]",
            nullable: false,
            defaultValue: new List<NoneOperationalIncomeTag>(),
            oldClrType: typeof(List<NoneOperationalIncomeTag>),
            oldType: "none_operational_income_tag[]",
            oldDefaultValue: new List<NoneOperationalIncomeTag>());

        migrationBuilder.AlterColumn<decimal>(
            "this_period_sale_ratio_with_last_year",
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
            "this_period_sale_ratio",
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
            "target_price",
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
            "target_market_value",
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
            "receivable_ratio",
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
            "ps",
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
            "peg",
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
            "pe",
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
            "pb",
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
            "pa",
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
            "owners_equity_ratio",
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
            "optimal_buy_price",
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
            "operational_margin",
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
            "net_profit_growth_ratio",
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
            "net_margin",
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
            "market_value",
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
            "market_cap",
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
            "last_close_price",
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
            "gross_margin",
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