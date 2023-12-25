#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace Fundamental.Migrations.Fundamental
{
    /// <inheritdoc />
    public partial class AddIncomeStatementSortTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IncomeStatementSort",
                schema: "manufacturing",
                columns: table => new
                {
                    _id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Order = table.Column<short>(type: "SMALLINT", nullable: false),
                    CodalRow = table.Column<short>(type: "SMALLINT", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IncomeStatementSort", x => x._id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IncomeStatementSort_CodalRow",
                schema: "manufacturing",
                table: "IncomeStatementSort",
                column: "CodalRow",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_IncomeStatementSort_Id",
                schema: "manufacturing",
                table: "IncomeStatementSort",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_IncomeStatementSort_Order",
                schema: "manufacturing",
                table: "IncomeStatementSort",
                column: "Order",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IncomeStatementSort",
                schema: "manufacturing");
        }
    }
}