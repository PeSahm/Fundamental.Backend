#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace Fundamental.Migrations.Fundamental
{
    /// <inheritdoc />
    public partial class AlterMonthlyActivityTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<short>(
                name: "ProductType",
                schema: "shd",
                table: "Symbol",
                type: "smallint",
                nullable: false,
                oldClrType: typeof(short),
                oldType: "smallint",
                oldDefaultValueSql: "1");

            migrationBuilder.CreateTable(
                name: "MonthlyActivities",
                schema: "fs",
                columns: table => new
                {
                    _id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SymbolId = table.Column<long>(type: "bigint", nullable: false),
                    TraceNo = table.Column<long>(type: "BIGINT", nullable: false),
                    Uri = table.Column<string>(type: "VARCHAR(512)", nullable: false),
                    Currency = table.Column<string>(type: "char(3)", fixedLength: true, maxLength: 3, nullable: false),
                    HasSubCompanySale = table.Column<bool>(type: "bit", nullable: false),
                    FiscalYear = table.Column<short>(type: "SMALLINT", nullable: false),
                    ReportMonth = table.Column<byte>(type: "TINYINT", nullable: false),
                    SaleBeforeCurrentMonth = table.Column<decimal>(type: "decimal(18,3)", precision: 18, scale: 3, nullable: false),
                    SaleCurrentMonth = table.Column<decimal>(type: "decimal(18,3)", precision: 18, scale: 3, nullable: false),
                    SaleIncludeCurrentMonth = table.Column<decimal>(type: "decimal(18,3)", precision: 18, scale: 3, nullable: false),
                    SaleLastYear = table.Column<decimal>(type: "decimal(18,3)", precision: 18, scale: 3, nullable: false),
                    YearEndMonth = table.Column<byte>(type: "TINYINT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonthlyActivities", x => x._id);
                    table.ForeignKey(
                        name: "FK_MonthlyActivities_Symbol_SymbolId",
                        column: x => x.SymbolId,
                        principalSchema: "shd",
                        principalTable: "Symbol",
                        principalColumn: "_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MonthlyActivities_Id",
                schema: "fs",
                table: "MonthlyActivities",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MonthlyActivities_SymbolId",
                schema: "fs",
                table: "MonthlyActivities",
                column: "SymbolId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MonthlyActivities",
                schema: "fs");

            migrationBuilder.AlterColumn<short>(
                name: "ProductType",
                schema: "shd",
                table: "Symbol",
                type: "smallint",
                nullable: false,
                defaultValueSql: "1",
                oldClrType: typeof(short),
                oldType: "smallint");
        }
    }
}