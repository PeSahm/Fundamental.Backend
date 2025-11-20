using Fundamental.Application.Codals.Enums;
using Fundamental.Application.Codals.Services;
using Fundamental.Domain.Codals.Manufacturing.Enums;
using Fundamental.Domain.Common.Enums;
using Newtonsoft.Json.Linq;

namespace Fundamental.Infrastructure.Services.Codals.Manufacturing.Detectors;

/// <summary>
/// Detector for Extraordinary Annual Assembly V1 JSON structure.
/// </summary>
public class ExtraAnnualAssemblyDetector : ICodalVersionDetector
{
    /// <summary>
    /// Determines the CodalVersion of a codal JSON document used for extraordinary annual assembly reports.
    /// </summary>
    /// <param name="json">A JSON string representing the codal document to inspect.</param>
    /// <returns>CodalVersion.V1 if the JSON root contains a "parentAssembly" property; CodalVersion.None if the property is absent or the input cannot be parsed.</returns>
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

    public static LetterType LetterType => LetterType.OrdinaryGeneralMeetingExtraordinaryDecisions;

    public static LetterPart LetterPart => LetterPart.NotSpecified;
}