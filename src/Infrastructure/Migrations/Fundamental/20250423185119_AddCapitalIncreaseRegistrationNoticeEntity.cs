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
    [Migration("20250423185119_AddCapitalIncreaseRegistrationNoticeEntity")]
    public class AddCapitalIncreaseRegistrationNoticeEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "fair",
                schema: "ex_areas");

            migrationBuilder.AlterColumn<List<NoneOperationalIncomeTag>>(
                name: "tags",
                schema: "manufacturing",
                table: "non-operation-income-expense",
                type: "none_operational_income_tag[]",
                nullable: false,
                defaultValue: new List<NoneOperationalIncomeTag>(),
                oldClrType: typeof(List<NoneOperationalIncomeTag>),
                oldType: "none_operational_income_tag[]",
                oldDefaultValue: new List<NoneOperationalIncomeTag>());

            migrationBuilder.CreateTable(
                name: "capital_increase_registration_notice",
                schema: "manufacturing",
                columns: table => new
                {
                    _id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    symbol_id = table.Column<long>(type: "bigint", nullable: false),
                    trace_no = table.Column<long>(type: "Bigint", nullable: false),
                    uri = table.Column<string>(type: "character varying(512)", unicode: false, maxLength: 512, nullable: false),
                    start_date = table.Column<DateOnly>(type: "Date", nullable: false),
                    last_extra_assembly_date = table.Column<DateOnly>(type: "Date", nullable: false),
                    primary_market_tracing_no = table.Column<long>(type: "Bigint", nullable: false),
                    currency = table.Column<IsoCurrency>(type: "iso_currency", nullable: false),
                    cash_forceclosure_priority = table.Column<decimal>(type: "numeric(36,10)", precision: 36, scale: 10, nullable: false),
                    cash_incoming = table.Column<decimal>(type: "numeric(36,10)", precision: 36, scale: 10, nullable: false),
                    new_capital = table.Column<decimal>(type: "numeric(36,10)", precision: 36, scale: 10, nullable: false),
                    previous_capital = table.Column<decimal>(type: "numeric(36,10)", precision: 36, scale: 10, nullable: false),
                    reserves = table.Column<decimal>(type: "numeric(36,10)", precision: 36, scale: 10, nullable: false),
                    retained_earning = table.Column<decimal>(type: "numeric(36,10)", precision: 36, scale: 10, nullable: false),
                    revaluation_surplus = table.Column<decimal>(type: "numeric(36,10)", precision: 36, scale: 10, nullable: false),
                    sarf_saham = table.Column<decimal>(type: "numeric(36,10)", precision: 36, scale: 10, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_capital_increase_registration_notice", x => x._id);
                    table.ForeignKey(
                        name: "fk_capital_increase_registration_notice_symbols_symbol_id",
                        column: x => x.symbol_id,
                        principalSchema: "shd",
                        principalTable: "symbol",
                        principalColumn: "_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_capital_increase_registration_notice_id",
                schema: "manufacturing",
                table: "capital_increase_registration_notice",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_capital_increase_registration_notice_symbol_id",
                schema: "manufacturing",
                table: "capital_increase_registration_notice",
                column: "symbol_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "capital_increase_registration_notice",
                schema: "manufacturing");

            migrationBuilder.EnsureSchema(
                name: "ex_areas");

            migrationBuilder.AlterColumn<List<NoneOperationalIncomeTag>>(
                name: "tags",
                schema: "manufacturing",
                table: "non-operation-income-expense",
                type: "none_operational_income_tag[]",
                nullable: false,
                defaultValue: new List<NoneOperationalIncomeTag>(),
                oldClrType: typeof(List<NoneOperationalIncomeTag>),
                oldType: "none_operational_income_tag[]",
                oldDefaultValue: new List<NoneOperationalIncomeTag>());

            migrationBuilder.CreateTable(
                name: "fair",
                schema: "ex_areas",
                columns: table => new
                {
                    _id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    json = table.Column<string>(type: "jsonb", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_fair", x => x._id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_fair_id",
                schema: "ex_areas",
                table: "fair",
                column: "Id",
                unique: true);
        }
    }
}
