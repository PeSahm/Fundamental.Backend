#nullable disable

using Fundamental.Domain.Common.Enums;
using Fundamental.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Fundamental.Migrations.Fundamental
{
    /// <inheritdoc />
    [DbContext(typeof(FundamentalDbContext))]
    [Migration("20250115201221_RemoveFinancialStatementTable")]
    public class RemoveFinancialStatementTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "financial-statement",
                schema: "fs");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "financial-statement",
                schema: "fs",
                columns: table => new
                {
                    _id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    symbol_id = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "Timestamp", nullable: false),
                    currency = table.Column<IsoCurrency>(type: "iso_currency", nullable: false),
                    trace_no = table.Column<long>(type: "BIGINT", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "Timestamp", nullable: true),
                    Uri = table.Column<string>(type: "character varying(512)", unicode: false, maxLength: 512, nullable: false),
                    asset = table.Column<decimal>(type: "numeric(36,10)", precision: 36, scale: 10, nullable: false),
                    bank_interest_income = table.Column<decimal>(type: "numeric(36,10)", precision: 36, scale: 10, nullable: false),
                    expense = table.Column<decimal>(type: "numeric(36,10)", precision: 36, scale: 10, nullable: false),
                    fiscal_year = table.Column<short>(type: "SMALLINT", nullable: false),
                    gross_profit = table.Column<decimal>(type: "numeric(36,10)", precision: 36, scale: 10, nullable: false),
                    investment_income = table.Column<decimal>(type: "numeric(36,10)", precision: 36, scale: 10, nullable: false),
                    net_profit = table.Column<decimal>(type: "numeric(36,10)", precision: 36, scale: 10, nullable: false),
                    operating_income = table.Column<decimal>(type: "numeric(36,10)", precision: 36, scale: 10, nullable: false),
                    operating_profit = table.Column<decimal>(type: "numeric(36,10)", precision: 36, scale: 10, nullable: false),
                    owners_equity = table.Column<decimal>(type: "numeric(36,10)", precision: 36, scale: 10, nullable: false),
                    receivables = table.Column<decimal>(type: "numeric(36,10)", precision: 36, scale: 10, nullable: false),
                    report_month = table.Column<short>(type: "smallint", nullable: false),
                    year_end_month = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_financial_statement", x => x._id);
                    table.ForeignKey(
                        name: "fk_financial_statement_symbols_symbol_id",
                        column: x => x.symbol_id,
                        principalSchema: "shd",
                        principalTable: "symbol",
                        principalColumn: "_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_financial_statement_id",
                schema: "fs",
                table: "financial-statement",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_financial_statement_symbol_id",
                schema: "fs",
                table: "financial-statement",
                column: "symbol_id");
        }
    }
}