using System.Collections.Generic;
using Fundamental.Domain.Codals.Manufacturing.Enums;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fundamental.Migrations.Fundamental
{
    /// <inheritdoc />
    public partial class FixFinancialStatementNaming : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "financial-statement",
                schema: "manufacturing",
                newName: "financial_statement",
                newSchema: "manufacturing");

            migrationBuilder.RenameColumn(
                name: "year-end-month",
                schema: "manufacturing",
                table: "financial_statement",
                newName: "year_end_month");

            migrationBuilder.RenameColumn(
                name: "sale-month",
                schema: "manufacturing",
                table: "financial_statement",
                newName: "sale_month");

            migrationBuilder.RenameColumn(
                name: "report-month",
                schema: "manufacturing",
                table: "financial_statement",
                newName: "report_month");

            migrationBuilder.RenameColumn(
                name: "fiscal-year",
                schema: "manufacturing",
                table: "financial_statement",
                newName: "fiscal_year");

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
            migrationBuilder.RenameTable(
                name: "financial_statement",
                schema: "manufacturing",
                newName: "financial-statement",
                newSchema: "manufacturing");

            migrationBuilder.RenameColumn(
                name: "year_end_month",
                schema: "manufacturing",
                table: "financial-statement",
                newName: "year-end-month");

            migrationBuilder.RenameColumn(
                name: "sale_month",
                schema: "manufacturing",
                table: "financial-statement",
                newName: "sale-month");

            migrationBuilder.RenameColumn(
                name: "report_month",
                schema: "manufacturing",
                table: "financial-statement",
                newName: "report-month");

            migrationBuilder.RenameColumn(
                name: "fiscal_year",
                schema: "manufacturing",
                table: "financial-statement",
                newName: "fiscal-year");

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
