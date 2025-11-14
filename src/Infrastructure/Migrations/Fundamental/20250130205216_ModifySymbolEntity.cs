#nullable disable

using Fundamental.Domain.Symbols.Enums;
using Fundamental.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Fundamental.Migrations.Fundamental;

/// <inheritdoc />
[DbContext(typeof(FundamentalDbContext))]
[Migration("20250130205216_ModifySymbolEntity")]
public class ModifySymbolEntity : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterDatabase()
            .Annotation("Npgsql:Enum:company_type",
                "none_financial_institution,public_investment_and_holding,financial_institutions,subsidiary_financial_institutions,intermediary_institutions,investment_funds,basket_companies,investment_advisory_companies,financial_information_processing_companies,capital_supply_companies,associations,central_asset_management_company,rating_institutions,article44,brokers,government_companies,exempt_companies,un_known1")
            .Annotation("Npgsql:Enum:enable_sub_company", "in_active,active,accepted")
            .Annotation("Npgsql:Enum:etf_type", "fixed_income,mixed_income,equity,land_buildings_and_projects,gold,vc_and_private_funds")
            .Annotation("Npgsql:Enum:exchange_type", "tse,ifb,irenex,ime,none")
            .Annotation("Npgsql:Enum:iso_currency", "irr,usd,eur")
            .Annotation("Npgsql:Enum:product_type",
                "equity,fund,bond,option_sell,index,forward,etf,vc,futures,certificate_of_deposit,coupon,mbs,gold_coin,option_buy,energy_electricity,intellectual_property,ime_certificate,ime_certificate_agriculture,ime_certificate_glass,other,all")
            .Annotation("Npgsql:Enum:publisher_fund_type",
                "un_known,not_a_fund,real_estate,fixed_income,mixed,equity,project,venture,market_making,commodity,diversified")
            .Annotation("Npgsql:Enum:publisher_market_type", "none,first,second,base,small_and_medium")
            .Annotation("Npgsql:Enum:publisher_state",
                "register_in_ime,register_in_irenex,register_in_tse,register_in_ifb,registered_not_accepted,not_registered")
            .Annotation("Npgsql:Enum:publisher_sub_company_type",
                "un_known,normal,liquidation,has_foreign_currency_unit,has_foreign_currency_unit_and_foreign_auditor,un_known1,un_known2,un_known3,un_known4,un_known5,un_known6")
            .Annotation("Npgsql:Enum:reporting_type",
                "production,structural,investment,bank,leasing,services,insurance,maritime_transportation,agriculture,capital_provision,un_known")
            .Annotation("Npgsql:Enum:review_status", "pending,rejected,approved")
            .OldAnnotation("Npgsql:Enum:company_type",
                "none_financial_institution,public_investment_and_holding,financial_institutions,subsidiary_financial_institutions,intermediary_institutions,investment_funds,basket_companies,investment_advisory_companies,financial_information_processing_companies,capital_supply_companies,associations,central_asset_management_company,rating_institutions,article44,brokers,government_companies,exempt_companies,un_known1")
            .OldAnnotation("Npgsql:Enum:enable_sub_company", "in_active,active,accepted")
            .OldAnnotation("Npgsql:Enum:iso_currency", "irr,usd,eur")
            .OldAnnotation("Npgsql:Enum:publisher_fund_type",
                "un_known,not_a_fund,real_estate,fixed_income,mixed,equity,project,venture,market_making,commodity,diversified")
            .OldAnnotation("Npgsql:Enum:publisher_market_type", "none,first,second,base,small_and_medium")
            .OldAnnotation("Npgsql:Enum:publisher_state",
                "register_in_ime,register_in_irenex,register_in_tse,register_in_ifb,registered_not_accepted,not_registered")
            .OldAnnotation("Npgsql:Enum:publisher_sub_company_type",
                "un_known,normal,liquidation,has_foreign_currency_unit,has_foreign_currency_unit_and_foreign_auditor,un_known1,un_known2,un_known3,un_known4,un_known5,un_known6")
            .OldAnnotation("Npgsql:Enum:reporting_type",
                "production,structural,investment,bank,leasing,services,insurance,maritime_transportation,agriculture,capital_provision,un_known")
            .OldAnnotation("Npgsql:Enum:review_status", "pending,rejected,approved");

        migrationBuilder.AddColumn<EtfType>(
            "etf_type",
            schema: "shd",
            table: "symbol",
            type: "etf_type",
            nullable: true);

        migrationBuilder.AddColumn<ExchangeType>(
            "exchange_type",
            schema: "shd",
            table: "symbol",
            type: "exchange_type",
            nullable: false,
            defaultValue: ExchangeType.None);

        migrationBuilder.AddColumn<ProductType>(
            "product_type2",
            schema: "shd",
            table: "symbol",
            type: "product_type",
            nullable: false,
            defaultValue: ProductType.All);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            "etf_type",
            schema: "shd",
            table: "symbol");

        migrationBuilder.DropColumn(
            "exchange_type",
            schema: "shd",
            table: "symbol");

        migrationBuilder.DropColumn(
            "product_type2",
            schema: "shd",
            table: "symbol");

        migrationBuilder.AlterDatabase()
            .Annotation("Npgsql:Enum:company_type",
                "none_financial_institution,public_investment_and_holding,financial_institutions,subsidiary_financial_institutions,intermediary_institutions,investment_funds,basket_companies,investment_advisory_companies,financial_information_processing_companies,capital_supply_companies,associations,central_asset_management_company,rating_institutions,article44,brokers,government_companies,exempt_companies,un_known1")
            .Annotation("Npgsql:Enum:enable_sub_company", "in_active,active,accepted")
            .Annotation("Npgsql:Enum:iso_currency", "irr,usd,eur")
            .Annotation("Npgsql:Enum:publisher_fund_type",
                "un_known,not_a_fund,real_estate,fixed_income,mixed,equity,project,venture,market_making,commodity,diversified")
            .Annotation("Npgsql:Enum:publisher_market_type", "none,first,second,base,small_and_medium")
            .Annotation("Npgsql:Enum:publisher_state",
                "register_in_ime,register_in_irenex,register_in_tse,register_in_ifb,registered_not_accepted,not_registered")
            .Annotation("Npgsql:Enum:publisher_sub_company_type",
                "un_known,normal,liquidation,has_foreign_currency_unit,has_foreign_currency_unit_and_foreign_auditor,un_known1,un_known2,un_known3,un_known4,un_known5,un_known6")
            .Annotation("Npgsql:Enum:reporting_type",
                "production,structural,investment,bank,leasing,services,insurance,maritime_transportation,agriculture,capital_provision,un_known")
            .Annotation("Npgsql:Enum:review_status", "pending,rejected,approved")
            .OldAnnotation("Npgsql:Enum:company_type",
                "none_financial_institution,public_investment_and_holding,financial_institutions,subsidiary_financial_institutions,intermediary_institutions,investment_funds,basket_companies,investment_advisory_companies,financial_information_processing_companies,capital_supply_companies,associations,central_asset_management_company,rating_institutions,article44,brokers,government_companies,exempt_companies,un_known1")
            .OldAnnotation("Npgsql:Enum:enable_sub_company", "in_active,active,accepted")
            .OldAnnotation("Npgsql:Enum:etf_type", "fixed_income,mixed_income,equity,land_buildings_and_projects,gold,vc_and_private_funds")
            .OldAnnotation("Npgsql:Enum:exchange_type", "tse,ifb,irenex,ime,none")
            .OldAnnotation("Npgsql:Enum:iso_currency", "irr,usd,eur")
            .OldAnnotation("Npgsql:Enum:product_type",
                "equity,fund,bond,option_sell,index,forward,etf,vc,futures,certificate_of_deposit,coupon,mbs,gold_coin,option_buy,energy_electricity,intellectual_property,ime_certificate,ime_certificate_agriculture,ime_certificate_glass,other,all")
            .OldAnnotation("Npgsql:Enum:publisher_fund_type",
                "un_known,not_a_fund,real_estate,fixed_income,mixed,equity,project,venture,market_making,commodity,diversified")
            .OldAnnotation("Npgsql:Enum:publisher_market_type", "none,first,second,base,small_and_medium")
            .OldAnnotation("Npgsql:Enum:publisher_state",
                "register_in_ime,register_in_irenex,register_in_tse,register_in_ifb,registered_not_accepted,not_registered")
            .OldAnnotation("Npgsql:Enum:publisher_sub_company_type",
                "un_known,normal,liquidation,has_foreign_currency_unit,has_foreign_currency_unit_and_foreign_auditor,un_known1,un_known2,un_known3,un_known4,un_known5,un_known6")
            .OldAnnotation("Npgsql:Enum:reporting_type",
                "production,structural,investment,bank,leasing,services,insurance,maritime_transportation,agriculture,capital_provision,un_known")
            .OldAnnotation("Npgsql:Enum:review_status", "pending,rejected,approved");
    }
}