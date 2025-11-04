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

            // V5: has buyRawMaterial, energy, AND sourceUsesCurrency sections (all three required)
            if (jObject["monthlyActivity"]?["buyRawMaterial"] != null &&
                jObject["monthlyActivity"]?["energy"] != null &&
                jObject["monthlyActivity"]?["sourceUsesCurrency"] != null)
            {
                return CodalVersion.V5;
            }

            // V4: has monthlyActivity with productionAndSales and productMonthlyActivityDesc1
            // but no buyRawMaterial/energy sections
            if (jObject["monthlyActivity"]?["productionAndSales"] != null &&
                jObject["monthlyActivity"]?["productMonthlyActivityDesc1"] != null)
            {
                // Check if it's V4 by looking for specific V4 structure
                JToken? productionAndSales = jObject["monthlyActivity"]?["productionAndSales"];

                if (productionAndSales?["yearData"] != null)
                {
                    return CodalVersion.V4;
                }
            }

            // V3: has monthlyActivity with productionAndSales and productMonthlyActivityDesc1
            if (jObject["monthlyActivity"]?["productionAndSales"] != null &&
                jObject["monthlyActivity"]?["productMonthlyActivityDesc1"] != null)
            {
                return CodalVersion.V3;
            }

            // V2: has productAndSales with fieldsItems
            if (jObject["productAndSales"]?["fieldsItems"] != null)
            {
                return CodalVersion.V2;
            }

            // V1: has productAndSales as array
            if (jObject["productAndSales"] is JArray)
            {
                return CodalVersion.V1;
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