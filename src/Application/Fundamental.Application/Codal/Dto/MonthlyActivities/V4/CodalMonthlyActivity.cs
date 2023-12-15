using DNTPersianUtils.Core;
using Fundamental.Application.Codal.Dto.MonthlyActivities.V4.Enums;
using Newtonsoft.Json;

namespace Fundamental.Application.Codal.Dto.MonthlyActivities.V4;

#pragma warning disable SA1402

public class CodalMonthlyActivity
{
    [JsonProperty("monthlyActivity")]
    public MonthlyActivityDto MonthlyActivity { get; set; }

    [JsonProperty("listedCapital")]
    public decimal ListedCapital { get; set; }

    [JsonProperty("unauthorizedCapital")]
    public decimal UnauthorizedCapital { get; set; }
}

public class MonthlyActivityDto
{
    [JsonProperty("version")]
    public string Version { get; set; }

    [JsonProperty("productionAndSales")]
    public ProductionAndSales ProductionAndSales { get; set; }

    [JsonProperty("productMonthlyActivityDesc1")]
    public ProductMonthlyActivityDesc1 ProductMonthlyActivityDesc1 { get; set; }
}

public class ProductionAndSales
{
    [JsonProperty("yearData")]
    public List<YearDatum> YearData { get; set; } = new();

    [JsonProperty("rowItems")]
    public List<RowItem> RowItems { get; set; } = new();
}

public class ProductMonthlyActivityDesc1
{
    [JsonProperty("rowItems")]
    public List<RowItem> RowItems { get; set; } = new();
}

public class RowItem
{
    [JsonProperty("rowCode")]
    public RowCode? RowCode { get; set; }

    [JsonProperty("oldFieldName")]
    public string OldFieldName { get; set; }

    [JsonProperty("category")]
    public Category? Category { get; set; }

    [JsonProperty("rowType")]
    public string RowType { get; set; }

    [JsonProperty("productId")]
    public string ProductId { get; set; }

    [JsonProperty("value_11971")]
    public string Value11971 { get; set; }

    [JsonProperty("unitId")]
    public decimal UnitId { get; set; }

    [JsonProperty("value_11972")]
    public string Value11972 { get; set; }

    [JsonProperty("value_11973")]
    public decimal Value11973 { get; set; }

    [JsonProperty("value_11974")]
    public decimal Value11974 { get; set; }

    [JsonProperty("value_11975")]
    public decimal Value11975 { get; set; }

    [JsonProperty("value_11976")]
    public decimal Value11976 { get; set; }

    // [JsonProperty("value_11977")]
    // public decimal Value11977 { get; set; }
    //
    // [JsonProperty("value_11978")]
    // public decimal Value11978 { get; set; }
    //
    // [JsonProperty("value_11979")]
    // public decimal Value11979 { get; set; }

    [JsonProperty("value_119710")]
    public decimal Value119710 { get; set; }

    [JsonProperty("value_119711")]
    public decimal Value119711 { get; set; }

    [JsonProperty("value_119712")]
    public decimal Value119712 { get; set; }

    [JsonProperty("value_119713")]
    public decimal Value119713 { get; set; }

    [JsonProperty("value_119714")]
    public decimal Value119714 { get; set; }

    [JsonProperty("value_119715")]
    public decimal Value119715 { get; set; }

    [JsonProperty("value_119716")]
    public decimal Value119716 { get; set; }

    [JsonProperty("value_119717")]
    public decimal Value119717 { get; set; }

    [JsonProperty("value_119718")]
    public decimal Value119718 { get; set; }

    [JsonProperty("value_119719")]
    public decimal Value119719 { get; set; }

    [JsonProperty("value_119720")]
    public decimal Value119720 { get; set; }

    [JsonProperty("value_119721")]
    public decimal Value119721 { get; set; }

    [JsonProperty("value_119722")]
    public decimal Value119722 { get; set; }

    [JsonProperty("value_119723")]
    public decimal Value119723 { get; set; }

    [JsonProperty("value_119724")]
    public decimal Value119724 { get; set; }

    [JsonProperty("value_119725")]
    public decimal Value119725 { get; set; }

    [JsonProperty("value_119726")]
    public string Value119726 { get; set; }

    [JsonProperty("value_11991")]
    public string Value11991 { get; set; }

    public decimal GetValue(SaleColumnId columnId)
    {
        string propertyName = $"Value{(int)columnId}";
        object? thisValue = GetType().GetProperties().FirstOrDefault(x => x.Name == propertyName)?.GetValue(this);

        if (thisValue is null)
        {
            return 0;
        }

        return decimal.Parse(thisValue.ToString() ?? "0");
    }
}

public class YearDatum
{
    [JsonProperty("columnId")]
    public SaleColumnId ColumnId { get; set; }

    [JsonProperty("caption")]
    public string Caption { get; set; }

    [JsonProperty("periodEndToDate")]
    public string PeriodEndToDate { get; set; }

    [JsonProperty("yearEndToDate")]
    public string YearEndToDate { get; set; }

    public DateOnly? YearEndToDateGeorgian => YearEndToDate.ToGregorianDateOnly();
    public DateOnly? PeriodEndToDateGeorgian => PeriodEndToDate.ToGregorianDateOnly();

    public int? FiscalYear => YearEndToDateGeorgian.GetPersianYear(false);

    public int? FiscalMonth => YearEndToDateGeorgian.GetPersianMonth(false);

    public int? ReportYear => PeriodEndToDateGeorgian.GetPersianYear(false);

    public int? ReportMonth => PeriodEndToDateGeorgian.GetPersianMonth(false);

    [JsonProperty("period")]
    public int? Period { get; set; }

    [JsonProperty("isAudited")]
    public object IsAudited { get; set; }
}
#pragma warning restore SA1649