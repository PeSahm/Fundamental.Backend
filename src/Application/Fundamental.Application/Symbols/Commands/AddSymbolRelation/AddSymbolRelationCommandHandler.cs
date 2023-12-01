using Fundamental.Application.Symbols.Specifications;
using Fundamental.Domain.Repositories.Base;
using Fundamental.Domain.Symbols.Entities;
using Fundamental.ErrorHandling;
using MediatR;

namespace Fundamental.Application.Symbols.Commands.AddSymbolRelation;

public sealed class AddSymbolRelationCommandHandler : IRequestHandler<AddSymbolRelationRequest, Response>
{
    private readonly IRepository<SymbolRelation> _symbolRelationRepository;
    private readonly IRepository<Symbol> _symbolRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddSymbolRelationCommandHandler(
        IRepository<SymbolRelation> symbolRelationRepository,
        IUnitOfWork unitOfWork,
        IRepository<Symbol> symbolRepository
    )
    {
        _symbolRelationRepository = symbolRelationRepository;
        _unitOfWork = unitOfWork;
        _symbolRepository = symbolRepository;
    }

    public async Task<Response> Handle(AddSymbolRelationRequest request, CancellationToken cancellationToken)
    {
        Symbol? investor = await _symbolRepository.FirstOrDefaultAsync(
            new SymbolSpec()
                .WhereIsin(request.Investor),
            cancellationToken);

        if (investor is null)
        {
            return AddSymbolRelationRequestErrorCodes.InvestorIsInvalid;
        }

        Symbol? investment = await _symbolRepository.FirstOrDefaultAsync(
            new SymbolSpec()
                .WhereIsin(request.Investment),
            cancellationToken);

        if (investment is null)
        {
            return AddSymbolRelationRequestErrorCodes.InvestmentIsInvalid;
        }

        bool hasRelation = await _symbolRelationRepository.AnyAsync(
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

        SymbolRelation symbolRelation = new SymbolRelation(Guid.NewGuid(), investor, investment, request.Ratio, DateTime.Now);
        _symbolRelationRepository.Add(symbolRelation);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Response.Successful();
    }
}