using Fundamental.Application.Codals.Services;
using Fundamental.Domain.Codals.Manufacturing.Enums;
using Newtonsoft.Json.Linq;

namespace Fundamental.Infrastructure.Services.Codals.Manufacturing.Processors.AnnualAssembly;

/// <summary>
/// Detector for Annual Assembly V1 JSON structure.
/// </summary>
public class AnnualAssemblyDetector : ICodalVersionDetector
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
}
