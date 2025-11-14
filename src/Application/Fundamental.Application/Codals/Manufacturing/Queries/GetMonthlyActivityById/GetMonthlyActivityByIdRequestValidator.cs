using FluentValidation;

namespace Fundamental.Application.Codals.Manufacturing.Queries.GetMonthlyActivityById;

public sealed class GetMonthlyActivityByIdRequestValidator : AbstractValidator<GetMonthlyActivityByIdRequest>
{
    public GetMonthlyActivityByIdRequestValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}