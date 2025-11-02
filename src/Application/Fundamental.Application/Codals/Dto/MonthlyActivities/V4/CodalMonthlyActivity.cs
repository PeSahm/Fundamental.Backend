using Fundamental.Application.Codals.Enums;
using DNTPersianUtils.Core;
using Fundamental.Application.Codals.Dto.MonthlyActivities.V4.Enums;
using Fundamental.Domain.Codals.Manufacturing.Enums;
using Fundamental.Application.Codals.Services;
using Fundamental.Domain.Common.Enums;
using Newtonsoft.Json;

namespace Fundamental.Application.Codals.Dto.MonthlyActivities.V4;

#pragma warning disable SA1402

public class CodalMonthlyActivity : ICodalMappingServiceMetadata
{
    [JsonProperty("monthlyActivity")]
    public MonthlyActivityDto? MonthlyActivity { get; set; }

    [JsonProperty("listedCapital")]
    public decimal ListedCapital { get; set; }

    [JsonProperty("unauthorizedCapital")]
    public decimal UnauthorizedCapital { get; set; }

    public ReportingType ReportingType => ReportingType.Production;
    public LetterType LetterType => LetterType.MonthlyActivity;
    public CodalVersion CodalVersion => CodalVersion.V4;
    public LetterPart LetterPart => LetterPart.NotSpecified;
}

public class MonthlyActivityDto
{
    [JsonProperty("version")]
    public string Version { get; set; }

    [JsonProperty("productionAndSales")]
    public ProductionAndSales? ProductionAndSales { get; set; }

    [JsonProperty("buyRawMaterial")]
    public BuyRawMaterial? BuyRawMaterial { get; set; }

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

public class BuyRawMaterial
{
    [JsonProperty("yearData")]
    public List<YearDatum> YearData { get; set; } = new();

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

    // Buy Raw Material properties for V4
    [JsonProperty("value_34641")]
    public string Value34641 { get; set; }

    [JsonProperty("value_34642")]
    public string Value34642 { get; set; }

    [JsonProperty("value_34643")]
    public decimal Value34643 { get; set; }

    [JsonProperty("value_34644")]
    public decimal Value34644 { get; set; }

    [JsonProperty("value_34645")]
    public decimal Value34645 { get; set; }

    [JsonProperty("value_34649")]
    public decimal Value34649 { get; set; }

    [JsonProperty("value_346410")]
    public decimal Value346410 { get; set; }

    [JsonProperty("value_346411")]
    public decimal Value346411 { get; set; }

    [JsonProperty("value_346412")]
    public decimal Value346412 { get; set; }

    [JsonProperty("value_346413")]
    public decimal Value346413 { get; set; }

    [JsonProperty("value_346414")]
    public decimal Value346414 { get; set; }

    [JsonProperty("value_346415")]
    public decimal Value346415 { get; set; }

    [JsonProperty("value_346416")]
    public decimal Value346416 { get; set; }

    [JsonProperty("value_346417")]
    public decimal Value346417 { get; set; }

    [JsonProperty("value_346418")]
    public decimal Value346418 { get; set; }

    [JsonProperty("value_346419")]
    public decimal Value346419 { get; set; }

    [JsonProperty("value_346420")]
    public decimal Value346420 { get; set; }

    public decimal GetValue(SaleColumnId columnId)
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