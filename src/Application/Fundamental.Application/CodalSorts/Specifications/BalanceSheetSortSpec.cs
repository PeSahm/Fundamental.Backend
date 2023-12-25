using Ardalis.Specification;
using Fundamental.Application.CodalSorts.Queries.GetBalanceSheetSort;
using Fundamental.Domain.Codals.Entities;

namespace Fundamental.Application.CodalSorts.Specifications;

public class BalanceSheetSortSpec : Specification<BalanceSheetSort, GetBalanceSheetSortResultDto>
{
    public static BalanceSheetSortSpec GetValidSpecifications()
    {
        BalanceSheetSortSpec spec = new();
        spec.Query
            .Select(x => new GetBalanceSheetSortResultDto
            {
                Order = x.Order,
                CodalRow = x.CodalRow,
                Category = x.Category,
            })
            .AsNoTracking()
            .OrderBy(x => x.Order);
        return spec;
    }
}