using Fundamental.Application.Codals.Enums;
using Fundamental.Application.Codals.Services;
using Fundamental.Application.Codals.Services.Enums;
using Fundamental.Domain.Common.Enums;

namespace Fundamental.Infrastructure.Services.Codals.Manufactering.Detectors;

public class MonthlyActivityDetector : ICodalVersionDetector
{
    public CodalVersion DetectVersion(string json)
    {
        return CodalVersion.V4;
    }

    public static ReportingType ReportingType => ReportingType.Production;

    public static LetterType LetterType => LetterType.MonthlyActivity;

    public static LetterPart LetterPart => LetterPart.NotSpecified;
}