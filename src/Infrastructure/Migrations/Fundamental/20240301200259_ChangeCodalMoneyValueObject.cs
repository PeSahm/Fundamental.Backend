#nullable disable

using Fundamental.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Fundamental.Migrations.Fundamental;

[DbContext(typeof(FundamentalDbContext))]
[Migration("20240301200259_ChangeCodalMoneyValueObject")]
public class ChangeCodalMoneyValueObject : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.RenameColumn(
            "value_value",
            schema: "manufacturing",
            table: "income-statement",
            newName: "value");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.RenameColumn(
            "value",
            schema: "manufacturing",
            table: "income-statement",
            newName: "value_value");
    }
}