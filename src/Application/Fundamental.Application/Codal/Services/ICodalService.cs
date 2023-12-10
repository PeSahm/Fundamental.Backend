using Fundamental.Application.Codal.Services.Enums;
using Fundamental.Application.Codal.Services.Models;

namespace Fundamental.Application.Codal.Services;

public interface ICodalService
{
    Task<List<GetStatementResponse>> GetMonthlyActivities(
        DateTime fromDate,
        ReportingType reportingType,
        CancellationToken cancellationToken = default
    );

    Task UpsertMonthlyActivities(GetStatementResponse statement, CancellationToken cancellationToken = default);
}