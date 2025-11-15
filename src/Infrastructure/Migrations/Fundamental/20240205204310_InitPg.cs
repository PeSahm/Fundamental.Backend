#nullable disable

using Fundamental.Domain.Common.Enums;
using Fundamental.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Fundamental.Migrations.Fundamental;

[DbContext(typeof(FundamentalDbContext))]
[Migration("20240205204310_InitPg")]
public class InitPg : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.EnsureSchema(
            "manufacturing");

        migrationBuilder.EnsureSchema(
            "shd");

        migrationBuilder.EnsureSchema(
            "fs");

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
            "balance-sheet-sort",
            schema: "manufacturing",
            columns: table => new
            {
                _id = table.Column<long>("bigint", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                Id = table.Column<Guid>("uuid", nullable: false),
                order = table.Column<short>("SMALLINT", nullable: false),
                codal_row = table.Column<short>("SMALLINT", nullable: false),
                description = table.Column<string>("character varying(512)", maxLength: 512, nullable: false),
                category = table.Column<short>("SMALLINT", nullable: false),
                CreatedAt = table.Column<DateTime>("Timestamp", nullable: false),
                ModifiedAt = table.Column<DateTime>("Timestamp", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_balance_sheet_sort", x => x._id);
            });

        migrationBuilder.CreateTable(
            "income-statement-sort",
            schema: "manufacturing",
            columns: table => new
            {
                _id = table.Column<long>("bigint", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                Id = table.Column<Guid>("uuid", nullable: false),
                order = table.Column<short>("SMALLINT", nullable: false),
                codal_row = table.Column<short>("SMALLINT", nullable: false),
                description = table.Column<string>("character varying(512)", maxLength: 512, nullable: false),
                CreatedAt = table.Column<DateTime>("Timestamp", nullable: false),
                ModifiedAt = table.Column<DateTime>("Timestamp", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_income_statement_sort", x => x._id);
            });

        migrationBuilder.CreateTable(
            "symbol",
            schema: "shd",
            columns: table => new
            {
                _id = table.Column<long>("bigint", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                Id = table.Column<Guid>("uuid", nullable: false),
                isin = table.Column<string>("character varying(15)", false, 15, nullable: false),
                tse_ins_code = table.Column<string>("character varying(40)", false, 40, nullable: false),
                en_name = table.Column<string>("character varying(100)", false, 100, nullable: false),
                symbol_en_name = table.Column<string>("character varying(100)", false, 100, nullable: false),
                title = table.Column<string>("character varying(100)", maxLength: 100, nullable: false),
                name = table.Column<string>("character varying(100)", maxLength: 100, nullable: false),
                company_en_code = table.Column<string>("character varying(100)", false, 100, nullable: false),
                company_persian_name = table.Column<string>("character varying(100)", maxLength: 100, nullable: false),
                company_isin = table.Column<string>("character varying(100)", maxLength: 100, nullable: true),
                market_cap = table.Column<long>("bigint", nullable: false),
                sector_code = table.Column<string>("character varying(50)", maxLength: 50, nullable: true),
                sub_sector_code = table.Column<string>("character varying(50)", maxLength: 50, nullable: true),
                is_un_official = table.Column<bool>("boolean", nullable: false),
                product_type = table.Column<short>("smallint", nullable: false),
                CreatedAt = table.Column<DateTime>("Timestamp", nullable: false),
                ModifiedAt = table.Column<DateTime>("Timestamp", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_symbol", x => x._id);
            });

        migrationBuilder.CreateTable(
            "balance-sheet",
            schema: "manufacturing",
            columns: table => new
            {
                _id = table.Column<long>("bigint", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                Id = table.Column<Guid>("uuid", nullable: false),
                symbol_id = table.Column<long>("bigint", nullable: false),
                trace_no = table.Column<long>("BIGINT", nullable: false),
                uri = table.Column<string>("character varying(512)", maxLength: 512, nullable: false),
                currency = table.Column<IsoCurrency>("iso_currency", nullable: false),
                yearendmonth = table.Column<short>(name: "year-end-month", type: "smallint", nullable: false),
                reportmonth = table.Column<short>(name: "report-month", type: "smallint", nullable: false),
                row = table.Column<short>("SMALLINT", nullable: false),
                codal_row = table.Column<short>("SMALLINT", nullable: false),
                codal_category = table.Column<short>("SMALLINT", nullable: false),
                description = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                is_audited = table.Column<bool>("boolean", nullable: false),
                fiscalyear = table.Column<short>(name: "fiscal-year", type: "SMALLINT", nullable: false),
                value = table.Column<decimal>("numeric(36,10)", precision: 36, scale: 10, nullable: false),
                CreatedAt = table.Column<DateTime>("Timestamp", nullable: false),
                ModifiedAt = table.Column<DateTime>("Timestamp", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_balance_sheet", x => x._id);
                table.ForeignKey(
                    "fk_balance_sheet_symbols_symbol_id",
                    x => x.symbol_id,
                    principalSchema: "shd",
                    principalTable: "symbol",
                    principalColumn: "_id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            "close-price",
            schema: "shd",
            columns: table => new
            {
                _id = table.Column<long>("bigint", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                Id = table.Column<Guid>("uuid", nullable: false),
                symbol_id = table.Column<long>("bigint", nullable: false),
                date = table.Column<DateOnly>("Date", nullable: false),
                close = table.Column<long>("bigint", nullable: false),
                open = table.Column<long>("bigint", nullable: false),
                high = table.Column<long>("bigint", nullable: false),
                low = table.Column<long>("bigint", nullable: false),
                last = table.Column<long>("bigint", nullable: false),
                close_adjusted = table.Column<long>("bigint", nullable: false),
                open_adjusted = table.Column<long>("bigint", nullable: false),
                high_adjusted = table.Column<long>("bigint", nullable: false),
                low_adjusted = table.Column<long>("bigint", nullable: false),
                last_adjusted = table.Column<long>("bigint", nullable: false),
                volume = table.Column<long>("bigint", nullable: false),
                quantity = table.Column<long>("bigint", nullable: false),
                value = table.Column<long>("bigint", nullable: false),
                CreatedAt = table.Column<DateTime>("Timestamp", nullable: false),
                ModifiedAt = table.Column<DateTime>("Timestamp", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_close_price", x => x._id);
                table.ForeignKey(
                    "fk_close_price_symbols_symbol_id",
                    x => x.symbol_id,
                    principalSchema: "shd",
                    principalTable: "symbol",
                    principalColumn: "_id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            "financial-statement",
            schema: "fs",
            columns: table => new
            {
                _id = table.Column<long>("bigint", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                Id = table.Column<Guid>("uuid", nullable: false),
                symbol_id = table.Column<long>("bigint", nullable: false),
                trace_no = table.Column<long>("BIGINT", nullable: false),
                Uri = table.Column<string>("character varying(512)", false, 512, nullable: false),
                currency = table.Column<IsoCurrency>("iso_currency", nullable: false),
                year_end_month = table.Column<short>("smallint", nullable: false),
                report_month = table.Column<short>("smallint", nullable: false),
                operating_income = table.Column<decimal>("numeric(36,10)", precision: 36, scale: 10, nullable: false),
                gross_profit = table.Column<decimal>("numeric(36,10)", precision: 36, scale: 10, nullable: false),
                operating_profit = table.Column<decimal>("numeric(36,10)", precision: 36, scale: 10, nullable: false),
                bank_interest_income = table.Column<decimal>("numeric(36,10)", precision: 36, scale: 10, nullable: false),
                investment_income = table.Column<decimal>("numeric(36,10)", precision: 36, scale: 10, nullable: false),
                net_profit = table.Column<decimal>("numeric(36,10)", precision: 36, scale: 10, nullable: false),
                expense = table.Column<decimal>("numeric(36,10)", precision: 36, scale: 10, nullable: false),
                asset = table.Column<decimal>("numeric(36,10)", precision: 36, scale: 10, nullable: false),
                owners_equity = table.Column<decimal>("numeric(36,10)", precision: 36, scale: 10, nullable: false),
                receivables = table.Column<decimal>("numeric(36,10)", precision: 36, scale: 10, nullable: false),
                fiscal_year = table.Column<short>("SMALLINT", nullable: false),
                CreatedAt = table.Column<DateTime>("Timestamp", nullable: false),
                ModifiedAt = table.Column<DateTime>("Timestamp", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_financial_statement", x => x._id);
                table.ForeignKey(
                    "fk_financial_statement_symbols_symbol_id",
                    x => x.symbol_id,
                    principalSchema: "shd",
                    principalTable: "symbol",
                    principalColumn: "_id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            "income-statement",
            schema: "manufacturing",
            columns: table => new
            {
                _id = table.Column<long>("bigint", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                Id = table.Column<Guid>("uuid", nullable: false),
                symbol_id = table.Column<long>("bigint", nullable: false),
                trace_no = table.Column<long>("BIGINT", nullable: false),
                uri = table.Column<string>("character varying(512)", false, 512, nullable: false),
                currency = table.Column<IsoCurrency>("iso_currency", nullable: false),
                yearendmonth = table.Column<short>(name: "year-end-month", type: "smallint", nullable: false),
                reportmonth = table.Column<short>(name: "report-month", type: "smallint", nullable: false),
                row = table.Column<int>("integer", nullable: false),
                codal_row = table.Column<int>("integer", nullable: false),
                codal_category = table.Column<int>("integer", nullable: false),
                description = table.Column<string>("character varying(256)", maxLength: 256, nullable: true),
                is_audited = table.Column<bool>("boolean", nullable: false),
                fiscalyear = table.Column<short>(name: "fiscal-year", type: "SMALLINT", nullable: false),
                value_value = table.Column<decimal>("numeric(36,10)", precision: 36, scale: 10, nullable: false),
                CreatedAt = table.Column<DateTime>("Timestamp", nullable: false),
                ModifiedAt = table.Column<DateTime>("Timestamp", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_income_statement", x => x._id);
                table.ForeignKey(
                    "fk_income_statement_symbols_symbol_id",
                    x => x.symbol_id,
                    principalSchema: "shd",
                    principalTable: "symbol",
                    principalColumn: "_id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            "monthly-activity",
            schema: "manufacturing",
            columns: table => new
            {
                _id = table.Column<long>("bigint", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                Id = table.Column<Guid>("uuid", nullable: false),
                symbol_id = table.Column<long>("bigint", nullable: false),
                trace_no = table.Column<long>("BIGINT", nullable: false),
                uri = table.Column<string>("character varying(512)", false, 512, nullable: false),
                currency = table.Column<IsoCurrency>("iso_currency", nullable: false),
                has_sub_company_sale = table.Column<bool>("boolean", nullable: false),
                fiscal_year = table.Column<short>("SMALLINT", nullable: false),
                report_month = table.Column<short>("smallint", nullable: false),
                sale_before_current_month = table.Column<decimal>("numeric(36,10)", precision: 36, scale: 10, nullable: false),
                sale_current_month = table.Column<decimal>("numeric(36,10)", precision: 36, scale: 10, nullable: false),
                sale_include_current_month = table.Column<decimal>("numeric(36,10)", precision: 36, scale: 10, nullable: false),
                sale_last_year = table.Column<decimal>("numeric(36,10)", precision: 36, scale: 10, nullable: false),
                year_end_month = table.Column<short>("smallint", nullable: false),
                CreatedAt = table.Column<DateTime>("Timestamp", nullable: false),
                ModifiedAt = table.Column<DateTime>("Timestamp", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_monthly_activity", x => x._id);
                table.ForeignKey(
                    "fk_monthly_activity_symbols_symbol_id",
                    x => x.symbol_id,
                    principalSchema: "shd",
                    principalTable: "symbol",
                    principalColumn: "_id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            "non-operation-income-expense",
            schema: "manufacturing",
            columns: table => new
            {
                _id = table.Column<long>("bigint", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                Id = table.Column<Guid>("uuid", nullable: false),
                symbol_id = table.Column<long>("bigint", nullable: false),
                trace_no = table.Column<long>("BIGINT", nullable: false),
                Uri = table.Column<string>("character varying(512)", false, 512, nullable: false),
                currency = table.Column<IsoCurrency>("iso_currency", nullable: false),
                year_end_month = table.Column<short>("smallint", nullable: false),
                report_month = table.Column<short>("smallint", nullable: false),
                description = table.Column<string>("character varying(512)", maxLength: 512, nullable: false),
                is_audited = table.Column<bool>("boolean", nullable: false),
                current_period = table.Column<bool>("boolean", nullable: false),
                previous_period = table.Column<bool>("boolean", nullable: false),
                forecast_period = table.Column<bool>("boolean", nullable: false),
                fiscal_year = table.Column<short>("SMALLINT", nullable: false),
                value = table.Column<decimal>("numeric(36,10)", precision: 36, scale: 10, nullable: false),
                CreatedAt = table.Column<DateTime>("Timestamp", nullable: false),
                ModifiedAt = table.Column<DateTime>("Timestamp", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_non_operation_income_expense", x => x._id);
                table.ForeignKey(
                    "fk_non_operation_income_expense_symbols_symbol_id",
                    x => x.symbol_id,
                    principalSchema: "shd",
                    principalTable: "symbol",
                    principalColumn: "_id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            "publisher",
            schema: "fs",
            columns: table => new
            {
                _id = table.Column<long>("bigint", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                Id = table.Column<Guid>("uuid", nullable: false),
                codal_id = table.Column<string>("character varying(128)", maxLength: 128, nullable: false),
                symbol_id = table.Column<long>("bigint", nullable: false),
                parent_symbol_id = table.Column<long>("bigint", nullable: true),
                isic = table.Column<string>("character varying(128)", maxLength: 128, nullable: true),
                reporting_type = table.Column<ReportingType>("reporting_type", nullable: false),
                company_type = table.Column<CompanyType>("company_type", nullable: false),
                executive_manager = table.Column<string>("character varying(512)", maxLength: 512, nullable: true),
                address = table.Column<string>("character varying(512)", maxLength: 512, nullable: true),
                tel_no = table.Column<string>("character varying(512)", maxLength: 512, nullable: true),
                fax_no = table.Column<string>("character varying(512)", maxLength: 512, nullable: true),
                activity_subject = table.Column<string>("character varying(2048)", maxLength: 2048, nullable: true),
                office_address = table.Column<string>("character varying(512)", maxLength: 512, nullable: true),
                share_office_address = table.Column<string>("character varying(512)", maxLength: 512, nullable: true),
                website = table.Column<string>("character varying(512)", maxLength: 512, nullable: true),
                email = table.Column<string>("character varying(512)", maxLength: 512, nullable: true),
                state = table.Column<PublisherState>("publisher_state", nullable: false),
                inspector = table.Column<string>("character varying(512)", maxLength: 512, nullable: true),
                financial_manager = table.Column<string>("character varying(512)", maxLength: 512, nullable: true),
                factory_tel = table.Column<string>("character varying(512)", maxLength: 512, nullable: true),
                factory_fax = table.Column<string>("character varying(512)", maxLength: 512, nullable: true),
                office_tel = table.Column<string>("character varying(512)", maxLength: 512, nullable: true),
                office_fax = table.Column<string>("character varying(512)", maxLength: 512, nullable: true),
                share_office_tel = table.Column<string>("character varying(512)", maxLength: 512, nullable: true),
                share_office_fax = table.Column<string>("character varying(512)", maxLength: 512, nullable: true),
                national_code = table.Column<string>("character varying(15)", maxLength: 15, nullable: true),
                financial_year = table.Column<string>("character varying(512)", maxLength: 512, nullable: true),
                currency = table.Column<IsoCurrency>("iso_currency", nullable: false),
                auditor_name = table.Column<string>("character varying(512)", maxLength: 512, nullable: true),
                is_enable_sub_company = table.Column<EnableSubCompany>("enable_sub_company", nullable: false),
                is_enabled = table.Column<bool>("boolean", nullable: false),
                fund_type = table.Column<PublisherFundType>("publisher_fund_type", nullable: false),
                sub_company_type = table.Column<PublisherSubCompanyType>("publisher_sub_company_type", nullable: false),
                is_supplied = table.Column<bool>("boolean", nullable: false),
                market_type = table.Column<PublisherMarketType>("publisher_market_type", nullable: false),
                listed_capital = table.Column<decimal>("numeric(36,10)", precision: 36, scale: 10, nullable: false),
                unauthorized_capital = table.Column<decimal>("numeric(36,10)", precision: 36, scale: 10, nullable: false),
                CreatedAt = table.Column<DateTime>("Timestamp", nullable: false),
                ModifiedAt = table.Column<DateTime>("Timestamp", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_publisher", x => x._id);
                table.ForeignKey(
                    "fk_publisher_symbols_parent_symbol_id",
                    x => x.parent_symbol_id,
                    principalSchema: "shd",
                    principalTable: "symbol",
                    principalColumn: "_id");
                table.ForeignKey(
                    "fk_publisher_symbols_symbol_id",
                    x => x.symbol_id,
                    principalSchema: "shd",
                    principalTable: "symbol",
                    principalColumn: "_id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            "symbol-relation",
            schema: "shd",
            columns: table => new
            {
                _id = table.Column<long>("bigint", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                Id = table.Column<Guid>("uuid", nullable: false),
                parent_id = table.Column<long>("bigint", nullable: false),
                ratio = table.Column<decimal>("numeric(18,3)", precision: 18, scale: 3, nullable: false),
                child_id = table.Column<long>("bigint", nullable: false),
                CreatedAt = table.Column<DateTime>("Timestamp", nullable: false),
                ModifiedAt = table.Column<DateTime>("Timestamp", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_symbol_relation", x => x._id);
                table.ForeignKey(
                    "fk_symbol_relation_symbol_child_id",
                    x => x.child_id,
                    principalSchema: "shd",
                    principalTable: "symbol",
                    principalColumn: "_id");
                table.ForeignKey(
                    "fk_symbol_relation_symbol_parent_id",
                    x => x.parent_id,
                    principalSchema: "shd",
                    principalTable: "symbol",
                    principalColumn: "_id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            "symbol-share-holders",
            schema: "shd",
            columns: table => new
            {
                _id = table.Column<long>("bigint", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                Id = table.Column<Guid>("uuid", nullable: false),
                symbol_id = table.Column<long>("bigint", nullable: false),
                share_holder_name = table.Column<string>("character varying(512)", maxLength: 512, nullable: false),
                share_percentage = table.Column<decimal>("numeric(18,5)", precision: 18, scale: 5, nullable: false),
                share_holder_source = table.Column<short>("SMALLINT", nullable: false),
                review_status = table.Column<short>("SMALLINT", nullable: false),
                share_holder_symbol_id = table.Column<long>("bigint", nullable: true),
                CreatedAt = table.Column<DateTime>("Timestamp", nullable: false),
                ModifiedAt = table.Column<DateTime>("Timestamp", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_symbol_share_holders", x => x._id);
                table.ForeignKey(
                    "fk_symbol_share_holders_symbol_share_holder_symbol_id",
                    x => x.share_holder_symbol_id,
                    principalSchema: "shd",
                    principalTable: "symbol",
                    principalColumn: "_id");
                table.ForeignKey(
                    "fk_symbol_share_holders_symbol_symbol_id",
                    x => x.symbol_id,
                    principalSchema: "shd",
                    principalTable: "symbol",
                    principalColumn: "_id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            "ix_balance_sheet_id",
            schema: "manufacturing",
            table: "balance-sheet",
            column: "Id",
            unique: true);

        migrationBuilder.CreateIndex(
            "ix_balance_sheet_symbol_id",
            schema: "manufacturing",
            table: "balance-sheet",
            column: "symbol_id");

        migrationBuilder.CreateIndex(
            "ix_balance_sheet_sort_category_codal_row",
            schema: "manufacturing",
            table: "balance-sheet-sort",
            columns: new[] { "category", "codal_row" },
            unique: true);

        migrationBuilder.CreateIndex(
            "ix_balance_sheet_sort_id",
            schema: "manufacturing",
            table: "balance-sheet-sort",
            column: "Id",
            unique: true);

        migrationBuilder.CreateIndex(
            "ix_balance_sheet_sort_order",
            schema: "manufacturing",
            table: "balance-sheet-sort",
            column: "order",
            unique: true);

        migrationBuilder.CreateIndex(
            "ix_close_price_id",
            schema: "shd",
            table: "close-price",
            column: "Id",
            unique: true);

        migrationBuilder.CreateIndex(
            "ix_close_price_symbol_id",
            schema: "shd",
            table: "close-price",
            column: "symbol_id");

        migrationBuilder.CreateIndex(
            "ix_financial_statement_id",
            schema: "fs",
            table: "financial-statement",
            column: "Id",
            unique: true);

        migrationBuilder.CreateIndex(
            "ix_financial_statement_symbol_id",
            schema: "fs",
            table: "financial-statement",
            column: "symbol_id");

        migrationBuilder.CreateIndex(
            "ix_income_statement_id",
            schema: "manufacturing",
            table: "income-statement",
            column: "Id",
            unique: true);

        migrationBuilder.CreateIndex(
            "ix_income_statement_symbol_id",
            schema: "manufacturing",
            table: "income-statement",
            column: "symbol_id");

        migrationBuilder.CreateIndex(
            "ix_income_statement_sort_id",
            schema: "manufacturing",
            table: "income-statement-sort",
            column: "Id",
            unique: true);

        migrationBuilder.CreateIndex(
            "ix_income_statement_sort_order",
            schema: "manufacturing",
            table: "income-statement-sort",
            column: "order",
            unique: true);

        migrationBuilder.CreateIndex(
            "ix_monthly_activity_id",
            schema: "manufacturing",
            table: "monthly-activity",
            column: "Id",
            unique: true);

        migrationBuilder.CreateIndex(
            "ix_monthly_activity_symbol_id",
            schema: "manufacturing",
            table: "monthly-activity",
            column: "symbol_id");

        migrationBuilder.CreateIndex(
            "ix_non_operation_income_expense_id",
            schema: "manufacturing",
            table: "non-operation-income-expense",
            column: "Id",
            unique: true);

        migrationBuilder.CreateIndex(
            "ix_non_operation_income_expense_symbol_id",
            schema: "manufacturing",
            table: "non-operation-income-expense",
            column: "symbol_id");

        migrationBuilder.CreateIndex(
            "ix_publisher_id",
            schema: "fs",
            table: "publisher",
            column: "Id",
            unique: true);

        migrationBuilder.CreateIndex(
            "ix_publisher_parent_symbol_id",
            schema: "fs",
            table: "publisher",
            column: "parent_symbol_id");

        migrationBuilder.CreateIndex(
            "ix_publisher_symbol_id",
            schema: "fs",
            table: "publisher",
            column: "symbol_id");

        migrationBuilder.CreateIndex(
            "ix_symbol_id",
            schema: "shd",
            table: "symbol",
            column: "Id",
            unique: true);

        migrationBuilder.CreateIndex(
            "ix_symbol_isin",
            schema: "shd",
            table: "symbol",
            column: "isin",
            unique: true);

        migrationBuilder.CreateIndex(
            "ix_symbol_name",
            schema: "shd",
            table: "symbol",
            column: "name");

        migrationBuilder.CreateIndex(
            "ix_symbol_relation_child_id",
            schema: "shd",
            table: "symbol-relation",
            column: "child_id");

        migrationBuilder.CreateIndex(
            "ix_symbol_relation_id",
            schema: "shd",
            table: "symbol-relation",
            column: "Id",
            unique: true);

        migrationBuilder.CreateIndex(
            "ix_symbol_relation_parent_id",
            schema: "shd",
            table: "symbol-relation",
            column: "parent_id");

        migrationBuilder.CreateIndex(
            "ix_symbol_share_holders_id",
            schema: "shd",
            table: "symbol-share-holders",
            column: "Id",
            unique: true);

        migrationBuilder.CreateIndex(
            "ix_symbol_share_holders_share_holder_symbol_id",
            schema: "shd",
            table: "symbol-share-holders",
            column: "share_holder_symbol_id");

        migrationBuilder.CreateIndex(
            "ix_symbol_share_holders_symbol_id",
            schema: "shd",
            table: "symbol-share-holders",
            column: "symbol_id");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            "balance-sheet",
            "manufacturing");

        migrationBuilder.DropTable(
            "balance-sheet-sort",
            "manufacturing");

        migrationBuilder.DropTable(
            "close-price",
            "shd");

        migrationBuilder.DropTable(
            "financial-statement",
            "fs");

        migrationBuilder.DropTable(
            "income-statement",
            "manufacturing");

        migrationBuilder.DropTable(
            "income-statement-sort",
            "manufacturing");

        migrationBuilder.DropTable(
            "monthly-activity",
            "manufacturing");

        migrationBuilder.DropTable(
            "non-operation-income-expense",
            "manufacturing");

        migrationBuilder.DropTable(
            "publisher",
            "fs");

        migrationBuilder.DropTable(
            "symbol-relation",
            "shd");

        migrationBuilder.DropTable(
            "symbol-share-holders",
            "shd");

        migrationBuilder.DropTable(
            "symbol",
            "shd");
    }
}