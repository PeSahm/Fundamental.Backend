using Fundamental.Application.Codals.Enums;
using System.Text.Json.Serialization;
using Fundamental.Domain.Codals.Manufacturing.Enums;
using Fundamental.Application.Codals.Services;
using Fundamental.Domain.Common.Enums;

namespace Fundamental.Application.Codals.Dto.MonthlyActivities.V1;

public class CodalMonthlyActivityV1 : ICodalMappingServiceMetadata
{
    [JsonPropertyName("productAndSales")]
    public List<ProductAndSalesV1Dto> ProductAndSales { get; set; } = new();

    [JsonPropertyName("description")]
    public DescriptionV1Dto Description { get; set; } = null!;

    [JsonPropertyName("financialYear")]
    public FinancialYearV1Dto FinancialYear { get; set; } = null!;

    [JsonPropertyName("listedCapital")]
    public string ListedCapital { get; set; } = null!;

    [JsonPropertyName("unauthorizedCapital")]
    public string UnauthorizedCapital { get; set; } = null!;

    public ReportingType ReportingType => ReportingType.Production;
    public LetterType LetterType => LetterType.MonthlyActivity;
    public CodalVersion CodalVersion => CodalVersion.V1;
    public LetterPart LetterPart => LetterPart.NotSpecified;
}