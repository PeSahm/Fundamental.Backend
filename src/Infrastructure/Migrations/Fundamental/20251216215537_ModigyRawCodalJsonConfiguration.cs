using System.Collections.Generic;
using Fundamental.Domain.Codals.Manufacturing.Enums;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fundamental.Migrations.Fundamental
{
    /// <inheritdoc />
    public partial class ModigyRawCodalJsonConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "isin",
                schema: "codals",
                table: "raw_codal_json",
                type: "character varying(13)",
                maxLength: 13,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(12)",
                oldMaxLength: 12,
                oldNullable: true);

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "isin",
                schema: "codals",
                table: "raw_codal_json",
                type: "character varying(12)",
                maxLength: 12,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(13)",
                oldMaxLength: 13,
                oldNullable: true);

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
        }
    }
}
