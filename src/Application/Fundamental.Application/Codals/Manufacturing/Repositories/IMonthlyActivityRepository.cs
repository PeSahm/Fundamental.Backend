using Fundamental.Application.Codals.Manufacturing.Queries.GetMonthlyActivities;
using Fundamental.Domain.Codals.Manufacturing.Entities;
using Fundamental.Domain.Codals.ValueObjects;
using Fundamental.Domain.Common.Dto;

namespace Fundamental.Application.Codals.Manufacturing.Repositories;

public interface IMonthlyActivityRepository
{
    /// <summary>
    /// Retrieves a paginated list of monthly activities matching the specified request criteria.
    /// </summary>
    /// <param name="request">Filters, sorting, and pagination options for the query.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <returns>A paginated collection of <see cref="GetMonthlyActivitiesListItem"/> representing the matching monthly activities.</returns>
    Task<Paginated<GetMonthlyActivitiesListItem>> GetMonthlyActivitiesAsync(
        GetMonthlyActivitiesRequest request,
        CancellationToken cancellationToken
    );

    Task<CanonicalMonthlyActivity?> GetFirstMonthlyActivity(
        string isin,
        FiscalYear fiscalYear,
        StatementMonth month,
        CancellationToken cancellationToken
    );

    Task<CanonicalMonthlyActivity?> GetMonthlyActivity(
        string isin,
        FiscalYear fiscalYear,
        StatementMonth month,
        CancellationToken cancellationToken
    );
}