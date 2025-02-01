using Ardalis.Specification;
using Fundamental.Domain.Symbols.Entities;

namespace Fundamental.Application.Symbols.Specifications;

public class IndexCompanySpec : Specification<IndexCompany>
{
    public IndexCompanySpec WhereIndexIsin(string isin)
    {
        Query.Where(x => x.Index.Isin == isin);
        return this;
    }

    public IndexCompanySpec WhereIndexInsCode(string tseInsCode)
    {
        Query.Where(x => x.Index.TseInsCode == tseInsCode);
        return this;
    }

    public IndexCompanySpec WhereCompanyIsin(string companyIsin)
    {
        Query.Where(x => x.Company.Isin == companyIsin);
        return this;
    }

    public IndexCompanySpec WhereCompanyInsCode(string companyIsin)
    {
        Query.Where(x => x.Company.TseInsCode == companyIsin);
        return this;
    }

    public new IndexCompanySpec AsNoTracking()
    {
        Query.AsNoTracking();
        return this;
    }
}