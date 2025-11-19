using Fundamental.Domain.Codals.Manufacturing.Enums;
using Fundamental.Domain.Common.Enums;
using Fundamental.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Fundamental.Migrations.Fundamental
{
    /// <inheritdoc />
    [DbContext(typeof(FundamentalDbContext))]
    [Migration("20251116091506_AddCanonicalAnnualAssemblyEntity")]
    public class AddCanonicalAnnualAssemblyEntity : Migration
    {
        /// <summary>
        /// Applies schema changes that add the canonical_annual_assembly entity and update the tags column on non_operation_income_and_expense.
        /// </summary>
        /// <remarks>
        /// Alters manufacturing.non_operation_income_and_expense.tags to use the PostgreSQL array type `none_operational_income_tag[]` with a non-null default empty list, and creates the manufacturing.canonical_annual_assembly table with its columns, primary key, a foreign key to shd.symbol(symbol_id) with cascade delete, and unique/non-unique indexes on `Id` and `symbol_id` respectively.
        /// </remarks>
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
                name: "canonical_annual_assembly",
                schema: "manufacturing",
                columns: table => new
                {
                    _id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    version = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    publish_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    symbol_id = table.Column<long>(type: "bigint", nullable: false),
                    trace_no = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    html_url = table.Column<string>(type: "text", nullable: false),
                    currency = table.Column<IsoCurrency>(type: "iso_currency", nullable: false),
                    year_end_month = table.Column<short>(type: "smallint", nullable: false),
                    report_month = table.Column<short>(type: "smallint", nullable: false),
                    assembly_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    board_member_period = table.Column<int>(type: "integer", nullable: true),
                    publish_security_description = table.Column<string>(type: "text", nullable: true),
                    other_description = table.Column<string>(type: "text", nullable: true),
                    new_hour = table.Column<string>(type: "text", nullable: true),
                    new_day = table.Column<string>(type: "text", nullable: true),
                    new_date = table.Column<string>(type: "text", nullable: true),
                    new_location = table.Column<string>(type: "text", nullable: true),
                    break_description = table.Column<string>(type: "text", nullable: true),
                    fiscal_year = table.Column<short>(type: "SMALLINT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    assembly_board_members = table.Column<string>(type: "jsonb", nullable: true),
                    assembly_chief_members_info = table.Column<string>(type: "jsonb", nullable: true),
                    assembly_interims = table.Column<string>(type: "jsonb", nullable: true),
                    audit_committee_chairman = table.Column<string>(type: "jsonb", nullable: true),
                    board_member_wage_and_gifts = table.Column<string>(type: "jsonb", nullable: true),
                    ceo = table.Column<string>(type: "jsonb", nullable: true),
                    independent_auditor_representative = table.Column<string>(type: "jsonb", nullable: true),
                    inspectors = table.Column<string>(type: "jsonb", nullable: true),
                    new_board_members = table.Column<string>(type: "jsonb", nullable: true),
                    news_papers = table.Column<string>(type: "jsonb", nullable: true),
                    parent_assembly_info = table.Column<string>(type: "jsonb", nullable: true),
                    proportioned_retained_earnings = table.Column<string>(type: "jsonb", nullable: true),
                    share_holders = table.Column<string>(type: "jsonb", nullable: true),
                    top_financial_position = table.Column<string>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_canonical_annual_assembly", x => x._id);
                    table.ForeignKey(
                        name: "fk_canonical_annual_assembly_symbols_symbol_id",
                        column: x => x.symbol_id,
                        principalSchema: "shd",
                        principalTable: "symbol",
                        principalColumn: "_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_canonical_annual_assembly_id",
                schema: "manufacturing",
                table: "canonical_annual_assembly",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_canonical_annual_assembly_symbol_id",
                schema: "manufacturing",
                table: "canonical_annual_assembly",
                column: "symbol_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "canonical_annual_assembly",
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