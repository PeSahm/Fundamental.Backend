using System.Text.Json.Serialization;
using Fundamental.Application.Codals.Enums;
using Fundamental.Application.Codals.Services;
using Fundamental.Domain.Codals.Manufacturing.Enums;
using Fundamental.Domain.Common.Enums;

namespace Fundamental.Application.Codals.Dto.MonthlyActivities.V2;

public class CodalMonthlyActivityV2 : ICodalMappingServiceMetadata
{
    [JsonPropertyName("productAndSales")]
    public ProductAndSalesV2Dto ProductAndSales { get; set; } = null!;

    [JsonPropertyName("listedCapital")]
    public string ListedCapital { get; set; } = null!;

    [JsonPropertyName("unauthorizedCapital")]
    public string UnauthorizedCapital { get; set; } = null!;

    public ReportingType ReportingType => ReportingType.Production;
    public LetterType LetterType => LetterType.MonthlyActivity;
    public CodalVersion CodalVersion => CodalVersion.V2;
    public LetterPart LetterPart => LetterPart.NotSpecified;
}