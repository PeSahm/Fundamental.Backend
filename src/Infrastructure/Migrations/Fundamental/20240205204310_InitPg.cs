#nullable disable

using Fundamental.Domain.Common.Enums;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Fundamental.Migrations.Fundamental
{
    /// <inheritdoc />
    public partial class InitPg : Migration
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

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:company_type",
                    "none_financial_institution,public_investment_and_holding,financial_institutions,subsidiary_financial_institutions,intermediary_institutions,investment_funds,basket_companies,investment_advisory_companies,financial_information_processing_companies,capital_supply_companies,associations,central_asset_management_company,rating_institutions,article44,brokers,government_companies,exempt_companies")
                .Annotation("Npgsql:Enum:enable_sub_company", "in_active,active,accepted")
                .Annotation("Npgsql:Enum:iso_currency", "irr,usd,eur")
                .Annotation("Npgsql:Enum:publisher_fund_type",
                    "un_known,real_estate,fixed_income,mixed,equity,project,venture,market_making,commodity,diversified")
                .Annotation("Npgsql:Enum:publisher_market_type", "none,first,second,base,small_and_medium")
                .Annotation("Npgsql:Enum:publisher_state",
                    "register_in_ime,register_in_irenex,register_in_tse,register_in_ifb,registered_not_accepted,not_registered")
                .Annotation("Npgsql:Enum:publisher_sub_company_type",
                    "normal,liquidation,has_foreign_currency_unit,has_foreign_currency_unit_and_foreign_auditor")
                .Annotation("Npgsql:Enum:reporting_type",
                    "production,structural,investment,bank,leasing,services,insurance,maritime_transportation,un_known");

            migrationBuilder.CreateTable(
                name: "balance-sheet-sort",
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
                    CreatedAt = table.Column<DateTime>(type: "Timestamp", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "Timestamp", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_balance_sheet_sort", x => x._id);
                });

            migrationBuilder.CreateTable(
                name: "income-statement-sort",
                schema: "manufacturing",
                columns: table => new
                {
                    _id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    order = table.Column<short>(type: "SMALLINT", nullable: false),
                    codal_row = table.Column<short>(type: "SMALLINT", nullable: false),
                    description = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "Timestamp", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "Timestamp", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_income_statement_sort", x => x._id);
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
                    CreatedAt = table.Column<DateTime>(type: "Timestamp", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "Timestamp", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_symbol", x => x._id);
                });

            migrationBuilder.CreateTable(
                name: "balance-sheet",
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
                    yearendmonth = table.Column<short>(name: "year-end-month", type: "smallint", nullable: false),
                    reportmonth = table.Column<short>(name: "report-month", type: "smallint", nullable: false),
                    row = table.Column<short>(type: "SMALLINT", nullable: false),
                    codal_row = table.Column<short>(type: "SMALLINT", nullable: false),
                    codal_category = table.Column<short>(type: "SMALLINT", nullable: false),
                    description = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    is_audited = table.Column<bool>(type: "boolean", nullable: false),
                    fiscalyear = table.Column<short>(name: "fiscal-year", type: "SMALLINT", nullable: false),
                    value = table.Column<decimal>(type: "numeric(36,10)", precision: 36, scale: 10, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "Timestamp", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "Timestamp", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_balance_sheet", x => x._id);
                    table.ForeignKey(
                        name: "fk_balance_sheet_symbols_symbol_id",
                        column: x => x.symbol_id,
                        principalSchema: "shd",
                        principalTable: "symbol",
                        principalColumn: "_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "close-price",
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
                    CreatedAt = table.Column<DateTime>(type: "Timestamp", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "Timestamp", nullable: true)
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
                name: "financial-statement",
                schema: "fs",
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
                    operating_income = table.Column<decimal>(type: "numeric(36,10)", precision: 36, scale: 10, nullable: false),
                    gross_profit = table.Column<decimal>(type: "numeric(36,10)", precision: 36, scale: 10, nullable: false),
                    operating_profit = table.Column<decimal>(type: "numeric(36,10)", precision: 36, scale: 10, nullable: false),
                    bank_interest_income = table.Column<decimal>(type: "numeric(36,10)", precision: 36, scale: 10, nullable: false),
                    investment_income = table.Column<decimal>(type: "numeric(36,10)", precision: 36, scale: 10, nullable: false),
                    net_profit = table.Column<decimal>(type: "numeric(36,10)", precision: 36, scale: 10, nullable: false),
                    expense = table.Column<decimal>(type: "numeric(36,10)", precision: 36, scale: 10, nullable: false),
                    asset = table.Column<decimal>(type: "numeric(36,10)", precision: 36, scale: 10, nullable: false),
                    owners_equity = table.Column<decimal>(type: "numeric(36,10)", precision: 36, scale: 10, nullable: false),
                    receivables = table.Column<decimal>(type: "numeric(36,10)", precision: 36, scale: 10, nullable: false),
                    fiscal_year = table.Column<short>(type: "SMALLINT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "Timestamp", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "Timestamp", nullable: true)
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
                name: "income-statement",
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
                    yearendmonth = table.Column<short>(name: "year-end-month", type: "smallint", nullable: false),
                    reportmonth = table.Column<short>(name: "report-month", type: "smallint", nullable: false),
                    row = table.Column<int>(type: "integer", nullable: false),
                    codal_row = table.Column<int>(type: "integer", nullable: false),
                    codal_category = table.Column<int>(type: "integer", nullable: false),
                    description = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    is_audited = table.Column<bool>(type: "boolean", nullable: false),
                    fiscalyear = table.Column<short>(name: "fiscal-year", type: "SMALLINT", nullable: false),
                    value_value = table.Column<decimal>(type: "numeric(36,10)", precision: 36, scale: 10, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "Timestamp", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "Timestamp", nullable: true)
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
                name: "monthly-activity",
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
                    has_sub_company_sale = table.Column<bool>(type: "boolean", nullable: false),
                    fiscal_year = table.Column<short>(type: "SMALLINT", nullable: false),
                    report_month = table.Column<short>(type: "smallint", nullable: false),
                    sale_before_current_month = table.Column<decimal>(type: "numeric(36,10)", precision: 36, scale: 10, nullable: false),
                    sale_current_month = table.Column<decimal>(type: "numeric(36,10)", precision: 36, scale: 10, nullable: false),
                    sale_include_current_month = table.Column<decimal>(type: "numeric(36,10)", precision: 36, scale: 10, nullable: false),
                    sale_last_year = table.Column<decimal>(type: "numeric(36,10)", precision: 36, scale: 10, nullable: false),
                    year_end_month = table.Column<short>(type: "smallint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "Timestamp", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "Timestamp", nullable: true)
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

            migrationBuilder.CreateTable(
                name: "non-operation-income-expense",
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
                    description = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: false),
                    is_audited = table.Column<bool>(type: "boolean", nullable: false),
                    current_period = table.Column<bool>(type: "boolean", nullable: false),
                    previous_period = table.Column<bool>(type: "boolean", nullable: false),
                    forecast_period = table.Column<bool>(type: "boolean", nullable: false),
                    fiscal_year = table.Column<short>(type: "SMALLINT", nullable: false),
                    value = table.Column<decimal>(type: "numeric(36,10)", precision: 36, scale: 10, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "Timestamp", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "Timestamp", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_non_operation_income_expense", x => x._id);
                    table.ForeignKey(
                        name: "fk_non_operation_income_expense_symbols_symbol_id",
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
                    CreatedAt = table.Column<DateTime>(type: "Timestamp", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "Timestamp", nullable: true)
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
                name: "symbol-relation",
                schema: "shd",
                columns: table => new
                {
                    _id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    parent_id = table.Column<long>(type: "bigint", nullable: false),
                    ratio = table.Column<decimal>(type: "numeric(18,3)", precision: 18, scale: 3, nullable: false),
                    child_id = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "Timestamp", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "Timestamp", nullable: true)
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
                name: "symbol-share-holders",
                schema: "shd",
                columns: table => new
                {
                    _id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    symbol_id = table.Column<long>(type: "bigint", nullable: false),
                    share_holder_name = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: false),
                    share_percentage = table.Column<decimal>(type: "numeric(18,5)", precision: 18, scale: 5, nullable: false),
                    share_holder_source = table.Column<short>(type: "SMALLINT", nullable: false),
                    review_status = table.Column<short>(type: "SMALLINT", nullable: false),
                    share_holder_symbol_id = table.Column<long>(type: "bigint", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "Timestamp", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "Timestamp", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_symbol_share_holders", x => x._id);
                    table.ForeignKey(
                        name: "fk_symbol_share_holders_symbol_share_holder_symbol_id",
                        column: x => x.share_holder_symbol_id,
                        principalSchema: "shd",
                        principalTable: "symbol",
                        principalColumn: "_id");
                    table.ForeignKey(
                        name: "fk_symbol_share_holders_symbol_symbol_id",
                        column: x => x.symbol_id,
                        principalSchema: "shd",
                        principalTable: "symbol",
                        principalColumn: "_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_balance_sheet_id",
                schema: "manufacturing",
                table: "balance-sheet",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_balance_sheet_symbol_id",
                schema: "manufacturing",
                table: "balance-sheet",
                column: "symbol_id");

            migrationBuilder.CreateIndex(
                name: "ix_balance_sheet_sort_category_codal_row",
                schema: "manufacturing",
                table: "balance-sheet-sort",
                columns: new[] { "category", "codal_row" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_balance_sheet_sort_id",
                schema: "manufacturing",
                table: "balance-sheet-sort",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_balance_sheet_sort_order",
                schema: "manufacturing",
                table: "balance-sheet-sort",
                column: "order",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_close_price_id",
                schema: "shd",
                table: "close-price",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_close_price_symbol_id",
                schema: "shd",
                table: "close-price",
                column: "symbol_id");

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

            migrationBuilder.CreateIndex(
                name: "ix_income_statement_id",
                schema: "manufacturing",
                table: "income-statement",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_income_statement_symbol_id",
                schema: "manufacturing",
                table: "income-statement",
                column: "symbol_id");

            migrationBuilder.CreateIndex(
                name: "ix_income_statement_sort_id",
                schema: "manufacturing",
                table: "income-statement-sort",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_income_statement_sort_order",
                schema: "manufacturing",
                table: "income-statement-sort",
                column: "order",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_monthly_activity_id",
                schema: "manufacturing",
                table: "monthly-activity",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_monthly_activity_symbol_id",
                schema: "manufacturing",
                table: "monthly-activity",
                column: "symbol_id");

            migrationBuilder.CreateIndex(
                name: "ix_non_operation_income_expense_id",
                schema: "manufacturing",
                table: "non-operation-income-expense",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_non_operation_income_expense_symbol_id",
                schema: "manufacturing",
                table: "non-operation-income-expense",
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
                column: "symbol_id");

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
                name: "ix_symbol_relation_child_id",
                schema: "shd",
                table: "symbol-relation",
                column: "child_id");

            migrationBuilder.CreateIndex(
                name: "ix_symbol_relation_id",
                schema: "shd",
                table: "symbol-relation",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_symbol_relation_parent_id",
                schema: "shd",
                table: "symbol-relation",
                column: "parent_id");

            migrationBuilder.CreateIndex(
                name: "ix_symbol_share_holders_id",
                schema: "shd",
                table: "symbol-share-holders",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_symbol_share_holders_share_holder_symbol_id",
                schema: "shd",
                table: "symbol-share-holders",
                column: "share_holder_symbol_id");

            migrationBuilder.CreateIndex(
                name: "ix_symbol_share_holders_symbol_id",
                schema: "shd",
                table: "symbol-share-holders",
                column: "symbol_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "balance-sheet",
                schema: "manufacturing");

            migrationBuilder.DropTable(
                name: "balance-sheet-sort",
                schema: "manufacturing");

            migrationBuilder.DropTable(
                name: "close-price",
                schema: "shd");

            migrationBuilder.DropTable(
                name: "financial-statement",
                schema: "fs");

            migrationBuilder.DropTable(
                name: "income-statement",
                schema: "manufacturing");

            migrationBuilder.DropTable(
                name: "income-statement-sort",
                schema: "manufacturing");

            migrationBuilder.DropTable(
                name: "monthly-activity",
                schema: "manufacturing");

            migrationBuilder.DropTable(
                name: "non-operation-income-expense",
                schema: "manufacturing");

            migrationBuilder.DropTable(
                name: "publisher",
                schema: "fs");

            migrationBuilder.DropTable(
                name: "symbol-relation",
                schema: "shd");

            migrationBuilder.DropTable(
                name: "symbol-share-holders",
                schema: "shd");

            migrationBuilder.DropTable(
                name: "symbol",
                schema: "shd");
        }
    }
}