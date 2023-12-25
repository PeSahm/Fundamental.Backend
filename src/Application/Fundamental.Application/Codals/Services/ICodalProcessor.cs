using Fundamental.Application.Codals.Enums;
using Fundamental.Application.Codals.Services.Enums;
using Fundamental.Application.Codals.Services.Models;
using Fundamental.Domain.Common.Enums;

namespace Fundamental.Application.Codals.Services;

public interface ICodalProcessor
{
    static virtual ReportingType ReportingType => ReportingType.UnKnown;

    static virtual LetterType LetterType => LetterType.UnKnown;

    static virtual CodalVersion CodalVersion => CodalVersion.V1;

    static virtual LetterPart LetterPart => LetterPart.NotSpecified;

    /// <summary>
    /// Detect Codal Json version based of letter type and reportingType
    /// </summary>
    /// <param name="statement">Codal Statement Data.</param>
    /// <param name="model">Codal Statement Json Data.</param>
    /// <param name="cancellationToken">cancellation Token.</param>
    /// <returns> an enum that represents json version.</returns>
    Task Process(GetStatementResponse statement, GetStatementJsonResponse model, CancellationToken cancellationToken);
}