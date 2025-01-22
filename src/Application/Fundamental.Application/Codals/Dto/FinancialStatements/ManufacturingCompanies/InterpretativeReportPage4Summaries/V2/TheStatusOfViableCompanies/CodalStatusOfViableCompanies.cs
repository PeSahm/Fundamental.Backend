using DNTPersianUtils.Core;
using Newtonsoft.Json;

namespace Fundamental.Application.Codals.Dto.FinancialStatements.ManufacturingCompanies.InterpretativeReportPage4Summaries.V2.
    TheStatusOfViableCompanies;

#pragma warning disable SA1402

public class CodalStatusOfViableCompanies
{
    [JsonProperty("yearData")]
    public List<YearDatum> YearData { get; set; }

    [JsonProperty("rowItems")]
    public List<RowItem> RowItems { get; set; }

    public bool IsValidReport()
    {
        YearDatum? yearDatum = YearData
            .Find(x => x.ColumnId == ColumnId.CurrentOwnershipPercentage);

        if (yearDatum is null)
        {
            return false;
        }

        if (yearDatum.FiscalYear is null || yearDatum.FiscalMonth is null || yearDatum.ReportMonth is null)
        {
            return false;
        }

        return true;
    }

    public void AddCustomRowItems()
    {
        int i = 1;

        foreach (RowItem rowItem in RowItems)
        {
            rowItem.RowNumber = i++;
        }
    }
}

public class RowItem
{
    [JsonProperty("rowCode")]
    public int RowCode { get; set; }

    [JsonProperty("oldFieldName")]
    public string OldFieldName { get; set; }

    [JsonProperty("category")]
    public int Category { get; set; }

    [JsonProperty("rowType")]
    public string RowType { get; set; }

    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("value_2391")]
    public string Value2391 { get; set; }

    [JsonProperty("value_2392")]
    public string Value2392 { get; set; }

    [JsonProperty("value_2393")]
    public string Value2393 { get; set; }

    [JsonProperty("value_2394")]
    public string Value2394 { get; set; }

    [JsonProperty("value_2395")]
    public string Value2395 { get; set; }

    [JsonProperty("value_2396")]
    public string Value2396 { get; set; }

    [JsonProperty("value_2397")]
    public string Value2397 { get; set; }

    [JsonProperty("value_2399")]
    public string Value2399 { get; set; }

    [JsonProperty("value_23910")]
    public string Value23910 { get; set; }

    [JsonProperty("value_2398")]
    public string Value2398 { get; set; }

    [JsonProperty("value_23911")]
    public string Value23911 { get; set; }

    [JsonProperty("value_23912")]
    public string Value23912 { get; set; }

    [JsonProperty("value_23913")]
    public string Value23913 { get; set; }

    [JsonProperty("value_23914")]
    public string Value23914 { get; set; }

    [JsonProperty("value_23915")]
    public string Value23915 { get; set; }

    [JsonProperty("value_23916")]
    public string Value23916 { get; set; }

    [JsonProperty("value_23917")]
    public string Value23917 { get; set; }

    [JsonProperty("value_23918")]
    public string Value23918 { get; set; }

    [JsonProperty("value_23919")]
    public string Value23919 { get; set; }

    [JsonProperty("value_23920")]
    public string Value23920 { get; set; }

    public int RowNumber { get; set; }

    public string? GetDescription()
    {
        if (string.IsNullOrWhiteSpace(Value2391))
        {
            return null;
        }

        return Value2391.NormalizePersianText(
            PersianNormalizers.ApplyPersianYeKe
        );
    }

    public decimal GetValue(ColumnId columnId)
    {
        string propertyName = $"Value{(int)columnId}";
        object? thisValue = Array.Find(GetType().GetProperties(), info => info.Name == propertyName)?.GetValue(this);

        if (thisValue is null)
        {
            return 0;
        }

        return decimal.TryParse(thisValue.ToString(), out decimal res) ? res : 0;
    }
}

public class YearDatum
{
    [JsonProperty("columnId")]
    public ColumnId ColumnId { get; set; }

    [JsonProperty("caption")]
    public string Caption { get; set; }

    [JsonProperty("periodEndToDate")]
    public string PeriodEndToDate { get; set; }

    [JsonProperty("yearEndToDate")]
    public string YearEndToDate { get; set; }

    [JsonProperty("period")]
    public int Period { get; set; }

    [JsonProperty("isAudited")]
    public bool IsAudited { get; set; }

