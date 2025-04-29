using System.Collections.Generic;
using Fundamental.Domain.Codals.Manufacturing.Enums;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fundamental.Migrations.Fundamental
{
    /// <inheritdoc />
    public partial class AddIndexesOnFsTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_publisher_symbol_id",
                schema: "fs",
                table: "publisher");

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
                name: "ix_symbol_sector_codes",
                schema: "shd",
                table: "symbol",
                columns: new[] { "sector_code", "sub_sector_code" });

            migrationBuilder.CreateIndex(
                name: "ix_publisher_symbol_id",
                schema: "fs",
                table: "publisher",
                column: "symbol_id",
                unique: false);

            migrationBuilder.CreateIndex(
                name: "ix_financial_statement_performance_metrics",
                schema: "manufacturing",
                table: "financial-statement",
                columns: new[] { "last_close_price", "pe", "market_value" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_symbol_sector_codes",
                schema: "shd",
                table: "symbol");

            migrationBuilder.DropIndex(
                name: "ix_publisher_symbol_id",
                schema: "fs",
                table: "publisher");

            migrationBuilder.DropIndex(
                name: "ix_financial_statement_performance_metrics",
                schema: "manufacturing",
                table: "financial-statement");

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
                name: "ix_publisher_symbol_id",
                schema: "fs",
                table: "publisher",
                column: "symbol_id");
        }
    }
}
