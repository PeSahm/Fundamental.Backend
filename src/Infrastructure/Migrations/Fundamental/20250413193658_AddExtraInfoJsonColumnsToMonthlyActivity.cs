#nullable disable

using Fundamental.Domain.Codals.Manufacturing.Enums;
using Fundamental.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Fundamental.Migrations.Fundamental;

/// <inheritdoc />
[DbContext(typeof(FundamentalDbContext))]
[Migration("20250413193658_AddExtraInfoJsonColumnsToMonthlyActivity")]
public class AddExtraInfoJsonColumnsToMonthlyActivity : Migration
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

        migrationBuilder.AddColumn<string>(
            "extra_sales_infos",
            schema: "manufacturing",
            table: "monthly-activity",
            type: "jsonb",
            nullable: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            "extra_sales_infos",
            schema: "manufacturing",
            table: "monthly-activity");

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