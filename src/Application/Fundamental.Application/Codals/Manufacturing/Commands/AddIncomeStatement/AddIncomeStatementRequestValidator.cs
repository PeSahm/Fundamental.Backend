using FluentValidation;
using Fundamental.Domain.Codals.Manufacturing.Entities;

namespace Fundamental.Application.Codals.Manufacturing.Commands.AddIncomeStatement;

public sealed class AddIncomeStatementRequestValidator : AbstractValidator<AddIncomeStatementRequest>
{
    public AddIncomeStatementRequestValidator()
    {
        RuleFor(x => x.Isin).NotEmpty();
        RuleFor(x => x.TraceNo).NotEmpty();
        RuleFor(x => x.Uri).NotEmpty();
        RuleFor(x => x.FiscalYear).NotEmpty();
        RuleFor(x => x.YearEndMonth).NotEmpty();
        RuleFor(x => x.ReportMonth).NotEmpty();
        RuleForEach(x => x.Items).ChildRules(item =>
        {
            item.RuleFor(x => x.CodalRow).NotEmpty()
                .Must(IncomeStatementSort.CheckCodalRow).WithMessage(x => $"{x.CodalRow} is not a valid income statement row");
            item.RuleFor(x => x.Value).GreaterThanOrEqualTo(0);
        });
    }
}