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
    [Migration("20251028143643_FixIncomeStatementNaming")]
    public class FixIncomeStatementNaming : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "income-statement",
                schema: "manufacturing",
                newName: "income_statement",
                newSchema: "manufacturing");

            migrationBuilder.RenameColumn(
                name: "fiscal-year",
                schema: "manufacturing",
                table: "income_statement",
                newName: "fiscal_year");

            migrationBuilder.RenameColumn(
                name: "year-end-month",
                schema: "manufacturing",
                table: "income_statement",
                newName: "year_end_month");

            migrationBuilder.RenameColumn(
                name: "report-month",
                schema: "manufacturing",
                table: "income_statement",
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
            migrationBuilder.RenameTable(
                name: "income_statement",
                schema: "manufacturing",
                newName: "income-statement",
                newSchema: "manufacturing");

            migrationBuilder.RenameColumn(
                name: "fiscal_year",
                schema: "manufacturing",
                table: "income-statement",
                newName: "fiscal-year");

            migrationBuilder.RenameColumn(
                name: "year_end_month",
                schema: "manufacturing",
                table: "income-statement",
                newName: "year-end-month");

            migrationBuilder.RenameColumn(
                name: "report_month",
                schema: "manufacturing",
                table: "income-statement",
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