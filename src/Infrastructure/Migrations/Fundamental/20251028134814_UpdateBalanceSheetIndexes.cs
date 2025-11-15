using Fundamental.Domain.Codals.Manufacturing.Enums;
using Fundamental.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fundamental.Migrations.Fundamental
{
    /// <inheritdoc />
    [DbContext(typeof(FundamentalDbContext))]
    [Migration("20251028134814_UpdateBalanceSheetIndexes")]
    public class UpdateBalanceSheetIndexes : Migration
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

            migrationBuilder.CreateIndex(
                name: "ix_balance_sheet_detail_balance_sheet_id_row",
                schema: "manufacturing",
                table: "balance-sheet-detail",
                columns: new[] { "balance_sheet_id", "row" });

            migrationBuilder.CreateIndex(
                name: "ix_balance_sheet_detail_category_row",
                schema: "manufacturing",
                table: "balance-sheet-detail",
                columns: new[] { "codal_category", "codal_row" });

            migrationBuilder.CreateIndex(
                name: "ix_balance_sheet_detail_codal_category",
                schema: "manufacturing",
                table: "balance-sheet-detail",
                column: "codal_category");

            migrationBuilder.CreateIndex(
                name: "ix_balance_sheet_detail_codal_row",
                schema: "manufacturing",
                table: "balance-sheet-detail",
                column: "codal_row");

            migrationBuilder.CreateIndex(
                name: "ix_balance_sheet_publish_date",
                schema: "manufacturing",
                table: "balance-sheet",
                column: "publish-date");

            migrationBuilder.CreateIndex(
                name: "ix_balance_sheet_trace_no",
                schema: "manufacturing",
                table: "balance-sheet",
                column: "trace_no");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_balance_sheet_detail_balance_sheet_id_row",
                schema: "manufacturing",
                table: "balance-sheet-detail");

            migrationBuilder.DropIndex(
                name: "ix_balance_sheet_detail_category_row",
                schema: "manufacturing",
                table: "balance-sheet-detail");

            migrationBuilder.DropIndex(
                name: "ix_balance_sheet_detail_codal_category",
                schema: "manufacturing",
                table: "balance-sheet-detail");

            migrationBuilder.DropIndex(
                name: "ix_balance_sheet_detail_codal_row",
                schema: "manufacturing",
                table: "balance-sheet-detail");

            migrationBuilder.DropIndex(
                name: "ix_balance_sheet_publish_date",
                schema: "manufacturing",
                table: "balance-sheet");

            migrationBuilder.DropIndex(
                name: "ix_balance_sheet_trace_no",
                schema: "manufacturing",
                table: "balance-sheet");

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
        }
    }
}