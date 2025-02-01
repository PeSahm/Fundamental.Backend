using DNTPersianUtils.Core;
using Fundamental.Application.Codals.Services;
using Fundamental.Application.Codals.Services.Models.MarketDataServiceModels;
using Fundamental.Application.Symbols.Models;
using Fundamental.Application.Symbols.Specifications;
using Fundamental.Domain.Repositories.Base;
using Fundamental.Domain.Symbols.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using Index = Fundamental.Domain.Symbols.Entities.Index;

namespace Fundamental.Application.Symbols.Jobs.UpdateIndexData;

public sealed class UpdateIndexDataCommandHandler(
    IRepository repository,
    IMarketDataService marketDataService,
    IUnitOfWork unitOfWork,
    ILogger<UpdateIndexDataCommandHandler> logger
) : IRequestHandler<UpdateIndexDataCommand>
{
    public async Task Handle(UpdateIndexDataCommand request, CancellationToken cancellationToken)
    {
        DateOnly fromDate = "1380/01/01".ToGregorianDateOnly() ?? DateTime.Now.ToDateOnly();

        List<SimpleIndex> simpleIndices = await repository.ListAsync(new SimpleIndexSpec(), cancellationToken);

        while (fromDate < DateTime.Now.Date.ToDateOnly())
        {
            List<IndexResponse> indices = await marketDataService.GetIndicesAsync(fromDate, cancellationToken);

            foreach (IndexResponse index in indices)
            {
                Symbol? indexSymbol = await repository.FirstOrDefaultAsync(
                    new SymbolSpec()
                        .AsNoTracking()
                        .WhereIsin(index.Isin, true),
                    cancellationToken);

                if (indexSymbol is null)
                {
                    logger.LogWarning("Index symbol not found for {Isin}", index.Isin);
                    continue;
                }

                if (simpleIndices.Exists(x => x.Isin == index.Isin && x.Date == fromDate))
                {
                    Index? existingIndex = await repository.FirstOrDefaultAsync(
                        new IndexSpec().WhereIsin(index.Isin)
                            .WhereDate(fromDate),
                        cancellationToken);

                    if (existingIndex is null)
                    {
                        continue;
                    }

                    existingIndex.UpdateIndex(index.Open, index.High, index.Low, index.Value);
                }
                else
                {
                    Index newIndex = new(
                        Guid.NewGuid(),
                        indexSymbol,
                        fromDate,
                        index.Open,
                        index.High,
                        index.Low,
                        index.Value,
                        DateTime.Now
                    );

                    repository.Add(newIndex);
                }
            }

            fromDate = fromDate.AddDays(1);
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}