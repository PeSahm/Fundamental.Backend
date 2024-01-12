using Fundamental.Application.Codals.Enums;
using Fundamental.Application.Codals.Services;
using Fundamental.Domain.Common.Enums;

namespace Fundamental.Infrastructure.Services.Codals.Manufacturing.Detectors;

public class BalanceSheetDetector : ICodalVersionDetector
{
    public CodalVersion DetectVersion(string json)
    {
        return CodalVersion.V5;
    }

    public static ReportingType ReportingType => ReportingType.Production;

    public static LetterType LetterType => LetterType.InterimStatement;

    public static LetterPart LetterPart => LetterPart.BalanceSheet;
}