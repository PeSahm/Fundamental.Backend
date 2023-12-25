using FluentValidation;
using Fundamental.Domain.Codals.Manufacturing.Entities;

namespace Fundamental.Application.Codals.Manufacturing.Commands.AddBalanceSheet;

public sealed class AddBalanceSheetRequestValidator : AbstractValidator<AddBalanceSheetRequest>
{
    public AddBalanceSheetRequestValidator()
    {
        RuleFor(x => x.Isin).NotEmpty();
        RuleFor(x => x.TraceNo).NotEmpty();
        RuleFor(x => x.Uri).NotEmpty();
        RuleFor(x => x.FiscalYear).NotEmpty();
        RuleFor(x => x.YearEndMonth).NotEmpty();
        RuleFor(x => x.ReportMonth).NotEmpty();
        RuleForEach(x => x.Items).ChildRules(item =>
        {
            item.RuleFor(x => x.CodalCategory).IsInEnum();
            item.RuleFor(x => x.CodalRow).NotEmpty()
                .Must(BalanceSheetSort.CheckCodalRow).WithMessage(x => $"{x.CodalRow} is not a valid balanceSheet row");
            item.RuleFor(x => x.Value).GreaterThanOrEqualTo(0);
        });
    }
}