#nullable disable

using Fundamental.Domain.Codals.Manufacturing.Enums;
using Fundamental.Domain.Common.Enums;
using Fundamental.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Fundamental.Migrations.Fundamental;

/// <inheritdoc />
[DbContext(typeof(FundamentalDbContext))]
[Migration("20250301193102_AddFinancialStatementsEntity")]
public class AddFinancialStatementsEntity : Migration
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

        migrationBuilder.AddColumn<long>(
            "financial_statement_id",
            schema: "manufacturing",
            table: "balance-sheet",
            type: "bigint",
            nullable: true);

        migrationBuilder.CreateTable(
            "financial-statement",
            schema: "manufacturing",
            columns: table => new
            {
                _id = table.Column<long>("bigint", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                Id = table.Column<Guid>("uuid", nullable: false),
                symbol_id = table.Column<long>("bigint", nullable: false),
                trace_no = table.Column<long>("BIGINT", nullable: false),
                currency = table.Column<IsoCurrency>("iso_currency", nullable: false),
                yearendmonth = table.Column<short>(name: "year-end-month", type: "smallint", nullable: false),
                last_close_price = table.Column<decimal>("numeric(18,2)", precision: 18, scale: 2, nullable: false),
                last_close_price_date = table.Column<DateOnly>("date", nullable: false),
                market_cap = table.Column<decimal>("numeric(18,2)", precision: 18, scale: 2, nullable: false),
                market_value = table.Column<decimal>("numeric(18,2)", precision: 18, scale: 2, nullable: false),
                reportmonth = table.Column<short>(name: "report-month", type: "smallint", nullable: false),
                salemonth = table.Column<short>(name: "sale-month", type: "smallint", nullable: false),
                this_period_sale_ratio = table.Column<decimal>("numeric(18,2)", precision: 18, scale: 2, nullable: false),
                this_period_sale_ratio_with_last_year =
                    table.Column<decimal>("numeric(18,2)", precision: 18, scale: 2, nullable: false),
                gross_margin = table.Column<decimal>("numeric(18,2)", precision: 18, scale: 2, nullable: false),
                operational_margin = table.Column<decimal>("numeric(18,2)", precision: 18, scale: 2, nullable: false),
                net_margin = table.Column<decimal>("numeric(18,4)", precision: 18, scale: 4, nullable: false),
                target_market_value = table.Column<decimal>("numeric(18,2)", precision: 18, scale: 2, nullable: false),
                target_price = table.Column<decimal>("numeric(18,2)", precision: 18, scale: 2, nullable: false),
                optimal_buy_price = table.Column<decimal>("numeric(18,2)", precision: 18, scale: 2, nullable: false),
                pe = table.Column<decimal>("numeric(18,2)", precision: 18, scale: 2, nullable: false),
                ps = table.Column<decimal>("numeric(18,2)", precision: 18, scale: 2, nullable: false),
                owners_equity_ratio = table.Column<decimal>("numeric(18,2)", precision: 18, scale: 2, nullable: false),
                pa = table.Column<decimal>("numeric(18,2)", precision: 18, scale: 2, nullable: false),
                pb = table.Column<decimal>("numeric(18,2)", precision: 18, scale: 2, nullable: false),
                receivable_ratio = table.Column<decimal>("numeric(18,2)", precision: 18, scale: 2, nullable: false),
                net_profit_growth_ratio = table.Column<decimal>("numeric(18,2)", precision: 18, scale: 2, nullable: false),
                peg = table.Column<decimal>("numeric(18,2)", precision: 18, scale: 2, nullable: false),
                version = table.Column<byte[]>("bytea", rowVersion: true, nullable: false),
                assets = table.Column<decimal>("numeric(36,10)", precision: 36, scale: 10, nullable: false),
                costs = table.Column<decimal>("numeric(36,10)", precision: 36, scale: 10, nullable: false),
                dps_last_year = table.Column<decimal>("numeric(36,10)", precision: 36, scale: 10, nullable: false),
                dps_ratio_last_year = table.Column<decimal>("numeric(36,10)", precision: 36, scale: 10, nullable: false),
                dps_ratio_two_years_ago = table.Column<decimal>("numeric(36,10)", precision: 36, scale: 10, nullable: false),
                dps_two_years_ago = table.Column<decimal>("numeric(36,10)", precision: 36, scale: 10, nullable: false),
                fall_operation_income = table.Column<decimal>("numeric(36,10)", precision: 36, scale: 10, nullable: false),
                fiscalyear = table.Column<short>(name: "fiscal-year", type: "SMALLINT", nullable: false),
                forecast_none_operational_profit =
                    table.Column<decimal>("numeric(36,10)", precision: 36, scale: 10, nullable: false),
                forecast_operational_profit = table.Column<decimal>("numeric(36,10)", precision: 36, scale: 10, nullable: false),
                forecast_sale = table.Column<decimal>("numeric(36,10)", precision: 36, scale: 10, nullable: false),
                forecast_total_profit = table.Column<decimal>("numeric(36,10)", precision: 36, scale: 10, nullable: false),
                gross_profit_or_loss = table.Column<decimal>("numeric(36,10)", precision: 36, scale: 10, nullable: false),
                last_year_net_profit_or_loss = table.Column<decimal>("numeric(36,10)", precision: 36, scale: 10, nullable: false),
                net_profit_or_loss = table.Column<decimal>("numeric(36,10)", precision: 36, scale: 10, nullable: false),
                none_operational_profit = table.Column<decimal>("numeric(36,10)", precision: 36, scale: 10, nullable: false),
                operational_income = table.Column<decimal>("numeric(36,10)", precision: 36, scale: 10, nullable: false),
                operational_profit_or_loss = table.Column<decimal>("numeric(36,10)", precision: 36, scale: 10, nullable: false),
                owners_equity = table.Column<decimal>("numeric(36,10)", precision: 36, scale: 10, nullable: false),
                receivables = table.Column<decimal>("numeric(36,10)", precision: 36, scale: 10, nullable: false),
                sale = table.Column<decimal>("numeric(36,10)", precision: 36, scale: 10, nullable: false),
                sale_average_exclude_this_period =
                    table.Column<decimal>("numeric(36,10)", precision: 36, scale: 10, nullable: false),
                sale_average_last_year_same_period =
                    table.Column<decimal>("numeric(36,10)", precision: 36, scale: 10, nullable: false),
                sale_before_this_month = table.Column<decimal>("numeric(36,10)", precision: 36, scale: 10, nullable: false),
                sale_last_year_same_period = table.Column<decimal>("numeric(36,10)", precision: 36, scale: 10, nullable: false),
                spring_operation_income = table.Column<decimal>("numeric(36,10)", precision: 36, scale: 10, nullable: false),
                summer_operation_income = table.Column<decimal>("numeric(36,10)", precision: 36, scale: 10, nullable: false),
                total_sale = table.Column<decimal>("numeric(36,10)", precision: 36, scale: 10, nullable: false),
                winter_operation_income = table.Column<decimal>("numeric(36,10)", precision: 36, scale: 10, nullable: false),
                CreatedAt = table.Column<DateTime>("Timestamp", nullable: false),
                ModifiedAt = table.Column<DateTime>("Timestamp", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_financial_statement", x => x._id);
                table.ForeignKey(
                    "fk_financial_statement_symbols_symbol_id",
                    x => x.symbol_id,
                    principalSchema: "shd",
                    principalTable: "symbol",
                    principalColumn: "_id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            "ix_balance_sheet_financial_statement_id",
            schema: "manufacturing",
            table: "balance-sheet",
            column: "financial_statement_id",
            unique: true);

        migrationBuilder.CreateIndex(
            "ix_financial_statement_id",
            schema: "manufacturing",
            table: "financial-statement",
            column: "Id",
            unique: true);

        migrationBuilder.CreateIndex(
            "ix_financial_statement_symbol_id",
            schema: "manufacturing",
            table: "financial-statement",
            column: "symbol_id");

        migrationBuilder.AddForeignKey(
            "fk_balance_sheet_manufacturing_financial_statement_financial_s",
            schema: "manufacturing",
            table: "balance-sheet",
            column: "financial_statement_id",
            principalSchema: "manufacturing",
            principalTable: "financial-statement",
            principalColumn: "_id");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            "fk_balance_sheet_manufacturing_financial_statement_financial_s",
            schema: "manufacturing",
            table: "balance-sheet");

        migrationBuilder.DropTable(
            "financial-statement",
            "manufacturing");

        migrationBuilder.DropIndex(
            "ix_balance_sheet_financial_statement_id",
            schema: "manufacturing",
            table: "balance-sheet");

        migrationBuilder.DropColumn(
            "financial_statement_id",
            schema: "manufacturing",
            table: "balance-sheet");

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
    }
}