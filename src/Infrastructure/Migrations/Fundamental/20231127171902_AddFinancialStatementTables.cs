using Fundamental.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fundamental.Migrations.Fundamental
{
    [DbContext(typeof(FundamentalDbContext))]
    [Migration("20231127171902_AddFinancialStatementTables")]
    public class AddFinancialStatementTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "fs");

            migrationBuilder.CreateTable(
                name: "FinancialStatements",
                schema: "fs",
                columns: table => new
                {
                    _id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SymbolId = table.Column<long>(type: "bigint", nullable: false),
                    FiscalYear = table.Column<short>(type: "SMALLINT", nullable: false),
                    Currency = table.Column<string>(type: "char(3)", fixedLength: true, maxLength: 3, nullable: false),
                    YearEndMonth = table.Column<byte>(type: "TINYINT", nullable: false),
                    ReportMonth = table.Column<byte>(type: "TINYINT", nullable: false),
                    OperatingIncome = table.Column<long>(type: "bigint", nullable: false),
                    GrossProfit = table.Column<long>(type: "bigint", nullable: false),
                    OperatingProfit = table.Column<long>(type: "bigint", nullable: false),
                    BankInterestIncome = table.Column<long>(type: "bigint", nullable: false),
                    InvestmentIncome = table.Column<long>(type: "bigint", nullable: false),
                    NetProfit = table.Column<long>(type: "bigint", nullable: false),
                    Expense = table.Column<long>(type: "bigint", nullable: false),
                    Asset = table.Column<long>(type: "bigint", nullable: false),
                    OwnersEquity = table.Column<long>(type: "bigint", nullable: false),
                    Receivables = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinancialStatements", x => x._id);
                    table.ForeignKey(
                        name: "FK_FinancialStatements_Symbol_SymbolId",
                        column: x => x.SymbolId,
                        principalSchema: "shd",
                        principalTable: "Symbol",
                        principalColumn: "_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FinancialStatements_Id",
                schema: "fs",
                table: "FinancialStatements",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FinancialStatements_SymbolId",
                schema: "fs",
                table: "FinancialStatements",
                column: "SymbolId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FinancialStatements",
                schema: "fs");
        }
    }
}