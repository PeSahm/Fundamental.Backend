using FluentValidation;

namespace Fundamental.Application.Codals.Manufacturing.Commands.UpdateFinancialStatement;

public class UpdateFinancialStatementRequestValidator : AbstractValidator<UpdateFinancialStatementRequest>
{
    public UpdateFinancialStatementRequestValidator()
    {
        RuleFor(x => x.Id).NotEmpty()
            .NotEqual(Guid.Empty)
            .WithMessage("Id is required");
        RuleFor(x => x.Isin).NotEmpty();
        RuleFor(x => x.TraceNo).GreaterThan(0u);
        RuleFor(x => x.Uri).NotEmpty();
        RuleFor(x => x.FiscalYear).InclusiveBetween((ushort)1390, (ushort)1410);
        RuleFor(x => x.YearEndMonth).InclusiveBetween((ushort)1, (ushort)12);
        RuleFor(x => x.ReportMonth).InclusiveBetween((ushort)1, (ushort)12);
    }
}