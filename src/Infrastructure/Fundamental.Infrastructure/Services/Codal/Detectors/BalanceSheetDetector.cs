using Fundamental.Application.Codal.Enums;
using Fundamental.Application.Codal.Services;
using Fundamental.Application.Codal.Services.Enums;
using Fundamental.Domain.Statements.Enums;

namespace Fundamental.Infrastructure.Services.Codal.Detectors;

public class BalanceSheetDetector : ICodalVersionDetector
{
    public CodalVersion DetectVersion(string json)
    {
        return CodalVersion.V5;
    }

    public static ReportingType ReportingType => ReportingType.Services;

    public static LetterType LetterType => LetterType.InterimStatement;
}