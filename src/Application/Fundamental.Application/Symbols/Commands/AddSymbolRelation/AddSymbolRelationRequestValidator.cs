using FluentValidation;

namespace Fundamental.Application.Symbols.Commands.AddSymbolRelation;

public sealed class AddSymbolRelationRequestValidator : AbstractValidator<AddSymbolRelationRequest>
{
    public AddSymbolRelationRequestValidator()
    {
        RuleFor(x => x.Investor)
            .NotEmpty()
            .WithMessage("Investor is required");

        RuleFor(x => x.Investment)
            .NotEmpty()
            .WithMessage("Investment is required");

        RuleFor(x => x.Ratio)
            .GreaterThan(0)
            .WithMessage("Ratio must be greater than 0");
    }
}