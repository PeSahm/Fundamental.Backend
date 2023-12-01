using FluentValidation;

namespace Fundamental.Application.Statements.Queries.GetFinancialStatementById;

public sealed class GetFinancialStatementByIdRequestValidator : AbstractValidator<GetFinancialStatementByIdRequest>
{
    public GetFinancialStatementByIdRequestValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}