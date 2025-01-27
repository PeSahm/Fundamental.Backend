using Fundamental.Application.Symbols.Specifications;
using Fundamental.Domain.Common.Enums;
using Fundamental.Domain.Repositories.Base;
using Fundamental.Domain.Symbols.Entities;
using Fundamental.ErrorHandling;
using MediatR;

namespace Fundamental.Application.Symbols.Commands.ApproveSymbolShareHolder;

public sealed class ApproveSymbolShareHolderCommandHandler(
    IUnitOfWork unitOfWork,
    IRepository repository
)
    : IRequestHandler<ApproveSymbolShareHolderRequest, Response>
{
    public async Task<Response> Handle(ApproveSymbolShareHolderRequest request, CancellationToken cancellationToken)
    {
        Symbol? symbol = await repository.FirstOrDefaultAsync(new SymbolSpec().WhereIsin(request.ShareHolderIsin), cancellationToken);

        if (symbol is null)
        {
            return ApproveSymbolShareHolderErrorCodes.ShareHolderIsinIsNotValid;
        }

        SymbolShareHolder? shareHolder =
            await repository.FirstOrDefaultAsync(SymbolShareHolderSpec.WhereId(request.Id), cancellationToken);

        if (shareHolder is null)
        {
            return ApproveSymbolShareHolderErrorCodes.IdIsNotValid;
        }

        shareHolder.SetShareHolderSymbol(symbol, DateTime.Now);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        List<SymbolShareHolder> allShareHolders = await repository
            .ListAsync(SymbolShareHolderSpec.WhereShareHolderName(shareHolder.ShareHolderName, ReviewStatus.Pending), cancellationToken);

        allShareHolders.ForEach(x => x.SetShareHolderSymbol(symbol, DateTime.Now));

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Response.Successful();
    }
}