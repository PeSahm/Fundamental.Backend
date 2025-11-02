using Fundamental.Application.Codals.Enums;
using DNTPersianUtils.Core;
using Fundamental.Domain.Codals.Manufacturing.Enums;
using Fundamental.Application.Codals.Services;
using Fundamental.Domain.Common.Enums;
using Newtonsoft.Json;

namespace Fundamental.Application.Codals.Dto.MonthlyActivities.V5;

#pragma warning disable SA1402

public class CodalMonthlyActivityV5 : ICodalMappingServiceMetadata
{
    [JsonProperty("monthlyActivity")]
    public MonthlyActivityDtoV5? MonthlyActivity { get; set; }

    [JsonProperty("listedCapital")]
    public decimal ListedCapital { get; set; }

    [JsonProperty("unauthorizedCapital")]
    public decimal UnauthorizedCapital { get; set; }

    public ReportingType ReportingType => ReportingType.Production;
    public LetterType LetterType => LetterType.MonthlyActivity;
    public CodalVersion CodalVersion => CodalVersion.V5;
    public LetterPart LetterPart => LetterPart.NotSpecified;
}

public class MonthlyActivityDtoV5
{
    [JsonProperty("version")]
    public string Version { get; set; }

    [JsonProperty("buyRawMaterial")]
    public BuyRawMaterial? BuyRawMaterial { get; set; }

    [JsonProperty("productionAndSales")]
    public ProductionAndSalesV5? ProductionAndSales { get; set; }

    [JsonProperty("energy")]
    public Energy? Energy { get; set; }

    [JsonProperty("sourceUsesCurrency")]
    public SourceUsesCurrency? SourceUsesCurrency { get; set; }

    [JsonProperty("productMonthlyActivityDesc1")]
    public ProductMonthlyActivityDesc1V5 ProductMonthlyActivityDesc1 { get; set; }
}

public class BuyRawMaterial
{
    [JsonProperty("yearData")]
    public List<YearDatumV5> YearData { get; set; } = new();

    [JsonProperty("rowItems")]
    public List<BuyRawMaterialRowItem> RowItems { get; set; } = new();
}

public class BuyRawMaterialRowItem
{
    [JsonProperty("rowCode")]
    public int? RowCode { get; set; }

    [JsonProperty("oldFieldName")]
    public string OldFieldName { get; set; }

    [JsonProperty("category")]
    public int? Category { get; set; }

    [JsonProperty("rowType")]
    public string RowType { get; set; }

    [JsonProperty("id")]
    public string Id { get; set; }

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
}

public class ProductionAndSalesV5
{
    [JsonProperty("yearData")]
    public List<YearDatumV5> YearData { get; set; } = new();

    [JsonProperty("rowItems")]
    public List<ProductionAndSalesRowItemV5> RowItems { get; set; } = new();
}

public class ProductionAndSalesRowItemV5
{
    [JsonProperty("rowCode")]
    public int? RowCode { get; set; }

    [JsonProperty("oldFieldName")]
    public string OldFieldName { get; set; }

    [JsonProperty("category")]
    public int? Category { get; set; }

    [JsonProperty("rowType")]
    public string RowType { get; set; }

    [JsonProperty("productId")]
    public string ProductId { get; set; }

    [JsonProperty("value_11971")]
    public string Value11971 { get; set; }

    [JsonProperty("unitId")]
    public string UnitId { get; set; }

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

    [JsonProperty("value_11977")]
    public decimal Value11977 { get; set; }

    [JsonProperty("value_11978")]
    public decimal Value11978 { get; set; }

    [JsonProperty("value_11979")]
    public decimal Value11979 { get; set; }

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
}

public class Energy
{
    [JsonProperty("yearData")]
    public List<YearDatumV5> YearData { get; set; } = new();

    [JsonProperty("rowItems")]
    public List<EnergyRowItem> RowItems { get; set; } = new();
}

public class EnergyRowItem
{
    [JsonProperty("rowCode")]
    public int? RowCode { get; set; }

    [JsonProperty("oldFieldName")]
    public string OldFieldName { get; set; }

    [JsonProperty("category")]
    public int? Category { get; set; }

