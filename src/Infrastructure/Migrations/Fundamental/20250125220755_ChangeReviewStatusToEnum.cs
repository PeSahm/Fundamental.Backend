using Fundamental.Domain.Common.Enums;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fundamental.Migrations.Fundamental
{
    /// <inheritdoc />
    public partial class ChangeReviewStatusToEnum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<ReviewStatus>(
                name: "review_status",
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
                name: "review_status",
                schema: "shd",
                table: "symbol-share-holders",
                type: "SMALLINT",
                nullable: false,
                oldClrType: typeof(ReviewStatus),
                oldType: "review_status");
        }
    }
}
