using Fundamental.Application.Codals.Enums;
using Fundamental.Application.Codals.Services.Enums;
using Fundamental.Domain.Common.Enums;

namespace Fundamental.Application.Codals.Services;

public interface ICodalVersionDetectorFactory
{
    ICodalVersionDetector GetCodalVersionDetector(
        ReportingType reportingType,
        LetterType letterType,
        LetterPart letterPart
    );
}