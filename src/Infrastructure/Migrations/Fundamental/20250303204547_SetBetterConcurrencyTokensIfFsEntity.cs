#nullable disable

using Fundamental.Domain.Codals.Manufacturing.Enums;
using Fundamental.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Fundamental.Migrations.Fundamental;

/// <inheritdoc />
[DbContext(typeof(FundamentalDbContext))]
[Migration("20250303204547_SetBetterConcurrencyTokensIfFsEntity")]
public class SetBetterConcurrencyTokensIfFsEntity : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            "last_close_price_date",
            schema: "manufacturing",
            table: "financial-statement");

        migrationBuilder.DropColumn(
            "version",
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

        migrationBuilder.AddColumn<uint>(
            "xmin",
            schema: "manufacturing",
            table: "financial-statement",
            type: "xid",
            rowVersion: true,
            nullable: false,
            defaultValue: 0u);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            "xmin",
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

        migrationBuilder.AddColumn<DateOnly>(
            "last_close_price_date",
            schema: "manufacturing",
            table: "financial-statement",
            type: "date",
            nullable: false,
            defaultValue: new DateOnly(1, 1, 1));

        migrationBuilder.AddColumn<byte[]>(
            "version",
            schema: "manufacturing",
            table: "financial-statement",
            type: "bytea",
            rowVersion: true,
            nullable: false,
            defaultValue: new byte[0]);
    }
}