using Fundamental.Application.Codal.Services.Enums;
using Fundamental.Application.Codal.Services.Models;
using Fundamental.Domain.Statements.Enums;

namespace Fundamental.Application.Codal.Services;

public interface ICodalService
{
    Task<List<GetStatementResponse>> GetStatements(
        DateTime fromDate,
        ReportingType reportingType,
        LetterType letterType,
        CancellationToken cancellationToken = default
    );

    Task ProcessCodal(GetStatementResponse statement, CancellationToken cancellationToken = default);
}