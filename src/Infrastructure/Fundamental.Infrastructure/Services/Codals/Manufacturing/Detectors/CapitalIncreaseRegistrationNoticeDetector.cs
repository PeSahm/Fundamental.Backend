using Fundamental.Application.Codals.Enums;
using Fundamental.Application.Codals.Services;
using Fundamental.Domain.Codals.Manufacturing.Enums;
using Fundamental.Domain.Common.Enums;

namespace Fundamental.Infrastructure.Services.Codals.Manufacturing.Detectors;

public class CapitalIncreaseRegistrationNoticeDetector : ICodalVersionDetector
{
    public CodalVersion DetectVersion(string json)
    {
        return CodalVersion.V1;
    }

    public static ReportingType ReportingType => ReportingType.UnKnown;

    public static LetterType LetterType => LetterType.CapitalIncreaseRegistrationNotice;

    public static LetterPart LetterPart => LetterPart.NotSpecified;
}