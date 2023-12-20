using Ardalis.Specification;
using Fundamental.Application.Statements.Queries.GetMonthlyActivities;
using Fundamental.Domain.Common.ValueObjects;
using Fundamental.Domain.Statements.Entities;

namespace Fundamental.Application.Statements.Specifications;

public sealed class MonthlyActivityResultItemSpec : Specification<MonthlyActivity, GetMonthlyActivitiesResultItem>
{
    public MonthlyActivityResultItemSpec()
    {
        Query.Select(x => new GetMonthlyActivitiesResultItem
        {
            Id = x.Id,
            Isin = x.Symbol.Isin,
            Symbol = x.Symbol.Name,
            Title = x.Symbol.Title,
            Uri = x.Uri,
            FiscalYear = x.FiscalYear,
            YearEndMonth = x.YearEndMonth,
            ReportMonth = x.ReportMonth,
            SaleBeforeCurrentMonth = (CodalMoney)x.SaleBeforeCurrentMonth,
            SaleCurrentMonth = (CodalMoney)x.SaleCurrentMonth,
            SaleIncludeCurrentMonth = (CodalMoney)x.SaleIncludeCurrentMonth,
            SaleLastYear = (CodalMoney)x.SaleLastYear,
            HasSubCompanySale = x.HasSubCompanySale,
            TraceNo = x.TraceNo,
            CreatedAt = x.CreatedAt
        });
    }
}