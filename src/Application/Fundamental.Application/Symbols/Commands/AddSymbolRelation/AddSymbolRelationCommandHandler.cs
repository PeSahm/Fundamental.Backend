using Fundamental.Application.Symbols.Specifications;
using Fundamental.Domain.Repositories.Base;
using Fundamental.Domain.Symbols.Entities;
using Fundamental.ErrorHandling;
using MediatR;

namespace Fundamental.Application.Symbols.Commands.AddSymbolRelation;

public sealed class AddSymbolRelationCommandHandler(
    IUnitOfWork unitOfWork,
    IRepository repository
)
    : IRequestHandler<AddSymbolRelationRequest, Response>
{
    public async Task<Response> Handle(AddSymbolRelationRequest request, CancellationToken cancellationToken)
    {
        Symbol? investor = await repository.FirstOrDefaultAsync(
            new SymbolSpec()
                .WhereIsin(request.Investor),
            cancellationToken);

        if (investor is null)
        {
            return AddSymbolRelationRequestErrorCodes.InvestorIsInvalid;
        }

        Symbol? investment = await repository.FirstOrDefaultAsync(
            new SymbolSpec()
                .WhereIsin(request.Investment),
            cancellationToken);

        if (investment is null)
        {
            return AddSymbolRelationRequestErrorCodes.InvestmentIsInvalid;
        }

        bool hasRelation = await repository.AnyAsync(
            new SymbolRelationSpec()
                .WhereParentIsin(request.Investor)
                .WhereChildIsin(request.Investment),
            cancellationToken);

        if (hasRelation)
        {
            return AddSymbolRelationRequestErrorCodes.RelationAlreadyExists;
        }

        if (request.Ratio <= 0)
        {
            return AddSymbolRelationRequestErrorCodes.RatioIsInvalid;
        }

        SymbolRelation symbolRelation = new(Guid.NewGuid(), investor, investment, request.Ratio, DateTime.Now);
        repository.Add(symbolRelation);

        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Response.Successful();
    }
}