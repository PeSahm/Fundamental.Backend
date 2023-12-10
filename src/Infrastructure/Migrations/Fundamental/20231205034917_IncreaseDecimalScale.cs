#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace Fundamental.Migrations.Fundamental
{
    /// <inheritdoc />
    public partial class IncreaseDecimalScale : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "SaleLastYear",
                schema: "fs",
                table: "MonthlyActivities",
                type: "decimal(36,10)",
                precision: 36,
                scale: 10,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,3)",
                oldPrecision: 18,
                oldScale: 3);

            migrationBuilder.AlterColumn<decimal>(
                name: "SaleIncludeCurrentMonth",
                schema: "fs",
                table: "MonthlyActivities",
                type: "decimal(36,10)",
                precision: 36,
                scale: 10,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,3)",
                oldPrecision: 18,
                oldScale: 3);

            migrationBuilder.AlterColumn<decimal>(
                name: "SaleCurrentMonth",
                schema: "fs",
                table: "MonthlyActivities",
                type: "decimal(36,10)",
                precision: 36,
                scale: 10,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,3)",
                oldPrecision: 18,
                oldScale: 3);

            migrationBuilder.AlterColumn<decimal>(
                name: "SaleBeforeCurrentMonth",
                schema: "fs",
                table: "MonthlyActivities",
                type: "decimal(36,10)",
                precision: 36,
                scale: 10,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,3)",
                oldPrecision: 18,
                oldScale: 3);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "SaleLastYear",
                schema: "fs",
                table: "MonthlyActivities",
                type: "decimal(18,3)",
                precision: 18,
                scale: 3,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(36,10)",
                oldPrecision: 36,
                oldScale: 10);

            migrationBuilder.AlterColumn<decimal>(
                name: "SaleIncludeCurrentMonth",
                schema: "fs",
                table: "MonthlyActivities",
                type: "decimal(18,3)",
                precision: 18,
                scale: 3,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(36,10)",
                oldPrecision: 36,
                oldScale: 10);

            migrationBuilder.AlterColumn<decimal>(
                name: "SaleCurrentMonth",
                schema: "fs",
                table: "MonthlyActivities",
                type: "decimal(18,3)",
                precision: 18,
                scale: 3,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(36,10)",
                oldPrecision: 36,
                oldScale: 10);

            migrationBuilder.AlterColumn<decimal>(
                name: "SaleBeforeCurrentMonth",
                schema: "fs",
                table: "MonthlyActivities",
                type: "decimal(18,3)",
                precision: 18,
                scale: 3,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(36,10)",
                oldPrecision: 36,
                oldScale: 10);
        }
    }
}