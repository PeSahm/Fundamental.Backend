using FluentValidation;

namespace Fundamental.Application.Symbols.Commands.ApproveSymbolShareHolder;

public sealed class ApproveSymbolShareHolderRequestValidator : AbstractValidator<ApproveSymbolShareHolderRequest>
{
    public ApproveSymbolShareHolderRequestValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.ShareHolderIsin).NotEmpty();
    }
}