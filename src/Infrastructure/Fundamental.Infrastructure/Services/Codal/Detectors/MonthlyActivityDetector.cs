using Fundamental.Application.Codal.Enums;
using Fundamental.Application.Codal.Services;
using Fundamental.Application.Codal.Services.Enums;
using Fundamental.Domain.Statements.Enums;

namespace Fundamental.Infrastructure.Services.Codal.Detectors;

public class MonthlyActivityDetector : ICodalVersionDetector
{
    public CodalVersion DetectVersion(string json)
    {
        return CodalVersion.V4;
    }

    public static ReportingType ReportingType => ReportingType.Production;

    public static LetterType LetterType => LetterType.MonthlyActivity;
}