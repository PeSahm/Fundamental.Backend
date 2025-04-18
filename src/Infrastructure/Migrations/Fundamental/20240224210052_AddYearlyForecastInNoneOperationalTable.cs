#nullable disable

using Fundamental.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Fundamental.Migrations.Fundamental;

[DbContext(typeof(FundamentalDbContext))]
[Migration("20240224210052_AddYearlyForecastInNoneOperationalTable")]
public class AddYearlyForecastInNoneOperationalTable : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<string>(
            "description",
            schema: "manufacturing",
            table: "non-operation-income-expense",
            type: "character varying(512)",
            maxLength: 512,
            nullable: true,
            oldClrType: typeof(string),
            oldType: "character varying(512)",
            oldMaxLength: 512);

        migrationBuilder.AddColumn<bool>(
            "yearly_forecast_period",
            schema: "manufacturing",
            table: "non-operation-income-expense",
            type: "boolean",
            nullable: false,
            defaultValueSql: "false");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            "yearly_forecast_period",
            schema: "manufacturing",
            table: "non-operation-income-expense");

        migrationBuilder.AlterColumn<string>(
            "description",
            schema: "manufacturing",
            table: "non-operation-income-expense",
            type: "character varying(512)",
            maxLength: 512,
            nullable: false,
            defaultValue: "",
            oldClrType: typeof(string),
            oldType: "character varying(512)",
            oldMaxLength: 512,
            oldNullable: true);
    }
}