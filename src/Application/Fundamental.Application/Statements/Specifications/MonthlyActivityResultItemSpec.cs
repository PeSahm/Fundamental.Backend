using Ardalis.Specification;
using Fundamental.Application.Statements.Queries.GetMonthlyActivities;
using Fundamental.Domain.Common.ValueObjects;
using Fundamental.Domain.Statements.Entities;

namespace Fundamental.Application.Statements.Specifications;

public sealed class MonthlyActivityResultItemSpec : Specification<MonthlyActivity, GetMonthlyActivitiesResultItem>
{
    public MonthlyActivityResultItemSpec()
    {
        Query.Select(x => new GetMonthlyActivitiesResultItem(
            x.Id,
            x.Symbol.Isin,
            x.Symbol.Name,
            x.Symbol.Title,
            x.Uri,
            x.FiscalYear,
            x.YearEndMonth,
            x.ReportMonth,
            (CodalMoney)x.SaleBeforeCurrentMonth,
            (CodalMoney)x.SaleCurrentMonth,
            (CodalMoney)x.SaleIncludeCurrentMonth,
            (CodalMoney)x.SaleLastYear,
            x.HasSubCompanySale,
            x.TraceNo,
            x.CreatedAt
        ));
    }
}