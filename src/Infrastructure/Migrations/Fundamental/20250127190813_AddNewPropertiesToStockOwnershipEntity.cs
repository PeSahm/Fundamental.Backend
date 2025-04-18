#nullable disable

using Fundamental.Domain.Common.Enums;
using Fundamental.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Fundamental.Migrations.Fundamental;

/// <inheritdoc />
[DbContext(typeof(FundamentalDbContext))]
[Migration("20250127190813_AddNewPropertiesToStockOwnershipEntity")]
public class AddNewPropertiesToStockOwnershipEntity : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<ReviewStatus>(
            "review_status",
            schema: "manufacturing",
            table: "stock_ownership",
            type: "review_status",
            nullable: false,
            defaultValue: ReviewStatus.Pending);

        migrationBuilder.AddColumn<decimal>(
            "trace_no",
            schema: "manufacturing",
            table: "stock_ownership",
            type: "numeric(20,0)",
            nullable: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            "review_status",
            schema: "manufacturing",
            table: "stock_ownership");

        migrationBuilder.DropColumn(
            "trace_no",
            schema: "manufacturing",
            table: "stock_ownership");
    }
}