using Fundamental.Application.Symbols.Specifications;
using Fundamental.Domain.Common.Enums;
using Fundamental.Domain.Repositories.Base;
using Fundamental.Domain.Symbols.Entities;
using Fundamental.ErrorHandling;
using MediatR;

namespace Fundamental.Application.Symbols.Commands.RejectSymbolShareHolder;

public sealed class RejectSymbolShareHolderCommandHandler(
    IRepository<SymbolShareHolder> symbolShareHolderRepository,
    IUnitOfWork unitOfWork
) : IRequestHandler<RejectSymbolShareHolderRequest, Response>
{
    public async Task<Response> Handle(RejectSymbolShareHolderRequest request, CancellationToken cancellationToken)
    {
        SymbolShareHolder? symbolShareHolder = await symbolShareHolderRepository
            .FirstOrDefaultAsync(SymbolShareHolderSpec.WhereId(request.Id), cancellationToken);

        if (symbolShareHolder is null)
        {
            return RejectSymbolShareHolderErrorCodes.NotFound;
        }

        symbolShareHolder.ChangeReviewStatus(ReviewStatus.Rejected, DateTime.Now);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        List<SymbolShareHolder> allShareHolders = await symbolShareHolderRepository
            .ListAsync(SymbolShareHolderSpec.WhereShareHolderName(symbolShareHolder.ShareHolderName, ReviewStatus.Pending),
                cancellationToken);

        allShareHolders.ForEach(x => x.ChangeReviewStatus(ReviewStatus.Rejected, DateTime.Now));

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Response.Successful();
    }
}