    public DateOnly? YearEndToDateGeorgian => YearEndToDate.ToGregorianDateOnly();
    public DateOnly? PeriodEndToDateGeorgian => PeriodEndToDate.ToGregorianDateOnly();
    public int? FiscalYear => YearEndToDateGeorgian.GetPersianYear(false);
    public int? FiscalMonth => YearEndToDateGeorgian.GetPersianMonth(false);
    public int? ReportYear => PeriodEndToDateGeorgian.GetPersianYear(false);
    public int? ReportMonth => PeriodEndToDateGeorgian.GetPersianMonth(false);
}

public enum ColumnId
{
    /// <summary>
    /// Company Name (نام شرکت)
    /// </summary>
    TickerSymbol = 2391,

    /// <summary>
    /// Last Year Ownership Percentage (پایان سال مالی گذشته - درصد مالکیت)
    /// </summary>
    PreviousYearOwnership = 2392,

    /// <summary>
    /// Last Year Cost (پایان سال مالی گذشته - بهای تمام شده)
    /// </summary>
    PreviousYearCostBasis = 2393,

    /// <summary>
    /// Last Year Financial Year (پایان سال مالی گذشته - سال مالی شرکت سرمایه پذیر)
    /// </summary>
    PreviousFiscalYear = 2394,

    /// <summary>
    /// Last Year Investment Income (پایان سال مالی گذشته - درآمد سرمایه گذاری - میلیون ریال)
    /// </summary>
    PreviousYearInvestmentReturns = 2395,

    /// <summary>
    /// Current Period Ownership Percentage (دوره ی جاری - درصد مالکیت)
    /// </summary>
    CurrentOwnershipPercentage = 2396,

    /// <summary>
    /// Current Period Cost (دوره ی جاری - بهای تمام شده)
    /// </summary>
    CurrentCostBasis = 2397,

    /// <summary>
    /// Current Period Earnings Per Share (دوره ی جاری - درآمد هر سهم)
    /// </summary>
    CurrentEarningsPerShare = 2399,

    /// <summary>
    /// Current Period Cash Earnings Per Share (دوره ی جاری - درآمد نقدی هر سهم)
    /// </summary>
    CurrentCashEarningsPerShare = 23910,

    /// <summary>
    /// Current Period Investment Income (دوره ی جاری - درآمد سرمایه گذاری - میلیون ریال)
    /// </summary>
    CurrentInvestmentReturns = 2398,

    /// <summary>
    /// Estimated Current Year Ownership Percentage (برآورد دوره منتهی به پایان سال مالی جاری - درصد مالکیت)
    /// </summary>
    EstimatedYearEndOwnership = 23911,

    /// <summary>
    /// Estimated Current Year Cost (برآورد دوره منتهی به پایان سال مالی جاری - بهای تمام شده)
    /// </summary>
    EstimatedYearEndCost = 23912,

    /// <summary>
    /// Estimated Current Year Earnings Per Share (برآورد دوره منتهی به پایان سال مالی جاری - درآمد هر سهم)
    /// </summary>
    EstimatedYearEndEarningsPerShare = 23913,

    /// <summary>
    /// Estimated Current Year Cash Earnings Per Share (برآورد دوره منتهی به پایان سال مالی جاری - درآمد نقدی هر سهم)
    /// </summary>
    EstimatedYearEndCashEarnings = 23914,

    /// <summary>
    /// Estimated Current Year Investment Income (برآورد دوره منتهی به پایان سال مالی جاری - درآمد سرمایه گذاری - میلیون ریال)
    /// </summary>
    EstimatedYearEndInvestmentReturns = 23915,

    /// <summary>
    /// Estimated Next Year Ownership Percentage (برآورد پایان سال مالی آینده(فقط برای دوره ی 9 ماهه) - درصد مالکیت)
    /// </summary>
    ForecastNextYearOwnership = 23916,

    /// <summary>
    /// Estimated Next Year Cost (برآورد پایان سال مالی آینده(فقط برای دوره ی 9 ماهه) - بهای تمام شده)
    /// </summary>
    ForecastNextYearCost = 23917,

    /// <summary>
    /// Estimated Next Year Earnings Per Share (برآورد پایان سال مالی آینده(فقط برای دوره ی 9 ماهه) - درآمد هر سهم)
    /// </summary>
    ForecastNextYearEPS = 23918,

    /// <summary>
    /// Estimated Next Year Cash Earnings Per Share (برآورد پایان سال مالی آینده(فقط برای دوره ی 9 ماهه) - درآمد نقدی هر سهم)
    /// </summary>
    ForecastNextYearCashEPS = 23919,

    /// <summary>
    /// Estimated Next Year Investment Income (برآورد پایان سال مالی آینده(فقط برای دوره ی 9 ماهه) - درآمد سرمایه گذاری - میلیون ریال)
    /// </summary>
    ForecastNextYearInvestmentReturns = 23920
}

#pragma warning restore SA1649