#nullable disable

using Fundamental.Domain.Common.Enums;
using Fundamental.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Fundamental.Migrations.Fundamental;

/// <inheritdoc />
[DbContext(typeof(FundamentalDbContext))]
[Migration("20250125220755_ChangeReviewStatusToEnum")]
public class ChangeReviewStatusToEnum : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<ReviewStatus>(
            "review_status",
            schema: "shd",
            table: "symbol-share-holders",
            type: "review_status",
            nullable: false,
            oldClrType: typeof(short),
            oldType: "SMALLINT");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<short>(
            "review_status",
            schema: "shd",
            table: "symbol-share-holders",
            type: "SMALLINT",
            nullable: false,
            oldClrType: typeof(ReviewStatus),
            oldType: "review_status");
    }
}