using System.ComponentModel;

namespace Fundamental.Application.Codals.Dto.FinancialStatements.ManufacturingCompanies.BalanceSheets.V5;

public enum RowCode
{
    [Description("دارایی ثابت مشهود")]
    PlantAssetsNetOfAccumulatedDepreciation = 17,

    [Description("سرمایه گذاری در املاک")]
    InvestmentProperty = 15,

    [Description("دارایی های نامشهود")]
    IntangibleAssets = 16,

    [Description("سرمایه گذاری های بلند مدت")]
    LongTermInvestments = 14,

    [Description("دریافتنی های بلند مدت")]
    LongTermNotesAndAccountsReceivable = 13,

    [Description("دارایی مالیات انتقالی")]
    DeferredTaxAsset = 45, // Translated from "دارایی مالیات انتقالی"

    [Description("سایر دارایی ها")]
    OtherAssets = 18,

    [Description("جمع دارایی های غیر جاری")]
    TotalNonCurrentAssets = 19,

    [Description("سفارشات و پیش پرداخت ها")]
    Prepayments = 9,

    [Description("موجودی مواد و کالا")]
    Inventories = 8,

    [Description("دریافتنی های تجاری و سایر دریافتنی ها")]
    TradeAndOtherReceivables = 44, // Translated from "دریافتنی‌های تجاری و سایر دریافتنی‌ها"

    [Description("سرمایه گذاری های کوتاه مدت")]
    ShortTermInvestments = 5,

    [Description("موجودی نقد")]
    CashAndCashEquivalents = 4,

    [Description("دارایی های نگهداری شده برای فروش")]
    NonCurrentAssetsForSale = 10,

    [Description("جمع دارایی های جاری")]
    TotalCurrentAssets = 11,

    [Description("جمع دارایی ها")]
    TotalAssets = 33,

    [Description("سرمایه")]
    CommonStock = 21,

    [Description("افزایش سرمایه در جریان")]
    InprocessCapitalIncrease = 22,

    [Description("صرف سهام")]
    StockConsume = 23,

    [Description("صرف سهام خزانه")]
    TreasuryStockExpense = 37, // Translated from "صرف سهام خزانه"

    [Description("اندوخته قانونی")]
    LegalReserve = 25,

    [Description("سایر اندوخته ها")]
    ExpansionReserve = 26,

    [Description("مازاد تجدید ارزیابی دارایی ها")]
    RevaluationSurplus = 28,

    [Description("تفاوت تسعیر ارز عملیات خارجی")]
    ForeignOperationCurrencyTranslationDifference = 38, // Translated from "تفاوت تسعیر ارز عملیات خارجی"

    [Description("سود(زیان) انباشه")]
    RetainedEarnings = 31,

    [Description("سهام خزانه")]
    TreasuryShares = 24,

    [Description("جمع حقوق مالکانه")]
    TotalEquity = 39, // Translated from "جمع حقوق مالکانه"

    [Description("بدهی مالیات انتفالی")]
    DeferredTaxLiability = 46, // Translated from "بدهی مالیات انتقالی"

    [Description("پرداختی های تجاری و سایر پرداختنی ها")]
    TradeAndOtherPayables = 41, // Translated from "پرداختنی‌های تجاری و سایر پرداختنی‌ها"

    [Description("مالیات پرداختنی")]
    CurrentTaxLiabilities = 6,

    [Description("سود سهام پرداختنی")]
    DividendsPayable = 7,

    [Description("جمع بدهی های جاری")]
    TotalCurrentLiabilities = 12,

    [Description("جمع حقوق مالکانه و بدهی ها")]
    TotalEquityAndLiabilities = 43 // Translated from "جمع حقوق مالکانه و بدهی‌ها"
}