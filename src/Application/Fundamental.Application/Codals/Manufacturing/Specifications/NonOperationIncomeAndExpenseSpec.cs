using Ardalis.Specification;
using Fundamental.Domain.Codals.Manufacturing.Entities;

namespace Fundamental.Application.Codals.Manufacturing.Specifications;

public class NonOperationIncomeAndExpenseSpec : Specification<NonOperationIncomeAndExpense>
{
    public NonOperationIncomeAndExpenseSpec GetById(Guid id)
    {
        Query.Where(x => x.Id == id);
        return this;
    }
}