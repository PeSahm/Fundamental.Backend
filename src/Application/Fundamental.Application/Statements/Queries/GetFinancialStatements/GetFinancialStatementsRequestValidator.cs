using FluentValidation;

namespace Fundamental.Application.Statements.Queries.GetFinancialStatements;

public sealed class GetFinancialStatementsRequestValidator : AbstractValidator<GetFinancialStatementsRequest>
{
    public GetFinancialStatementsRequestValidator()
    {
        RuleFor(x => x.PageSize).GreaterThan(0)
            .WithMessage("PageSize must be greater than 0");

        RuleFor(x => x.PageNumber).GreaterThan(0)
            .WithMessage("PageNumber must be greater than 0");
    }
}