using Fundamental.Domain.Codals.Manufacturing.Enums;
using Fundamental.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fundamental.Migrations.Fundamental
{
    /// <inheritdoc />
    [DbContext(typeof(FundamentalDbContext))]
    [Migration("20251028174448_AddBalanceSheetQueryOptimizationIndex")]
    public partial class AddBalanceSheetQueryOptimizationIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<List<NoneOperationalIncomeTag>>(
                name: "tags",
                schema: "manufacturing",
                table: "non_operation_income_and_expense",
                type: "none_operational_income_tag[]",
                nullable: false,
                defaultValue: new List<NoneOperationalIncomeTag>(),
                oldClrType: typeof(List<NoneOperationalIncomeTag>),
                oldType: "none_operational_income_tag[]",
                oldDefaultValue: new List<NoneOperationalIncomeTag>());

            // Add composite index for BalanceSheet query optimization
            // Covers GROUP BY (symbol_id, fiscal_year, report_month) and ORDER BY (publish_date)
            migrationBuilder.Sql(@"
                CREATE INDEX IF NOT EXISTS ""ix_balance_sheet_query_optimization""
                ON manufacturing.balance_sheet (symbol_id, fiscal_year, report_month, publish_date);
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<List<NoneOperationalIncomeTag>>(
                name: "tags",
                schema: "manufacturing",
                table: "non_operation_income_and_expense",
                type: "none_operational_income_tag[]",
                nullable: false,
                defaultValue: new List<NoneOperationalIncomeTag>(),
                oldClrType: typeof(List<NoneOperationalIncomeTag>),
                oldType: "none_operational_income_tag[]",
                oldDefaultValue: new List<NoneOperationalIncomeTag>());

            // Drop the composite index for BalanceSheet query optimization
            migrationBuilder.Sql(@"
                DROP INDEX IF EXISTS manufacturing.""ix_balance_sheet_query_optimization"";
            ");
        }
    }
}