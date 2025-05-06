using Fundamental.Application.Codals.Manufacturing.Queries.GetMonthlyActivities;
using Fundamental.Domain.Codals.Manufacturing.Entities;
using Fundamental.Domain.Codals.ValueObjects;
using Fundamental.Domain.Common.Dto;

namespace Fundamental.Application.Codals.Manufacturing.Repositories;

public interface IMonthlyActivityRepository
{
    Task<Paginated<GetMonthlyActivitiesResultItem>> GetMonthlyActivitiesAsync(
        GetMonthlyActivitiesRequest request,
        CancellationToken cancellationToken
    );

    Task<MonthlyActivity?> GetFirstMontlyActivity(
        string isin,
        FiscalYear fiscalYear,
        StatementMonth month,
        CancellationToken cancellationToken
    );
}