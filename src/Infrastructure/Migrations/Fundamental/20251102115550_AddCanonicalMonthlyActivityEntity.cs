using System;
using System.Collections.Generic;
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
    [Migration("20251102115550_AddCanonicalMonthlyActivityEntity")]
    public class AddCanonicalMonthlyActivityEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "monthly_activity",
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

            migrationBuilder.CreateTable(
                name: "canonical_monthly_activity",
                schema: "manufacturing",
                columns: table => new
                {
                    _id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    version = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    symbol_id = table.Column<long>(type: "bigint", nullable: false),
                    trace_no = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    uri = table.Column<string>(type: "text", nullable: false),
                    currency = table.Column<IsoCurrency>(type: "iso_currency", nullable: false),
                    year_end_month = table.Column<short>(type: "smallint", nullable: false),
                    report_month = table.Column<short>(type: "smallint", nullable: false),
                    has_sub_company_sale = table.Column<bool>(type: "boolean", nullable: false),
                    fiscal_year = table.Column<short>(type: "SMALLINT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    buy_raw_material_items = table.Column<string>(type: "jsonb", nullable: true),
                    currency_exchange_items = table.Column<string>(type: "jsonb", nullable: true),
                    descriptions = table.Column<string>(type: "jsonb", nullable: true),
                    energy_items = table.Column<string>(type: "jsonb", nullable: true),
                    production_and_sales_items = table.Column<string>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_canonical_monthly_activity", x => x._id);
                    table.ForeignKey(
                        name: "fk_canonical_monthly_activity_symbols_symbol_id",
                        column: x => x.symbol_id,
                        principalSchema: "shd",
                        principalTable: "symbol",
                        principalColumn: "_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "raw_monthly_activity_json",
                schema: "manufacturing",
                columns: table => new
                {
                    _id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    trace_no = table.Column<long>(type: "BIGINT", nullable: false),
                    symbol_id = table.Column<long>(type: "bigint", nullable: false),
                    publish_date = table.Column<DateTime>(type: "timestamp", nullable: false),
                    version = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    raw_json = table.Column<string>(type: "jsonb", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_raw_monthly_activity_json", x => x._id);
                    table.ForeignKey(
                        name: "fk_raw_monthly_activity_json_symbols_symbol_id",
                        column: x => x.symbol_id,
                        principalSchema: "shd",
                        principalTable: "symbol",
                        principalColumn: "_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_canonical_monthly_activity_id",
                schema: "manufacturing",
                table: "canonical_monthly_activity",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_canonical_monthly_activity_symbol_id",
                schema: "manufacturing",
                table: "canonical_monthly_activity",
                column: "symbol_id");

            migrationBuilder.CreateIndex(
                name: "ix_raw_monthly_activity_json_id",
                schema: "manufacturing",
                table: "raw_monthly_activity_json",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_raw_monthly_activity_json_symbol_id",
                schema: "manufacturing",
                table: "raw_monthly_activity_json",
                column: "symbol_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "canonical_monthly_activity",
                schema: "manufacturing");

            migrationBuilder.DropTable(
                name: "raw_monthly_activity_json",
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

            migrationBuilder.CreateTable(
                name: "monthly_activity",
                schema: "manufacturing",
                columns: table => new
                {
                    _id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    symbol_id = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    currency = table.Column<IsoCurrency>(type: "iso_currency", nullable: false),
                    has_sub_company_sale = table.Column<bool>(type: "boolean", nullable: false),
                    trace_no = table.Column<long>(type: "BIGINT", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    uri = table.Column<string>(type: "character varying(512)", unicode: false, maxLength: 512, nullable: false),
                    fiscal_year = table.Column<short>(type: "SMALLINT", nullable: false),
                    report_month = table.Column<short>(type: "smallint", nullable: false),
                    sale_before_current_month = table.Column<decimal>(type: "numeric(36,10)", precision: 36, scale: 10, nullable: false),
                    sale_current_month = table.Column<decimal>(type: "numeric(36,10)", precision: 36, scale: 10, nullable: false),
                    sale_include_current_month = table.Column<decimal>(type: "numeric(36,10)", precision: 36, scale: 10, nullable: false),
                    sale_last_year = table.Column<decimal>(type: "numeric(36,10)", precision: 36, scale: 10, nullable: false),
                    year_end_month = table.Column<short>(type: "smallint", nullable: false),
                    extra_sales_infos = table.Column<string>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_monthly_activity", x => x._id);
                    table.ForeignKey(
                        name: "fk_monthly_activity_symbols_symbol_id",
                        column: x => x.symbol_id,
                        principalSchema: "shd",
                        principalTable: "symbol",
                        principalColumn: "_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_monthly_activity_id",
                schema: "manufacturing",
                table: "monthly_activity",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_monthly_activity_symbol_id",
                schema: "manufacturing",
                table: "monthly_activity",
                column: "symbol_id");
        }
    }
}
