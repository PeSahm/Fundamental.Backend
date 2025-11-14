using FluentValidation;

namespace Fundamental.Application.Codals.Manufacturing.Queries.GetBalanceSheets;

public sealed class GetBalanceSheetRequestValidator : AbstractValidator<GetBalanceSheetRequest>
{
    public GetBalanceSheetRequestValidator()
    {
        RuleFor(x => x.PageSize).GreaterThan(0)
            .WithMessage("PageSize must be greater than 0");

        RuleFor(x => x.PageNumber).GreaterThan(0)
            .WithMessage("PageNumber must be greater than 0");
    }
}