using Fundamental.Application.Codals.Manufacturing.Specifications;
using Fundamental.Domain.Codals.Manufacturing.Entities;
using Fundamental.Domain.Common.Enums;
using Fundamental.Domain.Repositories.Base;
using Fundamental.ErrorHandling;
using MediatR;

namespace Fundamental.Application.Codals.Manufacturing.Commands.RejectStatusOfViableCompany;

public sealed class RejectStatusOfViableCompanyHandler(IRepository repository, IUnitOfWork unitOfWork)
    : IRequestHandler<RejectStatusOfViableCompanyRequest, Response>
{
    public async Task<Response> Handle(RejectStatusOfViableCompanyRequest request, CancellationToken cancellationToken)
    {
        StockOwnership? symbolShareHolder = await repository
            .FirstOrDefaultAsync(new StockOwnershipSpec().WhereId(request.Id), cancellationToken);

        if (symbolShareHolder is null)
        {
            return RejectStatusOfViableCompanyErrorCodes.NotFound;
        }

        symbolShareHolder.Reject(DateTime.Now);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        List<StockOwnership> allShareHolders = await repository
            .ListAsync(
                new StockOwnershipSpec().WhereSubsidiarySymbolName(symbolShareHolder.SubsidiarySymbolName)
                    .WhereReviewStatus(ReviewStatus.Pending),
                cancellationToken);

        allShareHolders.ForEach(x => x.Reject(DateTime.Now));

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Response.Successful();
    }
}