using Fundamental.Domain.Codals.Manufacturing.Enums;
using Fundamental.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fundamental.Migrations.Fundamental
{
    [DbContext(typeof(FundamentalDbContext))]
    [Migration("20251028153228_FixNonOperationIncomeAndExpenseNaming")]
    public class FixNonOperationIncomeAndExpenseNaming : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_non_operation_income_expense_symbols_symbol_id",
                schema: "manufacturing",
                table: "non-operation-income-expense");

            migrationBuilder.DropPrimaryKey(
                name: "pk_non_operation_income_expense",
                schema: "manufacturing",
                table: "non-operation-income-expense");

            migrationBuilder.RenameTable(
                name: "non-operation-income-expense",
                schema: "manufacturing",
                newName: "non_operation_income_and_expense",
                newSchema: "manufacturing");

            migrationBuilder.RenameIndex(
                name: "ix_non_operation_income_expense_symbol_id",
                schema: "manufacturing",
                table: "non_operation_income_and_expense",
                newName: "ix_non_operation_income_and_expense_symbol_id");

            migrationBuilder.RenameIndex(
                name: "ix_non_operation_income_expense_id",
                schema: "manufacturing",
                table: "non_operation_income_and_expense",
                newName: "ix_non_operation_income_and_expense_id");

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

            migrationBuilder.AddPrimaryKey(
                name: "pk_non_operation_income_and_expense",
                schema: "manufacturing",
                table: "non_operation_income_and_expense",
                column: "_id");

            migrationBuilder.AddForeignKey(
                name: "fk_non_operation_income_and_expense_symbols_symbol_id",
                schema: "manufacturing",
                table: "non_operation_income_and_expense",
                column: "symbol_id",
                principalSchema: "shd",
                principalTable: "symbol",
                principalColumn: "_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_non_operation_income_and_expense_symbols_symbol_id",
                schema: "manufacturing",
                table: "non_operation_income_and_expense");

            migrationBuilder.DropPrimaryKey(
                name: "pk_non_operation_income_and_expense",
                schema: "manufacturing",
                table: "non_operation_income_and_expense");

            migrationBuilder.RenameTable(
                name: "non_operation_income_and_expense",
                schema: "manufacturing",
                newName: "non-operation-income-expense",
                newSchema: "manufacturing");

            migrationBuilder.RenameIndex(
                name: "ix_non_operation_income_and_expense_symbol_id",
                schema: "manufacturing",
                table: "non-operation-income-expense",
                newName: "ix_non_operation_income_expense_symbol_id");

            migrationBuilder.RenameIndex(
                name: "ix_non_operation_income_and_expense_id",
                schema: "manufacturing",
                table: "non-operation-income-expense",
                newName: "ix_non_operation_income_expense_id");

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

            migrationBuilder.AddPrimaryKey(
                name: "pk_non_operation_income_expense",
                schema: "manufacturing",
                table: "non-operation-income-expense",
                column: "_id");

            migrationBuilder.AddForeignKey(
                name: "fk_non_operation_income_expense_symbols_symbol_id",
                schema: "manufacturing",
                table: "non-operation-income-expense",
                column: "symbol_id",
                principalSchema: "shd",
                principalTable: "symbol",
                principalColumn: "_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}