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
        // Use raw SQL with USING clause for PostgreSQL enum conversion
        migrationBuilder.Sql(@"
            ALTER TABLE shd.""symbol-share-holders""
            ALTER COLUMN review_status TYPE review_status
            USING (CASE review_status
                WHEN 0 THEN 'pending'::review_status
                WHEN 1 THEN 'rejected'::review_status
                WHEN 2 THEN 'approved'::review_status
                ELSE 'pending'::review_status
            END);
        ");
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