using FluentValidation;

namespace Fundamental.Application.Symbols.Queries.GetSymbols;

public class GetSymbolsValidator : AbstractValidator<GetSymbolsRequest>
{
    public GetSymbolsValidator()
    {
        RuleFor(x => x.Filter)
            .MinimumLength(2)
            .WithMessage("Filter must be at least 2 characters long");
    }
}