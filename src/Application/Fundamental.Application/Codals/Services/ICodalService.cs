using Fundamental.Application.Codals.Enums;
using Fundamental.Application.Codals.Services.Models.CodelServiceModels;
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

    Task<GetStatementResponse?> GetStatementByTraceNo(
        ulong traceNo,
        CancellationToken cancellationToken = default
    );

    Task ProcessCodal(GetStatementResponse statement, LetterPart letterPart, CancellationToken cancellationToken = default);

    Task ProcessCodal(
        GetStatementResponse statement,
        ReportingType reportingType,
        LetterPart letterPart,
        CancellationToken cancellationToken = default
    );

    Task<List<GetPublisherResponse>> GetPublishers(CancellationToken cancellationToken = default);
}