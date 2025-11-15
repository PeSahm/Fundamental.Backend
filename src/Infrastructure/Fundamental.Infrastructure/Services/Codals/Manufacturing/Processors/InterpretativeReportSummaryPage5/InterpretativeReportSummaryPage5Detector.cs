using Fundamental.Application.Codals.Enums;
using Fundamental.Application.Codals.Services;
using Fundamental.Domain.Codals.Manufacturing.Enums;
using Fundamental.Domain.Common.Enums;
using Newtonsoft.Json.Linq;

namespace Fundamental.Infrastructure.Services.Codals.Manufacturing.Processors.InterpretativeReportSummaryPage5;

/// <summary>
/// Detects the version of interpretativeReportSummaryPage5 data in CODAL JSON.
/// </summary>
public class InterpretativeReportSummaryPage5Detector : ICodalVersionDetector
{
    public CodalVersion DetectVersion(string json)
    {
        try
        {
            JObject jObject = JObject.Parse(json);

            // V2: has interpretativeReportSummaryPage5 with version="2"
            if (jObject["interpretativeReportSummaryPage5"]?["version"]?.ToString() == "2")
            {
                return CodalVersion.V2;
            }

            // Default to None if version not recognized
            return CodalVersion.None;
        }
        catch
        {
            return CodalVersion.None;
        }
    }

    public static ReportingType ReportingType => ReportingType.Production;
    public static LetterType LetterType => LetterType.InterimStatement;
    public static LetterPart LetterPart => LetterPart.InterpretativeReportSummaryPage5;
}
