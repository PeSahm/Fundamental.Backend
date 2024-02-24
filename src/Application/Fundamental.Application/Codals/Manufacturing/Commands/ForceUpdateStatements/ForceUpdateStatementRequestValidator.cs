using FluentValidation;

namespace Fundamental.Application.Codals.Manufacturing.Commands.ForceUpdateStatements;

public sealed class ForceUpdateStatementRequestValidator : AbstractValidator<ForceUpdateStatementRequest>
{
    public ForceUpdateStatementRequestValidator()
    {
        RuleFor(x => x.TraceNo)
            .GreaterThan(0U);
    }
}