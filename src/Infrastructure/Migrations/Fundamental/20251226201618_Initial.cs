#nullable disable

using Fundamental.Domain.Codals.Manufacturing.Enums;
using Fundamental.Domain.Common.Enums;
using Fundamental.Domain.Symbols.Enums;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Fundamental.Migrations.Fundamental
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "manufacturing");

            migrationBuilder.EnsureSchema(
                name: "shd");

            migrationBuilder.EnsureSchema(
                name: "fs");

            migrationBuilder.EnsureSchema(
                name: "codals");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:codal_version", "none,v1,v2,v3,v3o1,v4,v5,v7")
                .Annotation("Npgsql:Enum:company_type",
                    "article44,associations,basket_companies,brokers,capital_supply_companies,central_asset_management_company,exempt_companies,financial_information_processing_companies,financial_institutions,government_companies,intermediary_institutions,investment_advisory_companies,investment_funds,none_financial_institution,public_investment_and_holding,rating_institutions,subsidiary_financial_institutions,un_known1")
                .Annotation("Npgsql:Enum:enable_sub_company", "accepted,active,in_active")
                .Annotation("Npgsql:Enum:etf_type",
                    "energy,equity,fixed_income,gold,land_buildings_and_projects,mixed_income,vc_and_private_funds")
                .Annotation("Npgsql:Enum:exchange_type", "ifb,ime,irenex,none,tse")
                .Annotation("Npgsql:Enum:iso_currency", "eur,irr,usd")
                .Annotation("Npgsql:Enum:none_operational_income_tag", "bank_interest_income,other_renewable_income,stock_dividend_income")
                .Annotation("Npgsql:Enum:product_type",
                    "all,bond,certificate_of_deposit,coupon,energy_electricity,energy_saving_certificate,equity,etf,forward,fund,futures,gold_coin,ime_certificate,ime_certificate_agriculture,ime_certificate_glass,index,intellectual_property,mbs,option_buy,option_sell,other,vc")
                .Annotation("Npgsql:Enum:publisher_fund_type",
                    "commodity,diversified,equity,fixed_income,market_making,mixed,not_a_fund,project,real_estate,un_known,venture")
                .Annotation("Npgsql:Enum:publisher_market_type", "base,first,none,second,small_and_medium")
                .Annotation("Npgsql:Enum:publisher_state",
                    "not_registered,register_in_ifb,register_in_ime,register_in_irenex,register_in_tse,registered_not_accepted")
                .Annotation("Npgsql:Enum:publisher_sub_company_type",
                    "has_foreign_currency_unit,has_foreign_currency_unit_and_foreign_auditor,liquidation,normal,un_known,un_known1,un_known2,un_known3,un_known4,un_known5,un_known6,un_known7")
                .Annotation("Npgsql:Enum:reporting_type",
                    "agriculture,bank,capital_provision,insurance,investment,leasing,maritime_transportation,production,services,structural,un_known")
                .Annotation("Npgsql:Enum:review_status", "approved,pending,rejected");

            migrationBuilder.CreateTable(
                name: "balance_sheet_sort",
                schema: "manufacturing",
                columns: table => new
                {
                    _id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    order = table.Column<short>(type: "SMALLINT", nullable: false),
                    codal_row = table.Column<short>(type: "SMALLINT", nullable: false),
                    description = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: false),
                    category = table.Column<short>(type: "SMALLINT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_balance_sheet_sort", x => x._id);
                });

            migrationBuilder.CreateTable(
                name: "coravel_job_history",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    started_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ended_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    type_full_path = table.Column<string>(type: "text", nullable: true),
                    display_name = table.Column<string>(type: "text", nullable: true),
                    failed = table.Column<bool>(type: "boolean", nullable: false),
                    error_message = table.Column<string>(type: "text", nullable: true),
                    stack_trace = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_coravel_job_history", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "coravel_scheduled_job_history",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ended_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    type_full_path = table.Column<string>(type: "text", nullable: true),
                    display_name = table.Column<string>(type: "text", nullable: true),
                    failed = table.Column<bool>(type: "boolean", nullable: false),
                    error_message = table.Column<string>(type: "text", nullable: true),
                    stack_trace = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_coravel_scheduled_job_history", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "coravel_scheduled_jobs",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    invocable_full_path = table.Column<string>(type: "text", nullable: true),
                    cron_expression = table.Column<string>(type: "text", nullable: true),
                    frequency = table.Column<string>(type: "text", nullable: true),
                    days = table.Column<string>(type: "text", nullable: true),
                    prevent_overlapping = table.Column<bool>(type: "boolean", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    active = table.Column<bool>(type: "boolean", nullable: false),
                    time_zone_info = table.Column<string>(type: "text", nullable: true),
                    run_on_dedicated_thread = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_coravel_scheduled_jobs", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "income_statement_sort",
                schema: "manufacturing",
                columns: table => new
                {
                    _id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    order = table.Column<short>(type: "SMALLINT", nullable: false),
                    codal_row = table.Column<short>(type: "SMALLINT", nullable: false),
                    description = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_income_statement_sort", x => x._id);
                });

            migrationBuilder.CreateTable(
                name: "raw_codal_json",
                schema: "codals",
                columns: table => new
                {
                    _id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    trace_no = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    publish_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    reporting_type = table.Column<ReportingType>(type: "reporting_type", nullable: false),
                    statement_letter_type = table.Column<int>(type: "integer", nullable: false),
                    html_url = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: true),
                    publisher_id = table.Column<long>(type: "BIGINT", nullable: false),
                    isin = table.Column<string>(type: "character varying(48)", maxLength: 48, nullable: true),
                    raw_json = table.Column<string>(type: "jsonb", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_raw_codal_json", x => x._id);
                });

            migrationBuilder.CreateTable(
                name: "symbol",
                schema: "shd",
                columns: table => new
                {
                    _id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    isin = table.Column<string>(type: "character varying(15)", unicode: false, maxLength: 15, nullable: false),
                    tse_ins_code = table.Column<string>(type: "character varying(40)", unicode: false, maxLength: 40, nullable: false),
                    en_name = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: false),
                    symbol_en_name = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: false),
                    title = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    company_en_code = table.Column<string>(type: "character varying(100)", unicode: false, maxLength: 100, nullable: false),
                    company_persian_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    company_isin = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    market_cap = table.Column<long>(type: "bigint", nullable: false),
                    sector_code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    sub_sector_code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    is_un_official = table.Column<bool>(type: "boolean", nullable: false),
                    product_type = table.Column<short>(type: "smallint", nullable: false),
                    product_type2 = table.Column<ProductType>(type: "product_type", nullable: false, defaultValue: ProductType.All),
                    exchange_type = table.Column<ExchangeType>(type: "exchange_type", nullable: false, defaultValue: ExchangeType.None),
                    etf_type = table.Column<EtfType>(type: "etf_type", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_symbol", x => x._id);
                });

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

            migrationBuilder.CreateTable(
                name: "canonical_extra_annual_assembly",
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
                    table.PrimaryKey("pk_canonical_extra_annual_assembly", x => x._id);
                    table.ForeignKey(
                        name: "fk_canonical_extra_annual_assembly_symbols_symbol_id",
                        column: x => x.symbol_id,
                        principalSchema: "shd",
                        principalTable: "symbol",
                        principalColumn: "_id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateTable(
                name: "canonical_monthly_activity",
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

            migrationBuilder.CreateTable(
                name: "close_price",
                schema: "shd",
                columns: table => new
                {
                    _id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    symbol_id = table.Column<long>(type: "bigint", nullable: false),
                    date = table.Column<DateOnly>(type: "Date", nullable: false),
                    close = table.Column<long>(type: "bigint", nullable: false),
                    open = table.Column<long>(type: "bigint", nullable: false),
                    high = table.Column<long>(type: "bigint", nullable: false),
                    low = table.Column<long>(type: "bigint", nullable: false),
                    last = table.Column<long>(type: "bigint", nullable: false),
                    close_adjusted = table.Column<long>(type: "bigint", nullable: false),
                    open_adjusted = table.Column<long>(type: "bigint", nullable: false),
                    high_adjusted = table.Column<long>(type: "bigint", nullable: false),
                    low_adjusted = table.Column<long>(type: "bigint", nullable: false),
                    last_adjusted = table.Column<long>(type: "bigint", nullable: false),
                    volume = table.Column<long>(type: "bigint", nullable: false),
                    quantity = table.Column<long>(type: "bigint", nullable: false),
                    value = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_close_price", x => x._id);
                    table.ForeignKey(
                        name: "fk_close_price_symbols_symbol_id",
                        column: x => x.symbol_id,
                        principalSchema: "shd",
                        principalTable: "symbol",
                        principalColumn: "_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "financial_statement",
                schema: "manufacturing",
                columns: table => new
                {
                    _id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    symbol_id = table.Column<long>(type: "bigint", nullable: false),
                    trace_no = table.Column<long>(type: "BIGINT", nullable: false),
                    currency = table.Column<IsoCurrency>(type: "iso_currency", nullable: false),
                    last_close_price = table.Column<decimal>(type: "numeric(36,10)", precision: 36, scale: 10, nullable: false),
                    market_cap = table.Column<decimal>(type: "numeric(36,10)", precision: 36, scale: 10, nullable: false),
                    market_value = table.Column<decimal>(type: "numeric(36,10)", precision: 36, scale: 10, nullable: false),
                    sale_trace_no = table.Column<long>(type: "BIGINT", nullable: false),
                    this_period_sale_ratio = table.Column<decimal>(type: "numeric(36,10)", precision: 36, scale: 10, nullable: false),
                    this_period_sale_ratio_with_last_year =
                        table.Column<decimal>(type: "numeric(36,10)", precision: 36, scale: 10, nullable: false),
                    gross_margin = table.Column<decimal>(type: "numeric(36,10)", precision: 36, scale: 10, nullable: false),
                    operational_margin = table.Column<decimal>(type: "numeric(36,10)", precision: 36, scale: 10, nullable: false),
                    net_margin = table.Column<decimal>(type: "numeric(36,10)", precision: 36, scale: 10, nullable: false),
                    target_market_value = table.Column<decimal>(type: "numeric(36,10)", precision: 36, scale: 10, nullable: false),
                    target_price = table.Column<decimal>(type: "numeric(36,10)", precision: 36, scale: 10, nullable: false),
                    optimal_buy_price = table.Column<decimal>(type: "numeric(36,10)", precision: 36, scale: 10, nullable: false),
                    pe = table.Column<decimal>(type: "numeric(36,10)", precision: 36, scale: 10, nullable: false),
                    ps = table.Column<decimal>(type: "numeric(36,10)", precision: 36, scale: 10, nullable: false),
                    owners_equity_ratio = table.Column<decimal>(type: "numeric(36,10)", precision: 36, scale: 10, nullable: false),
                    pa = table.Column<decimal>(type: "numeric(36,10)", precision: 36, scale: 10, nullable: false),
                    pb = table.Column<decimal>(type: "numeric(36,10)", precision: 36, scale: 10, nullable: false),
                    receivable_ratio = table.Column<decimal>(type: "numeric(36,10)", precision: 36, scale: 10, nullable: false),
                    net_profit_growth_ratio = table.Column<decimal>(type: "numeric(36,10)", precision: 36, scale: 10, nullable: false),
                    peg = table.Column<decimal>(type: "numeric(36,10)", precision: 36, scale: 10, nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false),
                    assets = table.Column<decimal>(type: "numeric(36,10)", precision: 36, scale: 10, nullable: false),
                    costs = table.Column<decimal>(type: "numeric(36,10)", precision: 36, scale: 10, nullable: false),
                    dps_last_year = table.Column<decimal>(type: "numeric(36,10)", precision: 36, scale: 10, nullable: false),
                    dps_ratio_last_year = table.Column<decimal>(type: "numeric(36,10)", precision: 36, scale: 10, nullable: false),
                    dps_ratio_two_years_ago = table.Column<decimal>(type: "numeric(36,10)", precision: 36, scale: 10, nullable: false),
                    dps_two_years_ago = table.Column<decimal>(type: "numeric(36,10)", precision: 36, scale: 10, nullable: false),
                    fall_operation_income = table.Column<decimal>(type: "numeric(36,10)", precision: 36, scale: 10, nullable: false),
                    fiscal_year = table.Column<short>(type: "SMALLINT", nullable: false),
                    forecast_none_operational_profit =
                        table.Column<decimal>(type: "numeric(36,10)", precision: 36, scale: 10, nullable: false),
                    forecast_operational_profit = table.Column<decimal>(type: "numeric(36,10)", precision: 36, scale: 10, nullable: false),
                    forecast_sale = table.Column<decimal>(type: "numeric(36,10)", precision: 36, scale: 10, nullable: false),
                    forecast_total_profit = table.Column<decimal>(type: "numeric(36,10)", precision: 36, scale: 10, nullable: false),
                    gross_profit_or_loss = table.Column<decimal>(type: "numeric(36,10)", precision: 36, scale: 10, nullable: false),
                    last_year_net_profit_or_loss = table.Column<decimal>(type: "numeric(36,10)", precision: 36, scale: 10, nullable: false),
                    net_profit_or_loss = table.Column<decimal>(type: "numeric(36,10)", precision: 36, scale: 10, nullable: false),
                    none_operational_profit = table.Column<decimal>(type: "numeric(36,10)", precision: 36, scale: 10, nullable: false),
                    operational_income = table.Column<decimal>(type: "numeric(36,10)", precision: 36, scale: 10, nullable: false),
                    operational_profit_or_loss = table.Column<decimal>(type: "numeric(36,10)", precision: 36, scale: 10, nullable: false),
                    other_operational_income = table.Column<decimal>(type: "numeric(36,10)", precision: 36, scale: 10, nullable: false),
                    owners_equity = table.Column<decimal>(type: "numeric(36,10)", precision: 36, scale: 10, nullable: false),
                    receivables = table.Column<decimal>(type: "numeric(36,10)", precision: 36, scale: 10, nullable: false),
                    report_month = table.Column<short>(type: "smallint", nullable: false),
                    sale = table.Column<decimal>(type: "numeric(36,10)", precision: 36, scale: 10, nullable: false),
                    sale_average_exclude_this_period =
                        table.Column<decimal>(type: "numeric(36,10)", precision: 36, scale: 10, nullable: false),
                    sale_average_last_year_same_period =
                        table.Column<decimal>(type: "numeric(36,10)", precision: 36, scale: 10, nullable: false),
                    sale_before_this_month = table.Column<decimal>(type: "numeric(36,10)", precision: 36, scale: 10, nullable: false),
                    sale_last_year_same_period = table.Column<decimal>(type: "numeric(36,10)", precision: 36, scale: 10, nullable: false),
                    sale_month = table.Column<short>(type: "smallint", nullable: false),
                    sale_year_year = table.Column<short>(type: "SMALLINT", nullable: false),
                    spring_operation_income = table.Column<decimal>(type: "numeric(36,10)", precision: 36, scale: 10, nullable: false),
                    summer_operation_income = table.Column<decimal>(type: "numeric(36,10)", precision: 36, scale: 10, nullable: false),
                    total_sale = table.Column<decimal>(type: "numeric(36,10)", precision: 36, scale: 10, nullable: false),
                    winter_operation_income = table.Column<decimal>(type: "numeric(36,10)", precision: 36, scale: 10, nullable: false),
                    year_end_month = table.Column<short>(type: "smallint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    inventory_outstanding_data = table.Column<string>(type: "jsonb", nullable: true),
                    sales_outstanding_data = table.Column<string>(type: "jsonb", nullable: true)
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

            migrationBuilder.CreateTable(
                name: "income_statement",
                schema: "manufacturing",
                columns: table => new
                {
                    _id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    symbol_id = table.Column<long>(type: "bigint", nullable: false),
                    trace_no = table.Column<long>(type: "BIGINT", nullable: false),
                    uri = table.Column<string>(type: "character varying(512)", unicode: false, maxLength: 512, nullable: false),
                    currency = table.Column<IsoCurrency>(type: "iso_currency", nullable: false),
                    year_end_month = table.Column<short>(type: "smallint", nullable: false),
                    report_month = table.Column<short>(type: "smallint", nullable: false),
                    is_audited = table.Column<bool>(type: "boolean", nullable: false),
                    publish_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    fiscal_year = table.Column<short>(type: "SMALLINT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_income_statement", x => x._id);
                    table.ForeignKey(
                        name: "fk_income_statement_symbols_symbol_id",
                        column: x => x.symbol_id,
                        principalSchema: "shd",
                        principalTable: "symbol",
                        principalColumn: "_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "index_company",
                schema: "shd",
                columns: table => new
                {
                    _id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    index_id = table.Column<long>(type: "bigint", nullable: false),
                    company_id = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_index_company", x => x._id);
                    table.ForeignKey(
                        name: "fk_index_company_symbols_company_id",
                        column: x => x.company_id,
                        principalSchema: "shd",
                        principalTable: "symbol",
                        principalColumn: "_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_index_company_symbols_index_id",
                        column: x => x.index_id,
                        principalSchema: "shd",
                        principalTable: "symbol",
                        principalColumn: "_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "indices",
                schema: "shd",
                columns: table => new
                {
                    _id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    symbol_id = table.Column<long>(type: "bigint", nullable: false),
                    date = table.Column<DateOnly>(type: "date", nullable: false),
                    open = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    high = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    low = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    value = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_indices", x => x._id);
                    table.ForeignKey(
                        name: "fk_indices_symbols_symbol_id",
                        column: x => x.symbol_id,
                        principalSchema: "shd",
                        principalTable: "symbol",
                        principalColumn: "_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "non_operation_income_and_expense",
                schema: "manufacturing",
                columns: table => new
                {
                    _id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    symbol_id = table.Column<long>(type: "bigint", nullable: false),
                    trace_no = table.Column<long>(type: "BIGINT", nullable: false),
                    Uri = table.Column<string>(type: "character varying(512)", unicode: false, maxLength: 512, nullable: false),
                    currency = table.Column<IsoCurrency>(type: "iso_currency", nullable: false),
                    year_end_month = table.Column<short>(type: "smallint", nullable: false),
                    report_month = table.Column<short>(type: "smallint", nullable: false),
                    description = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    is_audited = table.Column<bool>(type: "boolean", nullable: false),
                    current_period = table.Column<bool>(type: "boolean", nullable: false),
                    previous_period = table.Column<bool>(type: "boolean", nullable: false),
                    forecast_period = table.Column<bool>(type: "boolean", nullable: false),
                    yearly_forecast_period = table.Column<bool>(type: "boolean", nullable: false, defaultValueSql: "false"),
                    tags = table.Column<List<NoneOperationalIncomeTag>>(type: "none_operational_income_tag[]",
                        nullable: false,
                        defaultValue: new List<NoneOperationalIncomeTag>()),
                    fiscal_year = table.Column<short>(type: "SMALLINT", nullable: false),
                    value = table.Column<decimal>(type: "numeric(36,10)", precision: 36, scale: 10, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_non_operation_income_and_expense", x => x._id);
                    table.ForeignKey(
                        name: "fk_non_operation_income_and_expense_symbols_symbol_id",
                        column: x => x.symbol_id,
                        principalSchema: "shd",
                        principalTable: "symbol",
                        principalColumn: "_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "publisher",
                schema: "fs",
                columns: table => new
                {
                    _id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    codal_id = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    symbol_id = table.Column<long>(type: "bigint", nullable: false),
                    parent_symbol_id = table.Column<long>(type: "bigint", nullable: true),
                    isic = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    reporting_type = table.Column<ReportingType>(type: "reporting_type", nullable: false),
                    company_type = table.Column<CompanyType>(type: "company_type", nullable: false),
                    executive_manager = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    address = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    tel_no = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    fax_no = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    activity_subject = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: true),
                    office_address = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    share_office_address = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    website = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    email = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    state = table.Column<PublisherState>(type: "publisher_state", nullable: false),
                    inspector = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    financial_manager = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    factory_tel = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    factory_fax = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    office_tel = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    office_fax = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    share_office_tel = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    share_office_fax = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    national_code = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: true),
                    financial_year = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    currency = table.Column<IsoCurrency>(type: "iso_currency", nullable: false),
                    auditor_name = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    is_enable_sub_company = table.Column<EnableSubCompany>(type: "enable_sub_company", nullable: false),
                    is_enabled = table.Column<bool>(type: "boolean", nullable: false),
                    fund_type = table.Column<PublisherFundType>(type: "publisher_fund_type", nullable: false),
                    sub_company_type = table.Column<PublisherSubCompanyType>(type: "publisher_sub_company_type", nullable: false),
                    is_supplied = table.Column<bool>(type: "boolean", nullable: false),
                    market_type = table.Column<PublisherMarketType>(type: "publisher_market_type", nullable: false),
                    listed_capital = table.Column<decimal>(type: "numeric(36,10)", precision: 36, scale: 10, nullable: false),
                    unauthorized_capital = table.Column<decimal>(type: "numeric(36,10)", precision: 36, scale: 10, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_publisher", x => x._id);
                    table.ForeignKey(
                        name: "fk_publisher_symbols_parent_symbol_id",
                        column: x => x.parent_symbol_id,
                        principalSchema: "shd",
                        principalTable: "symbol",
                        principalColumn: "_id");
                    table.ForeignKey(
                        name: "fk_publisher_symbols_symbol_id",
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
                    version = table.Column<CodalVersion>(type: "codal_version", nullable: false, defaultValue: CodalVersion.None),
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

            migrationBuilder.CreateTable(
                name: "stock_ownership",
                schema: "manufacturing",
                columns: table => new
                {
                    _id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    parent_symbol_id = table.Column<long>(type: "bigint", nullable: false),
                    subsidiary_symbol_name = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: false),
                    ownership_percentage = table.Column<decimal>(type: "numeric(18,4)", precision: 18, scale: 4, nullable: false),
                    ownership_percentage_provided_by_admin =
                        table.Column<decimal>(type: "numeric(18,4)", precision: 18, scale: 4, nullable: true),
                    subsidiary_symbol_id = table.Column<long>(type: "bigint", nullable: true),
                    review_status = table.Column<ReviewStatus>(type: "review_status", nullable: false, defaultValue: ReviewStatus.Pending),
                    trace_no = table.Column<decimal>(type: "numeric(20,0)", nullable: true),
                    url = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true),
                    currency = table.Column<IsoCurrency>(type: "iso_currency", nullable: false),
                    cost_price = table.Column<decimal>(type: "numeric(36,10)", precision: 36, scale: 10, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_stock_ownership", x => x._id);
                    table.ForeignKey(
                        name: "fk_stock_ownership_symbols_parent_symbol_id",
                        column: x => x.parent_symbol_id,
                        principalSchema: "shd",
                        principalTable: "symbol",
                        principalColumn: "_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_stock_ownership_symbols_subsidiary_symbol_id",
                        column: x => x.subsidiary_symbol_id,
                        principalSchema: "shd",
                        principalTable: "symbol",
                        principalColumn: "_id");
                });

            migrationBuilder.CreateTable(
                name: "symbol_relation",
                schema: "shd",
                columns: table => new
                {
                    _id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    parent_id = table.Column<long>(type: "bigint", nullable: false),
                    ratio = table.Column<decimal>(type: "numeric(18,3)", precision: 18, scale: 3, nullable: false),
                    child_id = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_symbol_relation", x => x._id);
                    table.ForeignKey(
                        name: "fk_symbol_relation_symbol_child_id",
                        column: x => x.child_id,
                        principalSchema: "shd",
                        principalTable: "symbol",
                        principalColumn: "_id");
                    table.ForeignKey(
                        name: "fk_symbol_relation_symbol_parent_id",
                        column: x => x.parent_id,
                        principalSchema: "shd",
                        principalTable: "symbol",
                        principalColumn: "_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "symbol_share_holder",
                schema: "shd",
                columns: table => new
                {
                    _id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    symbol_id = table.Column<long>(type: "bigint", nullable: false),
                    share_holder_name = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: false),
                    share_percentage = table.Column<decimal>(type: "numeric(18,5)", precision: 18, scale: 5, nullable: false),
                    review_status = table.Column<ReviewStatus>(type: "review_status", nullable: false),
                    share_holder_symbol_id = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_symbol_share_holder", x => x._id);
                    table.ForeignKey(
                        name: "fk_symbol_share_holder_symbol_share_holder_symbol_id",
                        column: x => x.share_holder_symbol_id,
                        principalSchema: "shd",
                        principalTable: "symbol",
                        principalColumn: "_id");
                    table.ForeignKey(
                        name: "fk_symbol_share_holder_symbol_symbol_id",
                        column: x => x.symbol_id,
                        principalSchema: "shd",
                        principalTable: "symbol",
                        principalColumn: "_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "balance_sheet",
                schema: "manufacturing",
                columns: table => new
                {
                    _id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    symbol_id = table.Column<long>(type: "bigint", nullable: false),
                    trace_no = table.Column<long>(type: "BIGINT", nullable: false),
                    uri = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: false),
                    currency = table.Column<IsoCurrency>(type: "iso_currency", nullable: false),
                    year_end_month = table.Column<short>(type: "smallint", nullable: false),
                    report_month = table.Column<short>(type: "smallint", nullable: false),
                    is_audited = table.Column<bool>(type: "boolean", nullable: false),
                    financial_statement_id = table.Column<long>(type: "bigint", nullable: true),
                    publish_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    fiscal_year = table.Column<short>(type: "SMALLINT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_balance_sheet", x => x._id);
                    table.ForeignKey(
                        name: "fk_balance_sheet_manufacturing_financial_statement_financial_s",
                        column: x => x.financial_statement_id,
                        principalSchema: "manufacturing",
                        principalTable: "financial_statement",
                        principalColumn: "_id");
                    table.ForeignKey(
                        name: "fk_balance_sheet_symbols_symbol_id",
                        column: x => x.symbol_id,
                        principalSchema: "shd",
                        principalTable: "symbol",
                        principalColumn: "_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "income_statement_details",
                schema: "manufacturing",
                columns: table => new
                {
                    _id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    income_statement_id = table.Column<long>(type: "bigint", nullable: false),
                    row = table.Column<int>(type: "integer", nullable: false),
                    codal_row = table.Column<int>(type: "integer", nullable: false),
                    description = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    currency = table.Column<IsoCurrency>(type: "iso_currency", nullable: false),
                    value = table.Column<decimal>(type: "numeric(36,10)", precision: 36, scale: 10, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_income_statement_details", x => x._id);
                    table.ForeignKey(
                        name: "fk_income_statement_details_income_statement_income_statement_",
                        column: x => x.income_statement_id,
                        principalSchema: "manufacturing",
                        principalTable: "income_statement",
                        principalColumn: "_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "balance_sheet_detail",
                schema: "manufacturing",
                columns: table => new
                {
                    _id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    balance_sheet_id = table.Column<long>(type: "bigint", nullable: false),
                    row = table.Column<short>(type: "SMALLINT", nullable: false),
                    codal_row = table.Column<short>(type: "SMALLINT", nullable: false),
                    codal_category = table.Column<short>(type: "SMALLINT", nullable: false),
                    description = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    currency = table.Column<IsoCurrency>(type: "iso_currency", nullable: false),
                    value = table.Column<decimal>(type: "numeric(36,10)", precision: 36, scale: 10, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_balance_sheet_detail", x => x._id);
                    table.ForeignKey(
                        name: "fk_balance_sheet_detail_balance_sheet_balance_sheet_id",
                        column: x => x.balance_sheet_id,
                        principalSchema: "manufacturing",
                        principalTable: "balance_sheet",
                        principalColumn: "_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_balance_sheet_financial_statement_id",
                schema: "manufacturing",
                table: "balance_sheet",
                column: "financial_statement_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_balance_sheet_id",
                schema: "manufacturing",
                table: "balance_sheet",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_balance_sheet_publish_date",
                schema: "manufacturing",
                table: "balance_sheet",
                column: "publish_date");

            migrationBuilder.CreateIndex(
                name: "ix_balance_sheet_symbol_id",
                schema: "manufacturing",
                table: "balance_sheet",
                column: "symbol_id");

            migrationBuilder.CreateIndex(
                name: "ix_balance_sheet_trace_no",
                schema: "manufacturing",
                table: "balance_sheet",
                column: "trace_no");

            migrationBuilder.CreateIndex(
                name: "ix_balance_sheet_detail_balance_sheet_id",
                schema: "manufacturing",
                table: "balance_sheet_detail",
                column: "balance_sheet_id");

            migrationBuilder.CreateIndex(
                name: "ix_balance_sheet_detail_balance_sheet_id_row",
                schema: "manufacturing",
                table: "balance_sheet_detail",
                columns: new[] { "balance_sheet_id", "row" });

            migrationBuilder.CreateIndex(
                name: "ix_balance_sheet_detail_category_row",
                schema: "manufacturing",
                table: "balance_sheet_detail",
                columns: new[] { "codal_category", "codal_row" });

            migrationBuilder.CreateIndex(
                name: "ix_balance_sheet_detail_codal_category",
                schema: "manufacturing",
                table: "balance_sheet_detail",
                column: "codal_category");

            migrationBuilder.CreateIndex(
                name: "ix_balance_sheet_detail_codal_row",
                schema: "manufacturing",
                table: "balance_sheet_detail",
                column: "codal_row");

            migrationBuilder.CreateIndex(
                name: "ix_balance_sheet_detail_id",
                schema: "manufacturing",
                table: "balance_sheet_detail",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_balance_sheet_sort_category_codal_row",
                schema: "manufacturing",
                table: "balance_sheet_sort",
                columns: new[] { "category", "codal_row" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_balance_sheet_sort_id",
                schema: "manufacturing",
                table: "balance_sheet_sort",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_balance_sheet_sort_order",
                schema: "manufacturing",
                table: "balance_sheet_sort",
                column: "order",
                unique: true);

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

            migrationBuilder.CreateIndex(
                name: "ix_canonical_extra_annual_assembly_id",
                schema: "manufacturing",
                table: "canonical_extra_annual_assembly",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_canonical_extra_annual_assembly_symbol_id",
                schema: "manufacturing",
                table: "canonical_extra_annual_assembly",
                column: "symbol_id");

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

            migrationBuilder.CreateIndex(
                name: "ix_close_price_id",
                schema: "shd",
                table: "close_price",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_close_price_symbol_id",
                schema: "shd",
                table: "close_price",
                column: "symbol_id");

            migrationBuilder.CreateIndex(
                name: "ix_coravel_job_history_ended_at",
                table: "coravel_job_history",
                column: "ended_at");

            migrationBuilder.CreateIndex(
                name: "ix_coravel_scheduled_job_history_ended_at",
                table: "coravel_scheduled_job_history",
                column: "ended_at");

            migrationBuilder.CreateIndex(
                name: "ix_coravel_scheduled_jobs_active",
                table: "coravel_scheduled_jobs",
                column: "active");

            migrationBuilder.CreateIndex(
                name: "ix_coravel_scheduled_jobs_invocable_full_path",
                table: "coravel_scheduled_jobs",
                column: "invocable_full_path");

            migrationBuilder.CreateIndex(
                name: "ix_financial_statement_id",
                schema: "manufacturing",
                table: "financial_statement",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_financial_statement_performance_metrics",
                schema: "manufacturing",
                table: "financial_statement",
                columns: new[] { "last_close_price", "pe", "market_value" });

            migrationBuilder.CreateIndex(
                name: "ix_financial_statement_symbol_id",
                schema: "manufacturing",
                table: "financial_statement",
                column: "symbol_id");

            migrationBuilder.CreateIndex(
                name: "ix_income_statement_id",
                schema: "manufacturing",
                table: "income_statement",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_income_statement_symbol_id",
                schema: "manufacturing",
                table: "income_statement",
                column: "symbol_id");

            migrationBuilder.CreateIndex(
                name: "ix_income_statement_details_id",
                schema: "manufacturing",
                table: "income_statement_details",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_income_statement_details_income_statement_id",
                schema: "manufacturing",
                table: "income_statement_details",
                column: "income_statement_id");

            migrationBuilder.CreateIndex(
                name: "ix_income_statement_sort_id",
                schema: "manufacturing",
                table: "income_statement_sort",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_income_statement_sort_order",
                schema: "manufacturing",
                table: "income_statement_sort",
                column: "order",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_index_company_company_id",
                schema: "shd",
                table: "index_company",
                column: "company_id");

            migrationBuilder.CreateIndex(
                name: "ix_index_company_id",
                schema: "shd",
                table: "index_company",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_index_company_index_id",
                schema: "shd",
                table: "index_company",
                column: "index_id");

            migrationBuilder.CreateIndex(
                name: "ix_indices_date_symbol_id",
                schema: "shd",
                table: "indices",
                columns: new[] { "date", "symbol_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_indices_id",
                schema: "shd",
                table: "indices",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_indices_symbol_id",
                schema: "shd",
                table: "indices",
                column: "symbol_id");

            migrationBuilder.CreateIndex(
                name: "ix_non_operation_income_and_expense_id",
                schema: "manufacturing",
                table: "non_operation_income_and_expense",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_non_operation_income_and_expense_symbol_id",
                schema: "manufacturing",
                table: "non_operation_income_and_expense",
                column: "symbol_id");

            migrationBuilder.CreateIndex(
                name: "ix_publisher_id",
                schema: "fs",
                table: "publisher",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_publisher_parent_symbol_id",
                schema: "fs",
                table: "publisher",
                column: "parent_symbol_id");

            migrationBuilder.CreateIndex(
                name: "ix_publisher_symbol_id",
                schema: "fs",
                table: "publisher",
                column: "symbol_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_raw_codal_json_id",
                schema: "codals",
                table: "raw_codal_json",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_raw_codal_json_trace_no",
                schema: "codals",
                table: "raw_codal_json",
                column: "trace_no",
                unique: true);

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

            migrationBuilder.CreateIndex(
                name: "ix_stock_ownership_id",
                schema: "manufacturing",
                table: "stock_ownership",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_stock_ownership_parent_symbol_id",
                schema: "manufacturing",
                table: "stock_ownership",
                column: "parent_symbol_id");

            migrationBuilder.CreateIndex(
                name: "ix_stock_ownership_subsidiary_symbol_id",
                schema: "manufacturing",
                table: "stock_ownership",
                column: "subsidiary_symbol_id");

            migrationBuilder.CreateIndex(
                name: "ix_symbol_id",
                schema: "shd",
                table: "symbol",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_symbol_isin",
                schema: "shd",
                table: "symbol",
                column: "isin",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_symbol_name",
                schema: "shd",
                table: "symbol",
                column: "name");

            migrationBuilder.CreateIndex(
                name: "ix_symbol_sector_codes",
                schema: "shd",
                table: "symbol",
                columns: new[] { "sector_code", "sub_sector_code" });

            migrationBuilder.CreateIndex(
                name: "ix_symbol_relation_child_id",
                schema: "shd",
                table: "symbol_relation",
                column: "child_id");

            migrationBuilder.CreateIndex(
                name: "ix_symbol_relation_id",
                schema: "shd",
                table: "symbol_relation",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_symbol_relation_parent_id",
                schema: "shd",
                table: "symbol_relation",
                column: "parent_id");

            migrationBuilder.CreateIndex(
                name: "ix_symbol_share_holder_id",
                schema: "shd",
                table: "symbol_share_holder",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_symbol_share_holder_share_holder_symbol_id",
                schema: "shd",
                table: "symbol_share_holder",
                column: "share_holder_symbol_id");

            migrationBuilder.CreateIndex(
                name: "ix_symbol_share_holder_symbol_id",
                schema: "shd",
                table: "symbol_share_holder",
                column: "symbol_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "balance_sheet_detail",
                schema: "manufacturing");

            migrationBuilder.DropTable(
                name: "balance_sheet_sort",
                schema: "manufacturing");

            migrationBuilder.DropTable(
                name: "canonical_annual_assembly",
                schema: "manufacturing");

            migrationBuilder.DropTable(
                name: "canonical_extra_annual_assembly",
                schema: "manufacturing");

            migrationBuilder.DropTable(
                name: "canonical_extra_assembly",
                schema: "manufacturing");

            migrationBuilder.DropTable(
                name: "canonical_interpretative_report_summary_page5",
                schema: "manufacturing");

            migrationBuilder.DropTable(
                name: "canonical_monthly_activity",
                schema: "manufacturing");

            migrationBuilder.DropTable(
                name: "capital_increase_registration_notice",
                schema: "manufacturing");

            migrationBuilder.DropTable(
                name: "close_price",
                schema: "shd");

            migrationBuilder.DropTable(
                name: "coravel_job_history");

            migrationBuilder.DropTable(
                name: "coravel_scheduled_job_history");

            migrationBuilder.DropTable(
                name: "coravel_scheduled_jobs");

            migrationBuilder.DropTable(
                name: "income_statement_details",
                schema: "manufacturing");

            migrationBuilder.DropTable(
                name: "income_statement_sort",
                schema: "manufacturing");

            migrationBuilder.DropTable(
                name: "index_company",
                schema: "shd");

            migrationBuilder.DropTable(
                name: "indices",
                schema: "shd");

            migrationBuilder.DropTable(
                name: "non_operation_income_and_expense",
                schema: "manufacturing");

            migrationBuilder.DropTable(
                name: "publisher",
                schema: "fs");

            migrationBuilder.DropTable(
                name: "raw_codal_json",
                schema: "codals");

            migrationBuilder.DropTable(
                name: "raw_monthly_activity_json",
                schema: "manufacturing");

            migrationBuilder.DropTable(
                name: "stock_ownership",
                schema: "manufacturing");

            migrationBuilder.DropTable(
                name: "symbol_relation",
                schema: "shd");

            migrationBuilder.DropTable(
                name: "symbol_share_holder",
                schema: "shd");

            migrationBuilder.DropTable(
                name: "balance_sheet",
                schema: "manufacturing");

            migrationBuilder.DropTable(
                name: "income_statement",
                schema: "manufacturing");

            migrationBuilder.DropTable(
                name: "financial_statement",
                schema: "manufacturing");

            migrationBuilder.DropTable(
                name: "symbol",
                schema: "shd");
        }
    }
}