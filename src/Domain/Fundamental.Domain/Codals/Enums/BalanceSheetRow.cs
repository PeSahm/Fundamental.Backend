using System.ComponentModel;
using Fundamental.Domain.Attributes;

namespace Fundamental.Domain.Codals.Enums;

public static class BalanceSheetRow
{
    [Description("سایر دارایی ها")]
    [BalanceSheetCategory(BalanceSheetCategory.Assets)]
    public static ushort OtherAssets => 18;

    [Description("جمع دارایی های غیر جاری")]
    [BalanceSheetCategory(BalanceSheetCategory.Assets)]
    public static ushort TotalNonCurrentAssets => 19;

    [Description("دارایی های جاری")]
    [BalanceSheetCategory(BalanceSheetCategory.Assets)]

    public static ushort CurrentAssets => 3;

    [Description("سفارشات و پیش پرداخت ها")]
    [BalanceSheetCategory(BalanceSheetCategory.Assets)]

    public static ushort OrdersAndPrepayments => 9;

    [Description("موجودی مواد و کالا")]
    [BalanceSheetCategory(BalanceSheetCategory.Assets)]

    public static ushort Inventory => 8;

    [Description("دریافتنی های تجاری و سایر دریافتنی ها")]
    [BalanceSheetCategory(BalanceSheetCategory.Assets)]

    public static ushort TradeAndOtherReceivables => 44;

    [Description("سرمایه گذاری های کوتاه مدت")]
    [BalanceSheetCategory(BalanceSheetCategory.Assets)]

    public static ushort ShortTermInvestments => 5;

    [Description("موجودی نقد")]
    [BalanceSheetCategory(BalanceSheetCategory.Assets)]

    public static ushort Cash => 4;

    [Description("دارایی های نگه داری شده برای فروش")]
    [BalanceSheetCategory(BalanceSheetCategory.Assets)]

    public static ushort AssetsHeldForSale => 10;

    [Description("جمع دارایی های جاری")]
    [BalanceSheetCategory(BalanceSheetCategory.Assets)]

    public static ushort TotalCurrentAssets => 11;

    [Description("جمع دارایی ها")]
    [BalanceSheetCategory(BalanceSheetCategory.Assets)]

    public static ushort TotalAssets => 33;

    [Description("حقوق مالکانه و بدهی ها")]
    [BalanceSheetCategory(BalanceSheetCategory.Liability)]

    public static ushort EquityAndLiabilities => 35;

    [Description("حقوق مالکانه")]
    [BalanceSheetCategory(BalanceSheetCategory.Liability)]

    public static ushort Equity => 36;

    [Description("سرمايه")]
    [BalanceSheetCategory(BalanceSheetCategory.Liability)]

    public static ushort Capital => 21;

    [Description("افزایش سرمایه در جریان")]
    [BalanceSheetCategory(BalanceSheetCategory.Liability)]

    public static ushort CapitalIncreaseInProgress => 22;

    [Description("صرف سهام")]
    [BalanceSheetCategory(BalanceSheetCategory.Liability)]

    public static ushort SharePremium => 23;

    [Description("صرف سهام خزانه")]
    [BalanceSheetCategory(BalanceSheetCategory.Liability)]

    public static ushort TreasurySharePremium => 37;

    [Description("اندوخته قانونی")]
    [BalanceSheetCategory(BalanceSheetCategory.Liability)]

    public static ushort LegalReserve => 25;

    [Description("سایر اندوخته ها")]
    [BalanceSheetCategory(BalanceSheetCategory.Liability)]

    public static ushort OtherReserves => 26;

    [Description("دارایی ها")]
    [BalanceSheetCategory(BalanceSheetCategory.Assets)]

    public static ushort Assets => 2;

    [Description("مازاد تجدید ارزیابی دارایی ها")]
    [BalanceSheetCategory(BalanceSheetCategory.Liability)]

    public static ushort RevaluationSurplus => 28;

    [Description("تفاوت تسعیر ارز عملیات خارجی")]
    [BalanceSheetCategory(BalanceSheetCategory.Liability)]

    public static ushort ForeignOperationExchangeDifferences => 38;

    [Description("سود (زيان) انباشته")]
    [BalanceSheetCategory(BalanceSheetCategory.Liability)]

    public static ushort AccumulatedProfitLoss => 31;

    [Description("سهام خزانه")]
    [BalanceSheetCategory(BalanceSheetCategory.Liability)]

    public static ushort TreasuryShares => 24;

    [Description("جمع حقوق مالکانه")]
    [BalanceSheetCategory(BalanceSheetCategory.Liability)]

