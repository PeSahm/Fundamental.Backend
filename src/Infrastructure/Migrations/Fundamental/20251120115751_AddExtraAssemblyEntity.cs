using Fundamental.Domain.Codals.Manufacturing.Enums;
using Fundamental.Domain.Common.Enums;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Fundamental.Migrations.Fundamental
{
    /// <inheritdoc />
    public partial class AddExtraAssemblyEntity : Migration
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
                name: "canonical_extra_assembly",
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
                    capital_change_state = table.Column<int>(type: "integer", nullable: false),
                    last_share_value = table.Column<int>(type: "integer", nullable: true),
                    last_capital = table.Column<int>(type: "integer", nullable: true),
                    last_share_count = table.Column<long>(type: "bigint", nullable: true),
                    old_address = table.Column<string>(type: "text", nullable: true),
                    new_address = table.Column<string>(type: "text", nullable: true),
                    old_name = table.Column<string>(type: "text", nullable: true),
                    new_name = table.Column<string>(type: "text", nullable: true),
                    old_activity_subject = table.Column<string>(type: "text", nullable: true),
                    new_activity_subject = table.Column<string>(type: "text", nullable: true),
                    old_financial_year_month_length = table.Column<int>(type: "integer", nullable: true),
                    old_financial_year_end_date = table.Column<string>(type: "text", nullable: true),
                    old_financial_year_day_length = table.Column<int>(type: "integer", nullable: true),
                    new_financial_year_end_date = table.Column<string>(type: "text", nullable: true),
                    new_financial_year_month_length = table.Column<string>(type: "text", nullable: true),
                    new_financial_year_day_length = table.Column<string>(type: "text", nullable: true),
                    is_location_change = table.Column<bool>(type: "boolean", nullable: false),
                    is_name_change = table.Column<bool>(type: "boolean", nullable: false),
                    is_activity_subject_change = table.Column<bool>(type: "boolean", nullable: false),
                    is_financial_year_change = table.Column<bool>(type: "boolean", nullable: false),
                    is_decided_clause141 = table.Column<bool>(type: "boolean", nullable: false),
                    decided_clause141des = table.Column<string>(type: "text", nullable: true),
                    is_accord_with_seo_statute_approved = table.Column<bool>(type: "boolean", nullable: false),
                    other_des = table.Column<string>(type: "text", nullable: true),
                    primary_market_tracing_no = table.Column<int>(type: "integer", nullable: true),
                    correction_statute_approved = table.Column<bool>(type: "boolean", nullable: false),
                    fiscal_year = table.Column<short>(type: "SMALLINT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    assembly_board_members = table.Column<string>(type: "jsonb", nullable: true),
                    assembly_chief_members_info = table.Column<string>(type: "jsonb", nullable: true),
                    audit_committee_chairman = table.Column<string>(type: "jsonb", nullable: true),
                    ceo = table.Column<string>(type: "jsonb", nullable: true),
                    extra_assembly_decrease_capital = table.Column<string>(type: "jsonb", nullable: true),
                    extra_assembly_increase_capitals = table.Column<string>(type: "jsonb", nullable: true),
                    extra_assembly_scheduling = table.Column<string>(type: "jsonb", nullable: true),
                    extra_assembly_share_value_change_capital = table.Column<string>(type: "jsonb", nullable: true),
                    next_session_info = table.Column<string>(type: "jsonb", nullable: true),
                    parent_assembly_info = table.Column<string>(type: "jsonb", nullable: true),
                    share_holders = table.Column<string>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_canonical_extra_assembly", x => x._id);
                    table.ForeignKey(
                        name: "fk_canonical_extra_assembly_symbols_symbol_id",
                        column: x => x.symbol_id,
                        principalSchema: "shd",
                        principalTable: "symbol",
                        principalColumn: "_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_canonical_extra_assembly_id",
                schema: "manufacturing",
                table: "canonical_extra_assembly",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_canonical_extra_assembly_symbol_id",
                schema: "manufacturing",
                table: "canonical_extra_assembly",
                column: "symbol_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "canonical_extra_assembly",
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
