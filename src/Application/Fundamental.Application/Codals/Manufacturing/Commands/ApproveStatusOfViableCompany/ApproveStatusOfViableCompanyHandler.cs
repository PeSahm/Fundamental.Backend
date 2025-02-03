using Fundamental.Application.Codals.Manufacturing.Specifications;
using Fundamental.Application.Symbols.Specifications;
using Fundamental.Domain.Codals.Manufacturing.Entities;
using Fundamental.Domain.Common.Enums;
using Fundamental.Domain.Repositories.Base;
using Fundamental.Domain.Symbols.Entities;
using Fundamental.ErrorHandling;
using MediatR;

namespace Fundamental.Application.Codals.Manufacturing.Commands.ApproveStatusOfViableCompany;

public sealed class ApproveStatusOfViableCompanyHandler(IRepository repository, IUnitOfWork unitOfWork)
    : IRequestHandler<ApproveStatusOfViableCompanyRequest, Response>
{
    public async Task<Response> Handle(ApproveStatusOfViableCompanyRequest request, CancellationToken cancellationToken)
    {
        Symbol? symbol = await repository.FirstOrDefaultAsync(new SymbolSpec().WhereIsin(request.SubsidiaryIsin), cancellationToken);

        if (symbol is null)
        {
            return ApproveStatusOfViableCompanyErrorCodes.SubsidiaryIsinIsNotValid;
        }

        StockOwnership? ownership =
            await repository.FirstOrDefaultAsync(new StockOwnershipSpec().WhereId(request.Id), cancellationToken);

        if (ownership is null)
        {
            return ApproveStatusOfViableCompanyErrorCodes.IdIsNotValid;
        }

        if (request.Percentage is > 100 or <= 0)
        {
            return ApproveStatusOfViableCompanyErrorCodes.InvalidPercentage;
        }

        ownership.SetSubsidiarySymbol(symbol, DateTime.Now)
            .SetOwnershipPercentageProvidedByAdmin(request.Percentage, DateTime.Now);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        List<StockOwnership> allShareHolders = await repository
            .ListAsync(
                new StockOwnershipSpec()
                    .WhereSubsidiarySymbolName(ownership.SubsidiarySymbolName)
                    .WhereReviewStatus(ReviewStatus.Pending),
                cancellationToken);

        allShareHolders.ForEach(x => x.SetSubsidiarySymbol(symbol, DateTime.Now));

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Response.Successful();
    }
}