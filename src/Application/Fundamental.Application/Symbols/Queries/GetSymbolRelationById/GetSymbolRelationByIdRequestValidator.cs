using FluentValidation;

namespace Fundamental.Application.Symbols.Queries.GetSymbolRelationById;

public sealed class GetSymbolRelationByIdRequestValidator : AbstractValidator<GetSymbolRelationByIdRequest>
{
    public GetSymbolRelationByIdRequestValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
    }
}