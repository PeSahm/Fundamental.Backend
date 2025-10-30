using System.Text.Json.Serialization;

namespace Fundamental.Application.Codals.Dto.MonthlyActivities.V2;

public class ProductAndSalesV2Dto
{
    [JsonPropertyName("financialYear")]
    public FinancialYearV2Dto FinancialYear { get; set; } = null!;

    [JsonPropertyName("fieldsItems")]
    public List<FieldsItemV2Dto> FieldsItems { get; set; } = new();

    [JsonPropertyName("descriptions")]
    public List<DescriptionV2Dto> Descriptions { get; set; } = new();
}