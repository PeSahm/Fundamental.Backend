using Fundamental.Domain.Codals.Manufacturing.Enums;
using Fundamental.Application.Codals.Enums;
using Fundamental.Application.Codals.Services;
using Fundamental.Domain.Common.Enums;
using Newtonsoft.Json.Linq;

namespace Fundamental.Infrastructure.Services.Codals.Manufacturing.Detectors;

public class MonthlyActivityDetector : ICodalVersionDetector
{
    public CodalVersion DetectVersion(string json)
    {
        try
        {
            JObject jObject = JObject.Parse(json);

            // V1: has productAndSales as array at root level AND has description with periodEndToDateDescription
            // This is the oldest format and should be checked first
            if (jObject["productAndSales"]?.Type == JTokenType.Array &&
                jObject["description"]?["periodEndToDateDescription"] != null)
            {
                return CodalVersion.V1;
            }

            // V2: has productAndSales (object, not array) with fieldsItems at root level
            // Distinguished from V1 by having fieldsItems
            if (jObject["productAndSales"]?.Type == JTokenType.Object &&
                jObject["productAndSales"]?["fieldsItems"] != null)
            {
                return CodalVersion.V2;
            }

            // V5: has monthlyActivity with buyRawMaterial, energy, AND sourceUsesCurrency (all three required)
            if (jObject["monthlyActivity"]?["buyRawMaterial"] != null &&
                jObject["monthlyActivity"]?["energy"] != null &&
                jObject["monthlyActivity"]?["sourceUsesCurrency"] != null)
            {
                return CodalVersion.V5;
            }

            // V4: has monthlyActivity with productionAndSales, energy, and productMonthlyActivityDesc1
            // V4 is distinguished from V3 by having an energy section
            if (jObject["monthlyActivity"]?["productionAndSales"] != null &&
                jObject["monthlyActivity"]?["energy"] != null &&
                jObject["monthlyActivity"]?["productMonthlyActivityDesc1"] != null)
            {
                return CodalVersion.V4;
            }

            // V3: has monthlyActivity with productionAndSales and productMonthlyActivityDesc1
            // but no energy section (which distinguishes it from V4)
            if (jObject["monthlyActivity"]?["productionAndSales"] != null &&
                jObject["monthlyActivity"]?["productMonthlyActivityDesc1"] != null)
            {
                return CodalVersion.V3;
            }

            // Default to V4 for backward compatibility
            return CodalVersion.V4;
        }
        catch
        {
            // If parsing fails, default to V4
            return CodalVersion.V4;
        }
    }

    public static ReportingType ReportingType => ReportingType.Production;

    public static LetterType LetterType => LetterType.MonthlyActivity;

    public static LetterPart LetterPart => LetterPart.NotSpecified;
}