    public static ushort TotalEquity => 39;

    [Description("بدهی ها")]
    [BalanceSheetCategory(BalanceSheetCategory.Liability)]

    public static ushort Liabilities => 40;

    [Description("بدهی های غیر جاری")]
    [BalanceSheetCategory(BalanceSheetCategory.Liability)]

    public static ushort NonCurrentLiabilities => 13;

    [Description("پرداختنی های بلند مدت")]
    [BalanceSheetCategory(BalanceSheetCategory.Liability)]

    public static ushort LongTermPayables => 14;

    [Description("تسهیلات مالی بلندمدت")]
    [BalanceSheetCategory(BalanceSheetCategory.Liability)]

    public static ushort LongTermFinancialFacilities => 16;

    [Description("ذخیره مزایای پایان خدمت کارکنان")]
    [BalanceSheetCategory(BalanceSheetCategory.Liability)]

    public static ushort EmployeeEndOfServiceBenefits => 17;

    [Description("دارایی های غیر جاری")]
    [BalanceSheetCategory(BalanceSheetCategory.Assets)]

    public static ushort NonCurrentAssets => 12;

    [Description("جمع بدهی های غیر جاری")]
    [BalanceSheetCategory(BalanceSheetCategory.Liability)]

    public static ushort TotalNonCurrentLiabilities => 18;

    [Description("بدهی های جاری")]
    [BalanceSheetCategory(BalanceSheetCategory.Liability)]

    public static ushort CurrentLiabilities => 3;

    [Description("پرداختنی های تجاری و سایر پرداختنی ها")]
    [BalanceSheetCategory(BalanceSheetCategory.Liability)]

    public static ushort TradeAndOtherPayables => 41;

    [Description("مالیات پرداختنی")]
    [BalanceSheetCategory(BalanceSheetCategory.Liability)]

    public static ushort TaxesPayable => 6;

    [Description("سود سهام پرداختنی")]
    [BalanceSheetCategory(BalanceSheetCategory.Liability)]

    public static ushort DividendsPayable => 7;

    [Description("تسهیلات مالی")]
    [BalanceSheetCategory(BalanceSheetCategory.Liability)]

    public static ushort FinancialFacilities => 8;

    [Description("ذخایر")]
    [BalanceSheetCategory(BalanceSheetCategory.Liability)]

    public static ushort Provisions => 9;

    [Description("پیش دریافت ها")]
    [BalanceSheetCategory(BalanceSheetCategory.Liability)]

    public static ushort AdvancesReceived => 10;

    [Description("بدهی های مربوط به دارایی های نگه داری شده برای فروش")]
    [BalanceSheetCategory(BalanceSheetCategory.Liability)]

    public static ushort LiabilitiesRelatedToAssetsHeldForSale => 11;

    [Description("دارایی های ثابت مشهود")]
    [BalanceSheetCategory(BalanceSheetCategory.Assets)]

    public static ushort TangibleFixedAssets => 17;

    [Description("جمع بدهی های جاری")]
    [BalanceSheetCategory(BalanceSheetCategory.Liability)]

    public static ushort TotalCurrentLiabilities => 12;

    [Description("جمع بدهی ها")]
    [BalanceSheetCategory(BalanceSheetCategory.Liability)]

    public static ushort TotalLiabilities => 19;

    [Description("جمع حقوق مالکانه و بدهی ها")]
    [BalanceSheetCategory(BalanceSheetCategory.Liability)]

    public static ushort TotalEquityAndLiabilities => 43;

    [Description("سرمایه گذاری در املاک")]
    [BalanceSheetCategory(BalanceSheetCategory.Assets)]

    public static ushort InvestmentInProperties => 15;

    [Description("دارایی های نامشهود")]
    [BalanceSheetCategory(BalanceSheetCategory.Assets)]

    public static ushort IntangibleAssets => 16;

    [Description("سرمایه گذاری های بلند مدت")]
    [BalanceSheetCategory(BalanceSheetCategory.Assets)]

    public static ushort LongTermInvestments => 14;

    [Description("دریافتنی های بلند مدت")]
    [BalanceSheetCategory(BalanceSheetCategory.Assets)]

    public static ushort LongTermNotesAndAccountsReceivable => 13;

    [Description("دارایی مالیات انتقالی")]
    [BalanceSheetCategory(BalanceSheetCategory.Assets)]
    public static ushort DeferredTaxAsset => 45;

    [Description("بدهی مالیات انتفالی")]
    [BalanceSheetCategory(BalanceSheetCategory.Liability)]
    public static ushort DeferredTaxLiability => 46;
}