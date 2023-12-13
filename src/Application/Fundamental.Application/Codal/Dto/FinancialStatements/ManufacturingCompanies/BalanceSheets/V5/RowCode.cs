namespace Fundamental.Application.Codal.Dto.FinancialStatements.ManufacturingCompanies.BalanceSheets.V5;

public enum RowCode
{
    PlantAssetsNetOfAccumulatedDepreciation = 17,
    InvestmentProperty = 15,
    IntangibleAssets = 16,
    LongTermInvestments = 14,
    LongTermNotesAndAccountsReceivable = 13,
    DeferredTaxAsset = 45, // Translated from "دارایی مالیات انتقالی"
    OtherAssets = 18,
    TotalNonCurrentAssets = 19,
    Prepayments = 9,
    Inventories = 8,
    TradeAndOtherReceivables = 44, // Translated from "دریافتنی‌های تجاری و سایر دریافتنی‌ها"
    ShortTermInvestments = 5,
    CashAndCashEquivalents = 4,
    NonCurrentAssetsForSale = 10,
    TotalCurrentAssets = 11,
    TotalAssets = 33,
    CommonStock = 21,
    InprocessCapitalIncrease = 22,
    StockConsume = 23,
    TreasuryStockExpense = 37, // Translated from "صرف سهام خزانه"
    LegalReserve = 25,
    ExpansionReserve = 26,
    RevaluationSurplus = 28,
    ForeignOperationCurrencyTranslationDifference = 38, // Translated from "تفاوت تسعیر ارز عملیات خارجی"
    RetainedEarnings = 31,
    TreasuryShares = 24,
    TotalEquity = 39, // Translated from "جمع حقوق مالکانه"
    DeferredTaxLiability = 46, // Translated from "بدهی مالیات انتقالی"
    TradeAndOtherPayables = 41, // Translated from "پرداختنی‌های تجاری و سایر پرداختنی‌ها"
    CurrentTaxLiabilities = 6,
    DividendsPayable = 7,
    TotalCurrentLiabilities = 12,
    TotalEquityAndLiabilities = 43 // Translated from "جمع حقوق مالکانه و بدهی‌ها"
}