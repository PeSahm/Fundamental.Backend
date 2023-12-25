using Fundamental.Application.Codals.Enums;
using Fundamental.Application.Codals.Services.Enums;
using Fundamental.Application.Codals.Services.Models;
using Fundamental.Domain.Common.Enums;

namespace Fundamental.Application.Codals.Services;

public interface ICodalService
{
    Task<List<GetStatementResponse>> GetStatements(
        DateTime fromDate,
        ReportingType reportingType,
        LetterType letterType,
        CancellationToken cancellationToken = default
    );

    Task ProcessCodal(GetStatementResponse statement, LetterPart letterPart, CancellationToken cancellationToken = default);
}