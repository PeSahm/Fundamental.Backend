#nullable disable

using Fundamental.Domain.Codals.Manufacturing.Enums;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Fundamental.Migrations.Fundamental;

/// <inheritdoc />
public partial class AddOtherOperationalIncomeToFs : Migration
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

        migrationBuilder.AddColumn<decimal>(
            "other_operational_income",
            schema: "manufacturing",
            table: "financial-statement",
            type: "numeric(36,10)",
            precision: 36,
            scale: 10,
            nullable: false,
            defaultValue: 0m);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            "other_operational_income",
            schema: "manufacturing",
            table: "financial-statement");

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