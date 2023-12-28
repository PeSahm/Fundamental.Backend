#nullable disable

using Fundamental.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Fundamental.Migrations.Fundamental
{
    [DbContext(typeof(FundamentalDbContext))]
    [Migration("20231227135955_AddPublisherTable2")]
    public class AddPublisherTable2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Publisher",
                newName: "Publisher",
                newSchema: "fs");

            migrationBuilder.AlterColumn<string>(
                name: "Isin",
                schema: "shd",
                table: "Symbol",
                type: "varchar(15)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(12)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Publisher",
                schema: "fs",
                newName: "Publisher");

            migrationBuilder.AlterColumn<string>(
                name: "Isin",
                schema: "shd",
                table: "Symbol",
                type: "varchar(12)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(15)");
        }
    }
}