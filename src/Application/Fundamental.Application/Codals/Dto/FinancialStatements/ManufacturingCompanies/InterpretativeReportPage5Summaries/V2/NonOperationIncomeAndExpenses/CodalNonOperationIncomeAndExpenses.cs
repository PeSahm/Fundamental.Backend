using DNTPersianUtils.Core;
using Newtonsoft.Json;

namespace Fundamental.Application.Codals.Dto.FinancialStatements.ManufacturingCompanies.InterpretativeReportPage5Summaries.V2.
    NonOperationIncomeAndExpenses;

#pragma warning disable SA1402

public class CodalNonOperationIncomeAndExpenses
{
    [JsonProperty("yearData")]
    public List<YearDatum> YearData { get; set; }

    [JsonProperty("rowItems")]
    public List<RowItem> RowItems { get; set; }

    public bool IsValidReport()
    {
        YearDatum? yearDatum = YearData
            .Find(x => x.ColumnId == ColumnId.CurrentPeriod);

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

    [JsonProperty("value_2451")]
    public string Value2451 { get; set; }

    [JsonProperty("value_2452")]
    public string Value2452 { get; set; }

    [JsonProperty("value_2453")]
    public string Value2453 { get; set; }

    [JsonProperty("value_2454")]
    public string Value2454 { get; set; }

    public int RowNumber { get; set; }

    public string? GetDescription()
    {
        if (string.IsNullOrWhiteSpace(Value2451))
        {
            return null;
        }

        return Value2451.NormalizePersianText(
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

    public DateOnly? YearEndToDateGeorgian => ColumnId == ColumnId.YearlyPredicatePeriod
        ? PeriodEndToDate.ToGregorianDateOnly()
        : YearEndToDate.ToGregorianDateOnly();

    public DateOnly? PeriodEndToDateGeorgian => PeriodEndToDate.ToGregorianDateOnly();
    public int? FiscalYear => YearEndToDateGeorgian.GetPersianYear(false);

    public int? FiscalMonth => YearEndToDateGeorgian.GetPersianMonth(false);

    public int? ReportYear => PeriodEndToDateGeorgian.GetPersianYear(false);

    public int? ReportMonth => PeriodEndToDateGeorgian.GetPersianMonth(false);
}

public enum ColumnId
{
    CurrentPeriod = 2453,
    LastAnnualPeriod = 2452,
    PredictedPeriod = 2454,
    YearlyPredicatePeriod = 2455
}

#pragma warning restore SA1649