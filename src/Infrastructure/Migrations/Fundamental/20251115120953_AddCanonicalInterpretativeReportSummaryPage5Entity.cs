using Fundamental.Domain.Codals.Manufacturing.Enums;
using Fundamental.Domain.Common.Enums;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Fundamental.Migrations.Fundamental
{
    /// <inheritdoc />
    public partial class AddCanonicalInterpretativeReportSummaryPage5Entity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<List<NoneOperationalIncomeTag>>(
                name: "tags",
                schema: "manufacturing",
                table: "non_operation_income_and_expense",
                type: "none_operational_income_tag[]",
                nullable: false,
                defaultValue: new List<NoneOperationalIncomeTag>(),
                oldClrType: typeof(List<NoneOperationalIncomeTag>),
                oldType: "none_operational_income_tag[]",
                oldDefaultValue: new List<NoneOperationalIncomeTag>());

            migrationBuilder.CreateTable(
                name: "canonical_interpretative_report_summary_page5",
                schema: "manufacturing",
                columns: table => new
                {
                    _id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    version = table.Column<string>(type: "text", nullable: false),
                    publish_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    symbol_id = table.Column<long>(type: "bigint", nullable: false),
                    trace_no = table.Column<decimal>(type: "numeric(20,0)", maxLength: 50, nullable: false),
                    uri = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    currency = table.Column<IsoCurrency>(type: "iso_currency", nullable: false),
                    year_end_month = table.Column<short>(type: "smallint", nullable: false),
                    report_month = table.Column<short>(type: "smallint", nullable: false),
                    fiscal_year = table.Column<short>(type: "SMALLINT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    descriptions = table.Column<string>(type: "jsonb", nullable: true),
                    financing_details = table.Column<string>(type: "jsonb", nullable: true),
                    financing_details_estimated = table.Column<string>(type: "jsonb", nullable: true),
                    investment_incomes = table.Column<string>(type: "jsonb", nullable: true),
                    miscellaneous_expenses = table.Column<string>(type: "jsonb", nullable: true),
                    other_non_operating_expenses = table.Column<string>(type: "jsonb", nullable: true),
                    other_operating_incomes = table.Column<string>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_canonical_interpretative_report_summary_page5", x => x._id);
                    table.ForeignKey(
                        name: "fk_canonical_interpretative_report_summary_page5_symbols_symbo",
                        column: x => x.symbol_id,
                        principalSchema: "shd",
                        principalTable: "symbol",
                        principalColumn: "_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_canonical_interpretative_report_summary_page5_id",
                schema: "manufacturing",
                table: "canonical_interpretative_report_summary_page5",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_canonical_interpretative_report_summary_page5_symbol_id",
                schema: "manufacturing",
                table: "canonical_interpretative_report_summary_page5",
                column: "symbol_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "canonical_interpretative_report_summary_page5",
                schema: "manufacturing");

            migrationBuilder.AlterColumn<List<NoneOperationalIncomeTag>>(
                name: "tags",
                schema: "manufacturing",
                table: "non_operation_income_and_expense",
                type: "none_operational_income_tag[]",
                nullable: false,
                defaultValue: new List<NoneOperationalIncomeTag>(),
                oldClrType: typeof(List<NoneOperationalIncomeTag>),
                oldType: "none_operational_income_tag[]",
                oldDefaultValue: new List<NoneOperationalIncomeTag>());
        }
    }
}
