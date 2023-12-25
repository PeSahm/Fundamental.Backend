using FluentValidation;

namespace Fundamental.Application.Codals.Manufacturing.Queries.GetIncomeStatements;

public sealed class GetIncomeStatementsRequestValidator : AbstractValidator<GetIncomeStatementsRequest>
{
    public GetIncomeStatementsRequestValidator()
    {
        RuleFor(x => x.PageSize).GreaterThan(0)
            .WithMessage("PageSize must be greater than 0");

        RuleFor(x => x.PageNumber).GreaterThan(0)
            .WithMessage("PageNumber must be greater than 0");
    }
}