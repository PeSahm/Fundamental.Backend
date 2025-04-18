using Ardalis.Specification;
using Fundamental.Application.Codals.Manufacturing.Queries.GetBalanceSheetSort;
using Fundamental.Domain.Codals.Manufacturing.Entities;

namespace Fundamental.Application.Codals.Manufacturing.Specifications;

public class BalanceSheetSortSpec : Specification<BalanceSheetSort, GetBalanceSheetSortResultDto>
{
    public static BalanceSheetSortSpec GetValidSpecifications()
    {
        BalanceSheetSortSpec spec = new();

        spec.Query.AsNoTracking()
            .OrderBy(x => x.Order)
            .Select(x => new GetBalanceSheetSortResultDto
            {
                Order = x.Order,
                CodalRow = x.CodalRow,
                Category = x.Category
            });
        return spec;
    }
}