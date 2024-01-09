using FluentValidation;

namespace Fundamental.Application.Symbols.Commands.RejectSymbolShareHolder;

public sealed class RejectSymbolShareHolderRequestValidator : AbstractValidator<RejectSymbolShareHolderRequest>
{
    public RejectSymbolShareHolderRequestValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}