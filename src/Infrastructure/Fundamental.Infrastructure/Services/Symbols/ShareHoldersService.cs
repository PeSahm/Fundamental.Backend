using Fundamental.Application.Codals.Services.Models.MarketDataServiceModels;
using Fundamental.Application.Common.Extensions;
using Fundamental.Application.Symbols;
using Fundamental.Application.Symbols.Specifications;
using Fundamental.Domain.Common.Enums;
using Fundamental.Domain.Repositories.Base;
using Fundamental.Domain.Symbols.Entities;

namespace Fundamental.Infrastructure.Services.Symbols;

public class ShareHoldersService(
    IRepository<SymbolShareHolder> repository,
    IRepository<Symbol> symbolRepository,
    IUnitOfWork unitOfWork
) : IShareHoldersService
{
    public async Task CreateShareHolders(
        List<ShareHoldersResponse> shareHolders,
        CancellationToken cancellationToken = default
    )
    {
        shareHolders = shareHolders.GroupBy(x => new { x.Isin, ShareHolderName = x.ShareHolderName.Safe() })
            .Where(x => x.Count() == 1)
            .Select(x => x.First()).ToList();

        foreach (IGrouping<string, ShareHoldersResponse> shareHoldersResponses in shareHolders.GroupBy(x => x.Isin))
        {
            List<SymbolShareHolder> currentShareHolders =
                await repository.ListAsync(
                    SymbolShareHolderSpec.WhereIsin(
                        shareHoldersResponses.Key),
                    cancellationToken);

            foreach (ShareHoldersResponse shareHolder in shareHoldersResponses)
            {
                if (currentShareHolders.Exists(x => x.ShareHolderName == shareHolder.ShareHolderName.Safe()))
                {
                    List<SymbolShareHolder> symbolShareHolders =
                        currentShareHolders.Where(x => x.ShareHolderName == shareHolder.ShareHolderName.Safe()).ToList();

                    if (symbolShareHolders.Count > 1)
                    {
                        continue;
                    }

                    SymbolShareHolder theShareHolder = symbolShareHolders[0];

                    if (theShareHolder.ReviewStatus == ReviewStatus.Rejected)
                    {
                        continue;
                    }

                    theShareHolder.ChangeSharePercentage(shareHolder.Percent, DateTime.Now);
                }
                else
                {
                    Symbol? symbol =
                        await symbolRepository.FirstOrDefaultAsync(new SymbolSpec().WhereIsin(shareHolder.Isin), cancellationToken);

                    if (symbol is null)
                    {
                        continue;
                    }

                    SymbolShareHolder symbolShareHolder = new(
                        Guid.NewGuid(),
                        symbol,
                        shareHolder.ShareHolderName.Safe()!,
                        shareHolder.Percent,
                        DateTime.Now
                    );
                    repository.Add(symbolShareHolder);
                }
            }

            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}