using Ardalis.Specification;

namespace Fundamental.BuildingBlock.Extensions;

public static class SpecificationExtensions
{
    public static void LoadSpecification<T, T2>(this Specification<T, T2> targetSpec, ISpecification<T> referenceSpec)
        where T : class
    {
        if (referenceSpec is null)
        {
            throw new ArgumentNullException(nameof(referenceSpec));
        }

        foreach (WhereExpressionInfo<T> whereExpression in referenceSpec.WhereExpressions)
        {
            targetSpec.Query.Where(whereExpression.Filter);
        }

        foreach (OrderExpressionInfo<T> orderExpression in referenceSpec.OrderExpressions)
        {
            targetSpec.Query.OrderBy(orderExpression.KeySelector);
        }

        foreach (SearchExpressionInfo<T> searchCriteria in referenceSpec.SearchCriterias)
        {
            targetSpec.Query.Search(searchCriteria.Selector, searchCriteria.SearchTerm);
        }

        if (referenceSpec.Take.HasValue)
        {
            targetSpec.Query.Take(referenceSpec.Take.Value);
        }

        if (referenceSpec.Skip.HasValue)
        {
            targetSpec.Query.Skip(referenceSpec.Skip.Value);
        }
    }
}