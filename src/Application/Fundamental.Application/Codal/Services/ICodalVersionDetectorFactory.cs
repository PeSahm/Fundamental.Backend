using Fundamental.Application.Codal.Enums;
using Fundamental.Application.Codal.Services.Enums;
using Fundamental.Domain.Statements.Enums;

namespace Fundamental.Application.Codal.Services;

public interface ICodalVersionDetectorFactory
{
    ICodalVersionDetector GetCodalVersionDetector(
        ReportingType reportingType,
        LetterType letterType,
        LetterPart letterPart = LetterPart.NotSpecified
    );
}