    [JsonProperty("rowType")]
    public string RowType { get; set; }

    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("value_31951")]
    public string Value31951 { get; set; }

    [JsonProperty("value_31952")]
    public string Value31952 { get; set; }

    [JsonProperty("value_31953")]
    public string Value31953 { get; set; }

    [JsonProperty("value_31954")]
    public string Value31954 { get; set; }

    [JsonProperty("value_31955")]
    public decimal Value31955 { get; set; }

    [JsonProperty("value_31956")]
    public decimal Value31956 { get; set; }

    [JsonProperty("value_31957")]
    public decimal Value31957 { get; set; }

    [JsonProperty("value_319511")]
    public decimal Value319511 { get; set; }

    [JsonProperty("value_319512")]
    public decimal Value319512 { get; set; }

    [JsonProperty("value_319513")]
    public decimal Value319513 { get; set; }

    [JsonProperty("value_319514")]
    public decimal Value319514 { get; set; }

    [JsonProperty("value_319515")]
    public decimal Value319515 { get; set; }

    [JsonProperty("value_319516")]
    public decimal Value319516 { get; set; }

    [JsonProperty("value_319517")]
    public decimal Value319517 { get; set; }

    [JsonProperty("value_319518")]
    public decimal Value319518 { get; set; }

    [JsonProperty("value_319519")]
    public decimal Value319519 { get; set; }

    [JsonProperty("value_319520")]
    public decimal Value319520 { get; set; }

    [JsonProperty("value_319521")]
    public decimal Value319521 { get; set; }

    [JsonProperty("value_319522")]
    public decimal Value319522 { get; set; }

    [JsonProperty("value_319523")]
    public decimal Value319523 { get; set; }

    [JsonProperty("value_319524")]
    public string Value319524 { get; set; }
}

public class SourceUsesCurrency
{
    [JsonProperty("yearData")]
    public List<YearDatumV5> YearData { get; set; } = new();

    [JsonProperty("rowItems")]
    public List<SourceUsesCurrencyRowItem> RowItems { get; set; } = new();
}

public class SourceUsesCurrencyRowItem
{
    [JsonProperty("rowCode")]
    public int? RowCode { get; set; }

    [JsonProperty("oldFieldName")]
    public string OldFieldName { get; set; }

    [JsonProperty("category")]
    public int? Category { get; set; }

    [JsonProperty("rowType")]
    public string RowType { get; set; }

    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("value_36401")]
    public string Value36401 { get; set; }

    [JsonProperty("value_36402")]
    public string Value36402 { get; set; }

    [JsonProperty("value_36403")]
    public decimal Value36403 { get; set; }

    [JsonProperty("value_36404")]
    public decimal Value36404 { get; set; }

    [JsonProperty("value_36405")]
    public decimal Value36405 { get; set; }

    [JsonProperty("value_36409")]
    public decimal Value36409 { get; set; }

    [JsonProperty("value_364010")]
    public decimal Value364010 { get; set; }

    [JsonProperty("value_364011")]
    public decimal Value364011 { get; set; }

    [JsonProperty("value_364012")]
    public decimal Value364012 { get; set; }

    [JsonProperty("value_364013")]
    public decimal Value364013 { get; set; }

    [JsonProperty("value_364014")]
    public decimal Value364014 { get; set; }

    [JsonProperty("value_364015")]
    public decimal Value364015 { get; set; }

    [JsonProperty("value_364016")]
    public decimal Value364016 { get; set; }

    [JsonProperty("value_364017")]
    public decimal Value364017 { get; set; }

    [JsonProperty("value_364018")]
    public decimal Value364018 { get; set; }

    [JsonProperty("value_364019")]
    public decimal Value364019 { get; set; }

    [JsonProperty("value_364020")]
    public decimal Value364020 { get; set; }

    [JsonProperty("value_364021")]
    public decimal Value364021 { get; set; }

    [JsonProperty("value_364022")]
    public decimal Value364022 { get; set; }

    [JsonProperty("value_364023")]
    public decimal Value364023 { get; set; }
}

public class ProductMonthlyActivityDesc1V5
{
    [JsonProperty("rowItems")]
    public List<ProductMonthlyActivityDesc1RowItemV5> RowItems { get; set; } = new();
}

public class ProductMonthlyActivityDesc1RowItemV5
{
    [JsonProperty("rowCode")]
    public int? RowCode { get; set; }

    [JsonProperty("oldFieldName")]
    public string OldFieldName { get; set; }

    [JsonProperty("category")]
    public int? Category { get; set; }

    [JsonProperty("rowType")]
    public string RowType { get; set; }

    [JsonProperty("value_11991")]
    public string Value11991 { get; set; }
}

public class YearDatumV5
{
    [JsonProperty("columnId")]
    public string ColumnId { get; set; }

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