#nullable disable

using Fundamental.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Fundamental.Migrations.Fundamental;

/// <inheritdoc />
[DbContext(typeof(FundamentalDbContext))]
[Migration("20250126045347_ImprovePrecisionOfShareHolderProperty")]
public class ImprovePrecisionOfShareHolderProperty : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<decimal>(
            "ownership_percentage",
            schema: "manufacturing",
            table: "stock_ownership",
            type: "numeric(5,2)",
            precision: 5,
            scale: 2,
            nullable: false,
            oldClrType: typeof(decimal),
            oldType: "numeric(4,2)");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<decimal>(
            "ownership_percentage",
            schema: "manufacturing",
            table: "stock_ownership",
            type: "numeric(4,2)",
            nullable: false,
            oldClrType: typeof(decimal),
            oldType: "numeric(5,2)",
            oldPrecision: 5,
            oldScale: 2);
    }
}