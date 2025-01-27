using Fundamental.Domain.Common.Enums;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fundamental.Migrations.Fundamental
{
    /// <inheritdoc />
    public partial class AddNewPropertiesToStockOwnershipEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<ReviewStatus>(
                name: "review_status",
                schema: "manufacturing",
                table: "stock_ownership",
                type: "review_status",
                nullable: false,
                defaultValue: ReviewStatus.Pending);

            migrationBuilder.AddColumn<decimal>(
                name: "trace_no",
                schema: "manufacturing",
                table: "stock_ownership",
                type: "numeric(20,0)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "review_status",
                schema: "manufacturing",
                table: "stock_ownership");

            migrationBuilder.DropColumn(
                name: "trace_no",
                schema: "manufacturing",
                table: "stock_ownership");
        }
    }
}
