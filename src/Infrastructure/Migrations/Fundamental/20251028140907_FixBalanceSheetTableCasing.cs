using System.Collections.Generic;
using Fundamental.Domain.Codals.Manufacturing.Enums;
using Fundamental.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fundamental.Migrations.Fundamental
{
    /// <inheritdoc />
    [DbContext(typeof(FundamentalDbContext))]
    [Migration("20251028140907_FixBalanceSheetTableCasing")]
    public class FixBalanceSheetTableCasing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "publish-date",
                schema: "manufacturing",
                table: "balance-sheet",
                newName: "publish_date");

            migrationBuilder.RenameColumn(
                name: "fiscal-year",
                schema: "manufacturing",
                table: "balance-sheet",
                newName: "fiscal_year");

            migrationBuilder.RenameColumn(
                name: "year-end-month",
                schema: "manufacturing",
                table: "balance-sheet",
                newName: "year_end_month");

            migrationBuilder.RenameColumn(
                name: "report-month",
                schema: "manufacturing",
                table: "balance-sheet",
                newName: "report_month");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "publish_date",
                schema: "manufacturing",
                table: "balance-sheet",
                newName: "publish-date");

            migrationBuilder.RenameColumn(
                name: "fiscal_year",
                schema: "manufacturing",
                table: "balance-sheet",
                newName: "fiscal-year");

            migrationBuilder.RenameColumn(
                name: "year_end_month",
                schema: "manufacturing",
                table: "balance-sheet",
                newName: "year-end-month");

            migrationBuilder.RenameColumn(
                name: "report_month",
                schema: "manufacturing",
                table: "balance-sheet",
                newName: "report-month");

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
