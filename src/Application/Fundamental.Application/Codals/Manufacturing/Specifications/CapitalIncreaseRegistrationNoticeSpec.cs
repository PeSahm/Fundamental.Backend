using Ardalis.Specification;
using Fundamental.Domain.Codals.Manufacturing.Entities;

namespace Fundamental.Application.Codals.Manufacturing.Specifications;

public sealed class CapitalIncreaseRegistrationNoticeSpec : Specification<CapitalIncreaseRegistrationNotice>
{
    public CapitalIncreaseRegistrationNoticeSpec WhereTraceNo(ulong requestTraceNo)
    {
        Query.Where(x => x.TraceNo == requestTraceNo);
        return this;
    }

    public CapitalIncreaseRegistrationNoticeSpec WhereSymbol(string isin)
    {
        Query.Where(x => x.Symbol.Isin == isin);
        return this;
    }

    public CapitalIncreaseRegistrationNoticeSpec WhereId(Guid id)
    {
        Query.Where(x => x.Id == id);
        return this;
    }

    public CapitalIncreaseRegistrationNoticeSpec WhereIdNot(Guid id)
    {
        Query.Where(x => x.Id != id);
        return this;
    }

    public CapitalIncreaseRegistrationNoticeResultItemSpec Select()
    {
        CapitalIncreaseRegistrationNoticeResultItemSpec select = new();

        foreach (WhereExpressionInfo<CapitalIncreaseRegistrationNotice> whereExpression in WhereExpressions)
        {
            select.Query.Where(whereExpression.Filter);
        }

        return select;
    }
}