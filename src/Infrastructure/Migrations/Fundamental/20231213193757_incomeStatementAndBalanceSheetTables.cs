#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;

namespace Fundamental.Migrations.Fundamental
{
    /// <inheritdoc />
    public partial class incomeStatementAndBalanceSheetTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BalanceSheet",
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
                    YearEndMonth = table.Column<byte>(type: "TINYINT", nullable: false),
                    ReportMonth = table.Column<byte>(type: "TINYINT", nullable: false),
                    Row = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    IsAudited = table.Column<bool>(type: "bit", nullable: false),
                    FiscalYear = table.Column<short>(type: "SMALLINT", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(36,10)", precision: 36, scale: 10, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BalanceSheet", x => x._id);
                    table.ForeignKey(
                        name: "FK_BalanceSheet_Symbol_SymbolId",
                        column: x => x.SymbolId,
                        principalSchema: "shd",
                        principalTable: "Symbol",
                        principalColumn: "_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IncomeStatement",
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
                    YearEndMonth = table.Column<byte>(type: "TINYINT", nullable: false),
                    ReportMonth = table.Column<byte>(type: "TINYINT", nullable: false),
                    Row = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    IsAudited = table.Column<bool>(type: "bit", nullable: false),
                    FiscalYear = table.Column<short>(type: "SMALLINT", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(36,10)", precision: 36, scale: 10, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IncomeStatement", x => x._id);
                    table.ForeignKey(
                        name: "FK_IncomeStatement_Symbol_SymbolId",
                        column: x => x.SymbolId,
                        principalSchema: "shd",
                        principalTable: "Symbol",
                        principalColumn: "_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BalanceSheet_Id",
                schema: "fs",
                table: "BalanceSheet",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BalanceSheet_SymbolId",
                schema: "fs",
                table: "BalanceSheet",
                column: "SymbolId");

            migrationBuilder.CreateIndex(
                name: "IX_IncomeStatement_Id",
                schema: "fs",
                table: "IncomeStatement",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_IncomeStatement_SymbolId",
                schema: "fs",
                table: "IncomeStatement",
                column: "SymbolId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BalanceSheet",
                schema: "fs");

            migrationBuilder.DropTable(
                name: "IncomeStatement",
                schema: "fs");
        }
    }
}