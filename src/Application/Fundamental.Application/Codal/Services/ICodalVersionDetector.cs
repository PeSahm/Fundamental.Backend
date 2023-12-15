using System.Diagnostics.CodeAnalysis;
using Fundamental.Application.Codal.Enums;
using Fundamental.Application.Codal.Services.Enums;
using Fundamental.Domain.Statements.Enums;

namespace Fundamental.Application.Codal.Services;

public interface ICodalVersionDetector
{
    static virtual ReportingType ReportingType => ReportingType.UnKnown;
    static virtual LetterType LetterType => LetterType.UnKnown;

    /// <summary>
    /// Detect Codal Json version based of letter type and reportingType
    /// </summary>
    /// <param name="json">Codal raw json.</param>
    /// <returns> an enum that represents json version.</returns>
    CodalVersion DetectVersion([StringSyntax(StringSyntaxAttribute.Json)] string json);
}