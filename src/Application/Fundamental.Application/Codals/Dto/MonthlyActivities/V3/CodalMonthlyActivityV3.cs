using Fundamental.Application.Codals.Enums;
using Fundamental.Application.Codals.Services;
using Fundamental.Domain.Codals.Manufacturing.Enums;
using Fundamental.Domain.Common.Enums;
using Newtonsoft.Json;

namespace Fundamental.Application.Codals.Dto.MonthlyActivities.V3;

public class CodalMonthlyActivityV3 : ICodalMappingServiceMetadata
{
    [JsonProperty("monthlyActivity")]
    public MonthlyActivityDtoV3 MonthlyActivity { get; set; } = null!;

    [JsonProperty("listedCapital")]
    public string ListedCapital { get; set; } = null!;

    [JsonProperty("unauthorizedCapital")]
    public string UnauthorizedCapital { get; set; } = null!;

    public ReportingType ReportingType => ReportingType.Production;
    public LetterType LetterType => LetterType.MonthlyActivity;
    public CodalVersion CodalVersion => CodalVersion.V3;
    public LetterPart LetterPart => LetterPart.NotSpecified;
}