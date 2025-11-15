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
    [Migration("20251028154759_FixSymbolShareHoldersNaming")]
    public class FixSymbolShareHoldersNaming : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_symbol_share_holders_symbol_share_holder_symbol_id",
                schema: "shd",
                table: "symbol-share-holders");

            migrationBuilder.DropForeignKey(
                name: "fk_symbol_share_holders_symbol_symbol_id",
                schema: "shd",
                table: "symbol-share-holders");

            migrationBuilder.DropPrimaryKey(
                name: "pk_symbol_share_holders",
                schema: "shd",
                table: "symbol-share-holders");

            migrationBuilder.RenameTable(
                name: "symbol-share-holders",
                schema: "shd",
                newName: "symbol_share_holder",
                newSchema: "shd");

            migrationBuilder.RenameIndex(
                name: "ix_symbol_share_holders_symbol_id",
                schema: "shd",
                table: "symbol_share_holder",
                newName: "ix_symbol_share_holder_symbol_id");

            migrationBuilder.RenameIndex(
                name: "ix_symbol_share_holders_share_holder_symbol_id",
                schema: "shd",
                table: "symbol_share_holder",
                newName: "ix_symbol_share_holder_share_holder_symbol_id");

            migrationBuilder.RenameIndex(
                name: "ix_symbol_share_holders_id",
                schema: "shd",
                table: "symbol_share_holder",
                newName: "ix_symbol_share_holder_id");

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
                name: "pk_symbol_share_holder",
                schema: "shd",
                table: "symbol_share_holder",
                column: "_id");

            migrationBuilder.AddForeignKey(
                name: "fk_symbol_share_holder_symbol_share_holder_symbol_id",
                schema: "shd",
                table: "symbol_share_holder",
                column: "share_holder_symbol_id",
                principalSchema: "shd",
                principalTable: "symbol",
                principalColumn: "_id");

            migrationBuilder.AddForeignKey(
                name: "fk_symbol_share_holder_symbol_symbol_id",
                schema: "shd",
                table: "symbol_share_holder",
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
                name: "fk_symbol_share_holder_symbol_share_holder_symbol_id",
                schema: "shd",
                table: "symbol_share_holder");

            migrationBuilder.DropForeignKey(
                name: "fk_symbol_share_holder_symbol_symbol_id",
                schema: "shd",
                table: "symbol_share_holder");

            migrationBuilder.DropPrimaryKey(
                name: "pk_symbol_share_holder",
                schema: "shd",
                table: "symbol_share_holder");

            migrationBuilder.RenameTable(
                name: "symbol_share_holder",
                schema: "shd",
                newName: "symbol-share-holders",
                newSchema: "shd");

            migrationBuilder.RenameIndex(
                name: "ix_symbol_share_holder_symbol_id",
                schema: "shd",
                table: "symbol-share-holders",
                newName: "ix_symbol_share_holders_symbol_id");

            migrationBuilder.RenameIndex(
                name: "ix_symbol_share_holder_share_holder_symbol_id",
                schema: "shd",
                table: "symbol-share-holders",
                newName: "ix_symbol_share_holders_share_holder_symbol_id");

            migrationBuilder.RenameIndex(
                name: "ix_symbol_share_holder_id",
                schema: "shd",
                table: "symbol-share-holders",
                newName: "ix_symbol_share_holders_id");

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
                name: "pk_symbol_share_holders",
                schema: "shd",
                table: "symbol-share-holders",
                column: "_id");

            migrationBuilder.AddForeignKey(
                name: "fk_symbol_share_holders_symbol_share_holder_symbol_id",
                schema: "shd",
                table: "symbol-share-holders",
                column: "share_holder_symbol_id",
                principalSchema: "shd",
                principalTable: "symbol",
                principalColumn: "_id");

            migrationBuilder.AddForeignKey(
                name: "fk_symbol_share_holders_symbol_symbol_id",
                schema: "shd",
                table: "symbol-share-holders",
                column: "symbol_id",
                principalSchema: "shd",
                principalTable: "symbol",
                principalColumn: "_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}