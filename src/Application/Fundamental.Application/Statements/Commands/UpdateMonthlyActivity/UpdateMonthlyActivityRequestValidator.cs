using FluentValidation;

namespace Fundamental.Application.Statements.Commands.UpdateMonthlyActivity;

public sealed class UpdateMonthlyActivityRequestValidator : AbstractValidator<UpdateMonthlyActivityRequest>
{
    public UpdateMonthlyActivityRequestValidator()
    {
        RuleFor(x => x.Id).NotEmpty()
            .NotEqual(Guid.Empty)
            .WithMessage("Id is required.");

        RuleFor(x => x.Isin).NotEmpty();
        RuleFor(x => x.Isin).NotEmpty();
        RuleFor(x => x.TraceNo).GreaterThan(0u);
        RuleFor(x => x.Uri).NotEmpty();
        RuleFor(x => x.FiscalYear).InclusiveBetween((ushort)1390, (ushort)1410);
        RuleFor(x => x.YearEndMonth).InclusiveBetween((ushort)1, (ushort)12);
        RuleFor(x => x.ReportMonth).InclusiveBetween((ushort)1, (ushort)12);
    }
}