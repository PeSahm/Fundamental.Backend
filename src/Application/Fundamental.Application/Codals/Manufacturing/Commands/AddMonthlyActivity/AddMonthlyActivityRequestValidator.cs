using FluentValidation;

namespace Fundamental.Application.Codals.Manufacturing.Commands.AddMonthlyActivity;

public sealed class AddMonthlyActivityRequestValidator : AbstractValidator<AddMonthlyActivityRequest>
{
    public AddMonthlyActivityRequestValidator()
    {
        RuleFor(x => x.Isin).NotEmpty();
        RuleFor(x => x.TraceNo).GreaterThan(0u);
        RuleFor(x => x.Uri).NotEmpty();
        RuleFor(x => x.FiscalYear).InclusiveBetween((ushort)1390, (ushort)1410);
        RuleFor(x => x.YearEndMonth).InclusiveBetween((ushort)1, (ushort)12);
        RuleFor(x => x.ReportMonth).InclusiveBetween((ushort)1, (ushort)12);
    }
}