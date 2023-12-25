#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace Fundamental.Migrations.Fundamental
{
    /// <inheritdoc />
    public partial class ChangeTableSchemas3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "BalanceSheetSort",
                schema: "fundamental",
                newName: "BalanceSheetSort",
                newSchema: "manufacturing");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "fundamental");

            migrationBuilder.RenameTable(
                name: "BalanceSheetSort",
                schema: "manufacturing",
                newName: "BalanceSheetSort",
                newSchema: "fundamental");
        }
    }
}