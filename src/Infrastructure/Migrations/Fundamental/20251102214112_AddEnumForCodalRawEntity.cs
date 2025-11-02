using System.Collections.Generic;
using Fundamental.Domain.Codals.Manufacturing.Enums;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fundamental.Migrations.Fundamental
{
    /// <inheritdoc />
    public partial class AddEnumForCodalRawEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:codal_version", "none,v1,v2,v3,v3o1,v4,v5,v7")
                .Annotation("Npgsql:Enum:company_type", "article44,associations,basket_companies,brokers,capital_supply_companies,central_asset_management_company,exempt_companies,financial_information_processing_companies,financial_institutions,government_companies,intermediary_institutions,investment_advisory_companies,investment_funds,none_financial_institution,public_investment_and_holding,rating_institutions,subsidiary_financial_institutions,un_known1")
                .Annotation("Npgsql:Enum:enable_sub_company", "accepted,active,in_active")
                .Annotation("Npgsql:Enum:etf_type", "equity,fixed_income,gold,land_buildings_and_projects,mixed_income,vc_and_private_funds")
                .Annotation("Npgsql:Enum:exchange_type", "ifb,ime,irenex,none,tse")
                .Annotation("Npgsql:Enum:iso_currency", "eur,irr,usd")
                .Annotation("Npgsql:Enum:none_operational_income_tag", "bank_interest_income,other_renewable_income,stock_dividend_income")
                .Annotation("Npgsql:Enum:product_type", "all,bond,certificate_of_deposit,coupon,energy_electricity,equity,etf,forward,fund,futures,gold_coin,ime_certificate,ime_certificate_agriculture,ime_certificate_glass,index,intellectual_property,mbs,option_buy,option_sell,other,vc")
                .Annotation("Npgsql:Enum:publisher_fund_type", "commodity,diversified,equity,fixed_income,market_making,mixed,not_a_fund,project,real_estate,un_known,venture")
                .Annotation("Npgsql:Enum:publisher_market_type", "base,first,none,second,small_and_medium")
                .Annotation("Npgsql:Enum:publisher_state", "not_registered,register_in_ifb,register_in_ime,register_in_irenex,register_in_tse,registered_not_accepted")
                .Annotation("Npgsql:Enum:publisher_sub_company_type", "has_foreign_currency_unit,has_foreign_currency_unit_and_foreign_auditor,liquidation,normal,un_known,un_known1,un_known2,un_known3,un_known4,un_known5,un_known6,un_known7")
                .Annotation("Npgsql:Enum:reporting_type", "agriculture,bank,capital_provision,insurance,investment,leasing,maritime_transportation,production,services,structural,un_known")
                .Annotation("Npgsql:Enum:review_status", "approved,pending,rejected")
                .OldAnnotation("Npgsql:Enum:company_type", "article44,associations,basket_companies,brokers,capital_supply_companies,central_asset_management_company,exempt_companies,financial_information_processing_companies,financial_institutions,government_companies,intermediary_institutions,investment_advisory_companies,investment_funds,none_financial_institution,public_investment_and_holding,rating_institutions,subsidiary_financial_institutions,un_known1")
                .OldAnnotation("Npgsql:Enum:enable_sub_company", "accepted,active,in_active")
                .OldAnnotation("Npgsql:Enum:etf_type", "equity,fixed_income,gold,land_buildings_and_projects,mixed_income,vc_and_private_funds")
                .OldAnnotation("Npgsql:Enum:exchange_type", "ifb,ime,irenex,none,tse")
                .OldAnnotation("Npgsql:Enum:iso_currency", "eur,irr,usd")
                .OldAnnotation("Npgsql:Enum:none_operational_income_tag", "bank_interest_income,other_renewable_income,stock_dividend_income")
                .OldAnnotation("Npgsql:Enum:product_type", "all,bond,certificate_of_deposit,coupon,energy_electricity,equity,etf,forward,fund,futures,gold_coin,ime_certificate,ime_certificate_agriculture,ime_certificate_glass,index,intellectual_property,mbs,option_buy,option_sell,other,vc")
                .OldAnnotation("Npgsql:Enum:publisher_fund_type", "commodity,diversified,equity,fixed_income,market_making,mixed,not_a_fund,project,real_estate,un_known,venture")
                .OldAnnotation("Npgsql:Enum:publisher_market_type", "base,first,none,second,small_and_medium")
                .OldAnnotation("Npgsql:Enum:publisher_state", "not_registered,register_in_ifb,register_in_ime,register_in_irenex,register_in_tse,registered_not_accepted")
                .OldAnnotation("Npgsql:Enum:publisher_sub_company_type", "has_foreign_currency_unit,has_foreign_currency_unit_and_foreign_auditor,liquidation,normal,un_known,un_known1,un_known2,un_known3,un_known4,un_known5,un_known6,un_known7")
                .OldAnnotation("Npgsql:Enum:reporting_type", "agriculture,bank,capital_provision,insurance,investment,leasing,maritime_transportation,production,services,structural,un_known")
                .OldAnnotation("Npgsql:Enum:review_status", "approved,pending,rejected");

            migrationBuilder.Sql("ALTER TABLE manufacturing.raw_monthly_activity_json ALTER COLUMN version TYPE codal_version USING version::codal_version");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:company_type", "article44,associations,basket_companies,brokers,capital_supply_companies,central_asset_management_company,exempt_companies,financial_information_processing_companies,financial_institutions,government_companies,intermediary_institutions,investment_advisory_companies,investment_funds,none_financial_institution,public_investment_and_holding,rating_institutions,subsidiary_financial_institutions,un_known1")
                .Annotation("Npgsql:Enum:enable_sub_company", "accepted,active,in_active")
                .Annotation("Npgsql:Enum:etf_type", "equity,fixed_income,gold,land_buildings_and_projects,mixed_income,vc_and_private_funds")
                .Annotation("Npgsql:Enum:exchange_type", "ifb,ime,irenex,none,tse")
                .Annotation("Npgsql:Enum:iso_currency", "eur,irr,usd")
                .Annotation("Npgsql:Enum:none_operational_income_tag", "bank_interest_income,other_renewable_income,stock_dividend_income")
                .Annotation("Npgsql:Enum:product_type", "all,bond,certificate_of_deposit,coupon,energy_electricity,equity,etf,forward,fund,futures,gold_coin,ime_certificate,ime_certificate_agriculture,ime_certificate_glass,index,intellectual_property,mbs,option_buy,option_sell,other,vc")
                .Annotation("Npgsql:Enum:publisher_fund_type", "commodity,diversified,equity,fixed_income,market_making,mixed,not_a_fund,project,real_estate,un_known,venture")
                .Annotation("Npgsql:Enum:publisher_market_type", "base,first,none,second,small_and_medium")
                .Annotation("Npgsql:Enum:publisher_state", "not_registered,register_in_ifb,register_in_ime,register_in_irenex,register_in_tse,registered_not_accepted")
                .Annotation("Npgsql:Enum:publisher_sub_company_type", "has_foreign_currency_unit,has_foreign_currency_unit_and_foreign_auditor,liquidation,normal,un_known,un_known1,un_known2,un_known3,un_known4,un_known5,un_known6,un_known7")
                .Annotation("Npgsql:Enum:reporting_type", "agriculture,bank,capital_provision,insurance,investment,leasing,maritime_transportation,production,services,structural,un_known")
                .Annotation("Npgsql:Enum:review_status", "approved,pending,rejected")
                .OldAnnotation("Npgsql:Enum:codal_version", "none,v1,v2,v3,v3o1,v4,v5,v7")
                .OldAnnotation("Npgsql:Enum:company_type", "article44,associations,basket_companies,brokers,capital_supply_companies,central_asset_management_company,exempt_companies,financial_information_processing_companies,financial_institutions,government_companies,intermediary_institutions,investment_advisory_companies,investment_funds,none_financial_institution,public_investment_and_holding,rating_institutions,subsidiary_financial_institutions,un_known1")
                .OldAnnotation("Npgsql:Enum:enable_sub_company", "accepted,active,in_active")
                .OldAnnotation("Npgsql:Enum:etf_type", "equity,fixed_income,gold,land_buildings_and_projects,mixed_income,vc_and_private_funds")
                .OldAnnotation("Npgsql:Enum:exchange_type", "ifb,ime,irenex,none,tse")
                .OldAnnotation("Npgsql:Enum:iso_currency", "eur,irr,usd")
                .OldAnnotation("Npgsql:Enum:none_operational_income_tag", "bank_interest_income,other_renewable_income,stock_dividend_income")
                .OldAnnotation("Npgsql:Enum:product_type", "all,bond,certificate_of_deposit,coupon,energy_electricity,equity,etf,forward,fund,futures,gold_coin,ime_certificate,ime_certificate_agriculture,ime_certificate_glass,index,intellectual_property,mbs,option_buy,option_sell,other,vc")
                .OldAnnotation("Npgsql:Enum:publisher_fund_type", "commodity,diversified,equity,fixed_income,market_making,mixed,not_a_fund,project,real_estate,un_known,venture")
                .OldAnnotation("Npgsql:Enum:publisher_market_type", "base,first,none,second,small_and_medium")
                .OldAnnotation("Npgsql:Enum:publisher_state", "not_registered,register_in_ifb,register_in_ime,register_in_irenex,register_in_tse,registered_not_accepted")
                .OldAnnotation("Npgsql:Enum:publisher_sub_company_type", "has_foreign_currency_unit,has_foreign_currency_unit_and_foreign_auditor,liquidation,normal,un_known,un_known1,un_known2,un_known3,un_known4,un_known5,un_known6,un_known7")
                .OldAnnotation("Npgsql:Enum:reporting_type", "agriculture,bank,capital_provision,insurance,investment,leasing,maritime_transportation,production,services,structural,un_known")
                .OldAnnotation("Npgsql:Enum:review_status", "approved,pending,rejected");

            migrationBuilder.Sql("ALTER TABLE manufacturing.raw_monthly_activity_json ALTER COLUMN version TYPE character varying(10) USING version::text");

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
