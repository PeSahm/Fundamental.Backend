using Newtonsoft.Json;

namespace Fundamental.Application.Codals.Dto.MonthlyActivities.V3;

public class ProductionAndSalesRowItemV3Dto
{
    [JsonProperty("rowCode")]
    public int RowCode { get; set; }

    [JsonProperty("oldFieldName")]
    public string OldFieldName { get; set; } = string.Empty;

    [JsonProperty("category")]
    public int Category { get; set; }

    [JsonProperty("rowType")]
    public string RowType { get; set; } = null!;

    [JsonProperty("productId")]
    public string? ProductId { get; set; }

    [JsonProperty("unitId")]
    public string? UnitId { get; set; }

    // Dynamic properties for values
    [JsonProperty("value_11971")]
    public string? Value11971 { get; set; }

    [JsonProperty("value_11972")]
    public string? Value11972 { get; set; }

    [JsonProperty("value_11973")]
    public string? Value11973 { get; set; }

    [JsonProperty("value_11974")]
    public string? Value11974 { get; set; }

    [JsonProperty("value_11975")]
    public string? Value11975 { get; set; }

    [JsonProperty("value_11976")]
    public string? Value11976 { get; set; }

    [JsonProperty("value_11977")]
    public string? Value11977 { get; set; }

    [JsonProperty("value_11978")]
    public string? Value11978 { get; set; }

    [JsonProperty("value_11979")]
    public string? Value11979 { get; set; }

    [JsonProperty("value_119710")]
    public string? Value119710 { get; set; }

    [JsonProperty("value_119711")]
    public string? Value119711 { get; set; }

    [JsonProperty("value_119712")]
    public string? Value119712 { get; set; }

    [JsonProperty("value_119713")]
    public string? Value119713 { get; set; }

    [JsonProperty("value_119714")]
    public string? Value119714 { get; set; }

    [JsonProperty("value_119715")]
    public string? Value119715 { get; set; }

    [JsonProperty("value_119716")]
    public string? Value119716 { get; set; }

    [JsonProperty("value_119717")]
    public string? Value119717 { get; set; }

    [JsonProperty("value_119718")]
    public string? Value119718 { get; set; }

    [JsonProperty("value_119719")]
    public string? Value119719 { get; set; }

    [JsonProperty("value_119720")]
    public string? Value119720 { get; set; }

    [JsonProperty("value_119721")]
    public string? Value119721 { get; set; }

    [JsonProperty("value_119722")]
    public string? Value119722 { get; set; }

    [JsonProperty("value_119723")]
    public string? Value119723 { get; set; }

    [JsonProperty("value_119724")]
    public string? Value119724 { get; set; }

    [JsonProperty("value_119725")]
    public string? Value119725 { get; set; }

    [JsonProperty("value_119726")]
    public string? Value119726 { get; set; }
}