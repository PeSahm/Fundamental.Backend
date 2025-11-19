using Fundamental.Application.Codals.Enums;
using Fundamental.Application.Codals.Services;
using Fundamental.Domain.Codals.Manufacturing.Enums;
using Fundamental.Domain.Common.Enums;
using Newtonsoft.Json.Linq;

namespace Fundamental.Infrastructure.Services.Codals.Manufacturing.Detectors;

/// <summary>
/// Detector for Extraordinary General Meeting Decisions V1 JSON structure.
/// </summary>
public class ExtraAssemblyDetector : ICodalVersionDetector
{
    public CodalVersion DetectVersion(string json)
    {
        try
        {
            JObject root = JObject.Parse(json);

            // V1 contains "parentAssembly" property at root
            if (root["parentAssembly"] != null)
            {
                return CodalVersion.V1;
            }

            return CodalVersion.None;
        }
        catch
        {
            return CodalVersion.None;
        }
    }

    public static ReportingType ReportingType => ReportingType.Production;

    public static LetterType LetterType => LetterType.ExtraordinaryGeneralMeetingDecisions;

    public static LetterPart LetterPart => LetterPart.NotSpecified;
}
