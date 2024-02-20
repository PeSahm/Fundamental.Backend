#nullable disable

using Fundamental.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Fundamental.Migrations.Fundamental
{
    [DbContext(typeof(FundamentalDbContext))]
    [Migration("20240220035601_ChangeInPublisherEnum")]
    public class ChangeInPublisherEnum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:company_type",
                    "none_financial_institution,public_investment_and_holding,financial_institutions,subsidiary_financial_institutions,intermediary_institutions,investment_funds,basket_companies,investment_advisory_companies,financial_information_processing_companies,capital_supply_companies,associations,central_asset_management_company,rating_institutions,article44,brokers,government_companies,exempt_companies")
                .Annotation("Npgsql:Enum:enable_sub_company", "in_active,active,accepted")
                .Annotation("Npgsql:Enum:iso_currency", "irr,usd,eur")
                .Annotation("Npgsql:Enum:publisher_fund_type",
                    "un_known,not_a_fund,real_estate,fixed_income,mixed,equity,project,venture,market_making,commodity,diversified")
                .Annotation("Npgsql:Enum:publisher_market_type", "none,first,second,base,small_and_medium")
                .Annotation("Npgsql:Enum:publisher_state",
                    "register_in_ime,register_in_irenex,register_in_tse,register_in_ifb,registered_not_accepted,not_registered")
                .Annotation("Npgsql:Enum:publisher_sub_company_type",
                    "un_known,normal,liquidation,has_foreign_currency_unit,has_foreign_currency_unit_and_foreign_auditor")
                .Annotation("Npgsql:Enum:reporting_type",
                    "production,structural,investment,bank,leasing,services,insurance,maritime_transportation,un_known")
                .OldAnnotation("Npgsql:Enum:company_type",
                    "none_financial_institution,public_investment_and_holding,financial_institutions,subsidiary_financial_institutions,intermediary_institutions,investment_funds,basket_companies,investment_advisory_companies,financial_information_processing_companies,capital_supply_companies,associations,central_asset_management_company,rating_institutions,article44,brokers,government_companies,exempt_companies")
                .OldAnnotation("Npgsql:Enum:enable_sub_company", "in_active,active,accepted")
                .OldAnnotation("Npgsql:Enum:iso_currency", "irr,usd,eur")
                .OldAnnotation("Npgsql:Enum:publisher_fund_type",
                    "un_known,real_estate,fixed_income,mixed,equity,project,venture,market_making,commodity,diversified")
                .OldAnnotation("Npgsql:Enum:publisher_market_type", "none,first,second,base,small_and_medium")
                .OldAnnotation("Npgsql:Enum:publisher_state",
                    "register_in_ime,register_in_irenex,register_in_tse,register_in_ifb,registered_not_accepted,not_registered")
                .OldAnnotation("Npgsql:Enum:publisher_sub_company_type",
                    "normal,liquidation,has_foreign_currency_unit,has_foreign_currency_unit_and_foreign_auditor")
                .OldAnnotation("Npgsql:Enum:reporting_type",
                    "production,structural,investment,bank,leasing,services,insurance,maritime_transportation,un_known");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                    "production,structural,investment,bank,leasing,services,insurance,maritime_transportation,un_known")
                .OldAnnotation("Npgsql:Enum:company_type",
                    "none_financial_institution,public_investment_and_holding,financial_institutions,subsidiary_financial_institutions,intermediary_institutions,investment_funds,basket_companies,investment_advisory_companies,financial_information_processing_companies,capital_supply_companies,associations,central_asset_management_company,rating_institutions,article44,brokers,government_companies,exempt_companies")
                .OldAnnotation("Npgsql:Enum:enable_sub_company", "in_active,active,accepted")
                .OldAnnotation("Npgsql:Enum:iso_currency", "irr,usd,eur")
                .OldAnnotation("Npgsql:Enum:publisher_fund_type",
                    "un_known,not_a_fund,real_estate,fixed_income,mixed,equity,project,venture,market_making,commodity,diversified")
                .OldAnnotation("Npgsql:Enum:publisher_market_type", "none,first,second,base,small_and_medium")
                .OldAnnotation("Npgsql:Enum:publisher_state",
                    "register_in_ime,register_in_irenex,register_in_tse,register_in_ifb,registered_not_accepted,not_registered")
                .OldAnnotation("Npgsql:Enum:publisher_sub_company_type",
                    "un_known,normal,liquidation,has_foreign_currency_unit,has_foreign_currency_unit_and_foreign_auditor")
                .OldAnnotation("Npgsql:Enum:reporting_type",
                    "production,structural,investment,bank,leasing,services,insurance,maritime_transportation,un_known");
        }
    }
}