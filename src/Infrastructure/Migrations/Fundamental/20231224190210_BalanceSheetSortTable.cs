#nullable disable

using Fundamental.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Fundamental.Migrations.Fundamental
{
    [DbContext(typeof(FundamentalDbContext))]
    [Migration("20231224190210_BalanceSheetSortTable")]
    public class BalanceSheetSortTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "sort");

            migrationBuilder.CreateTable(
                name: "BalanceSheet",
                schema: "sort",
                columns: table => new
                {
                    _id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Order = table.Column<short>(type: "SMALLINT", nullable: false),
                    CodalRow = table.Column<short>(type: "SMALLINT", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    Category = table.Column<short>(type: "SMALLINT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BalanceSheet", x => x._id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BalanceSheet_Category_CodalRow",
                schema: "sort",
                table: "BalanceSheet",
                columns: new[] { "Category", "CodalRow" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BalanceSheet_Id",
                schema: "sort",
                table: "BalanceSheet",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BalanceSheet_Order",
                schema: "sort",
                table: "BalanceSheet",
                column: "Order",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BalanceSheet",
                schema: "sort");
        }
    }
}