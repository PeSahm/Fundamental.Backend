using System.Diagnostics.CodeAnalysis;
using Fundamental.Application.Codals.Enums;
using Fundamental.Domain.Common.Enums;

namespace Fundamental.Application.Codals.Services;

public interface ICodalVersionDetector
{
    static virtual ReportingType ReportingType => ReportingType.UnKnown;
    static virtual LetterType LetterType => LetterType.UnKnown;
    static virtual LetterPart LetterPart => LetterPart.NotSpecified;

    /// <summary>
    ///     Detect Codal Json version based of letter type and reportingType.
    /// </summary>
    /// <param name="json">Codal raw json.</param>
    /// <returns> an enum that represents json version.</returns>
    CodalVersion DetectVersion([StringSyntax(StringSyntaxAttribute.Json)] string json);
}