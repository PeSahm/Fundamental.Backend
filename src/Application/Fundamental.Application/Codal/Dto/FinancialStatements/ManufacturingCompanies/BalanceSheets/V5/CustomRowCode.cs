using System.ComponentModel;

namespace Fundamental.Application.Codal.Dto.FinancialStatements.ManufacturingCompanies.BalanceSheets.V5;

public enum CustomRowCode
{
    [Description("دارایی ثابت مشهود")]
    PlantAssetsNetOfAccumulatedDepreciation = 1,

    [Description("سرمایه گذاری در املاک")]
    InvestmentProperty = 2,

    [Description("دارایی های نامشهود")]
    IntangibleAssets = 3,

    [Description("سرمایه گذاری های بلند مدت")]
    LongTermInvestments = 4,

    [Description("دریافتنی های بلند مدت")]
    LongTermNotesAndAccountsReceivable = 5,

    [Description("دارایی مالیات انتقالی")]
    DeferredTaxAsset = 6,

    [Description("سایر دارایی ها")]
    OtherAssets = 7,

    [Description("جمع دارایی های غیر جاری")]
    TotalNonCurrentAssets = 8,

    [Description("سفارشات و پیش پرداخت ها")]
    Prepayments = 9,

    [Description("موجودی مواد و کالا")]
    Inventories = 10,

    [Description("دریافتنی های تجاری و سایر دریافتنی ها")]
    TradeAndOtherReceivables = 11,

    [Description("سرمایه گذاری های کوتاه مدت")]
    ShortTermInvestments = 12,

    [Description("موجودی نقد")]
    CashAndCashEquivalents = 13,

    [Description("دارایی های نگهداری شده برای فروش")]
    NonCurrentAssetsForSale = 14,

    [Description("جمع دارایی های جاری")]
    TotalCurrentAssets = 15,

    [Description("جمع دارایی ها")]
    TotalAssets = 16,

    [Description("سرمایه")]
    CommonStock = 17,

    [Description("افزایش سرمایه در جریان")]
    InprocessCapitalIncrease = 18,

    [Description("صرف سهام")]
    StockConsume = 19,

    [Description("صرف سهام خزانه")]
    TreasuryStockExpense = 20,

    [Description("اندوخته قانونی")]
    LegalReserve = 21,

    [Description("سایر اندوخته ها")]
    ExpansionReserve = 22,

    [Description("مازاد تجدید ارزیابی دارایی ها")]
    RevaluationSurplus = 23,

    [Description("تفاوت تسعیر ارز عملیات خارجی")]
    ForeignOperationCurrencyTranslationDifference = 24,

    [Description("سود(زیان) انباشه")]
    RetainedEarnings = 25,

    [Description("سهام خزانه")]
    TreasuryShares = 26,

    [Description("جمع حقوق مالکانه")]
    TotalEquity = 27,

    [Description("بدهی مالیات انتفالی")]
    DeferredTaxLiability = 28,

    [Description("پرداختی های تجاری و سایر پرداختنی ها")]
    TradeAndOtherPayables = 29,

    [Description("مالیات پرداختنی")]
    CurrentTaxLiabilities = 30,

    [Description("سود سهام پرداختنی")]
    DividendsPayable = 31,

    [Description("تسهیلات مالی")]
    LoanPayableCurrentPotion = 32,

    [Description("ذخایر")]
    Provisions = 33,

    [Description("پیش دریافت ها")]
    DeferredRevenue = 34,

    [Description("بدهی های مرتبط با دارایی های نگه داری شده برای فروش")]
    LiabilitiesDisposalGroupsForSale = 35,

    [Description("جمع بدهی های جاری")]
    TotalCurrentLiabilities = 36,

    [Description("جمع بدهی ها")]
    TotalLiabilities = 37,

    [Description("جمع حقوق مالکانه و بدهی ها")]
    TotalEquityAndLiabilities = 38
}