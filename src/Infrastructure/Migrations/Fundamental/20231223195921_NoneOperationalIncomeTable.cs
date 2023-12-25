#nullable disable

using Fundamental.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Fundamental.Migrations.Fundamental
{
    [DbContext(typeof(FundamentalDbContext))]
    [Migration("20231223195921_NoneOperationalIncomeTable")]
    public class NoneOperationalIncomeTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NonOperationIncomeAndExpense",
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
                    Description = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    IsAudited = table.Column<bool>(type: "BIT", nullable: false),
                    CurrentPeriod = table.Column<bool>(type: "BIT", nullable: false),
                    PreviousPeriod = table.Column<bool>(type: "BIT", nullable: false),
                    ForecastPeriod = table.Column<bool>(type: "BIT", nullable: false),
                    FiscalYear = table.Column<short>(type: "SMALLINT", nullable: false),
                    Value = table.Column<decimal>(type: "decimal(36,10)", precision: 36, scale: 10, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NonOperationIncomeAndExpense", x => x._id);
                    table.ForeignKey(
                        name: "FK_NonOperationIncomeAndExpense_Symbol_SymbolId",
                        column: x => x.SymbolId,
                        principalSchema: "shd",
                        principalTable: "Symbol",
                        principalColumn: "_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NonOperationIncomeAndExpense_Id",
                schema: "fs",
                table: "NonOperationIncomeAndExpense",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_NonOperationIncomeAndExpense_SymbolId",
                schema: "fs",
                table: "NonOperationIncomeAndExpense",
                column: "SymbolId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NonOperationIncomeAndExpense",
                schema: "fs");
        }
    }
}