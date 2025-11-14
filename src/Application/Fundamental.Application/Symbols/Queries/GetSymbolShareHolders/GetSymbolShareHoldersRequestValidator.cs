using FluentValidation;

namespace Fundamental.Application.Symbols.Queries.GetSymbolShareHolders;

public sealed class GetSymbolShareHoldersRequestValidator : AbstractValidator<GetSymbolShareHoldersRequest>
{
    public GetSymbolShareHoldersRequestValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThan(0)
            .WithMessage("PageNumber must be greater than 0");
    }
}