using Ardalis.Specification;
using Fundamental.Application.Codals.Manufacturing.Queries.GetIncomeStatementSort;
using Fundamental.Domain.Codals.Manufacturing.Entities;

namespace Fundamental.Application.Codals.Manufacturing.Specifications;

public class IncomeStatementSortSpec : Specification<IncomeStatementSort, GetIncomeStatementSortResultDto>
{
    public static IncomeStatementSortSpec GetValidSpecifications()
    {
        IncomeStatementSortSpec spec = new();
        spec.Query
            .Select(x => new GetIncomeStatementSortResultDto
            {
                Order = x.Order,
                CodalRow = x.CodalRow,
            })
            .AsNoTracking()
            .OrderBy(x => x.Order);
        return spec;
    }
}