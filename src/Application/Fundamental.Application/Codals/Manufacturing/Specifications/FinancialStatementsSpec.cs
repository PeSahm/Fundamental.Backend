using Ardalis.Specification;
using Fundamental.BuildingBlock.Extensions;
using Fundamental.Domain.Codals.Manufacturing.Entities;

namespace Fundamental.Application.Codals.Manufacturing.Specifications;

public class FinancialStatementsSpec : Specification<FinancialStatement>
{
    public FinancialStatementsSpec WhereIsin(string isin)
    {
        Query.Where(x => x.Symbol.Isin == isin);
        return this;
    }

    public FinancialStatementsResultSpec ToResultDto()
    {
        FinancialStatementsResultSpec select = new();
        select.LoadSpecification(this);
        return select;
    }

    public new FinancialStatementsSpec AsNoTracking()
    {
        Query.AsNoTracking();
        return this;
    }
}