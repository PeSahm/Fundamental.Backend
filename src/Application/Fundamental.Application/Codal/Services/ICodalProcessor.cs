using Fundamental.Application.Codal.Enums;
using Fundamental.Application.Codal.Services.Enums;
using Fundamental.Application.Codal.Services.Models;
using Fundamental.Domain.Statements.Enums;

namespace Fundamental.Application.Codal.Services;

public interface ICodalProcessor
{
    static virtual ReportingType ReportingType => ReportingType.UnKnown;

    static virtual LetterType LetterType => LetterType.UnKnown;

    static virtual CodalVersion CodalVersion => CodalVersion.V1;

    /// <summary>
    /// Detect Codal Json version based of letter type and reportingType
    /// </summary>
    /// <param name="statement">Codal Statement Data.</param>
    /// <param name="model">Codal Statement Json Data.</param>
    /// <param name="cancellationToken">cancellation Token.</param>
    /// <returns> an enum that represents json version.</returns>
    Task Process(GetStatementResponse statement, GetStatementJsonResponse model, CancellationToken cancellationToken);
}