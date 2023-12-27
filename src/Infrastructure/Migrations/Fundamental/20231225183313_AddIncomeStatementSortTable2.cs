#nullable disable

using Fundamental.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Fundamental.Migrations.Fundamental
{
    [DbContext(typeof(FundamentalDbContext))]
    [Migration("20231225183313_AddIncomeStatementSortTable2")]
    public class AddIncomeStatementSortTable2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_IncomeStatementSort_CodalRow",
                schema: "manufacturing",
                table: "IncomeStatementSort");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_IncomeStatementSort_CodalRow",
                schema: "manufacturing",
                table: "IncomeStatementSort",
                column: "CodalRow",
                unique: true);
        }
    }
}