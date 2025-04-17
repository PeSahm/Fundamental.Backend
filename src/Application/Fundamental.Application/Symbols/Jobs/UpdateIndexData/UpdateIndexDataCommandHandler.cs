using DNTPersianUtils.Core;
using Fundamental.Application.Codals.Services;
using Fundamental.Application.Codals.Services.Models.MarketDataServiceModels;
using Fundamental.Application.Symbols.Models;
using Fundamental.Application.Symbols.Specifications;
using Fundamental.Domain.Repositories.Base;
using Fundamental.Domain.Symbols.Entities;
using Fundamental.Domain.Symbols.Enums;
using Fundamental.ErrorHandling;
using MediatR;
using Microsoft.Extensions.Logging;
using Index = Fundamental.Domain.Symbols.Entities.Index;

namespace Fundamental.Application.Symbols.Jobs.UpdateIndexData;

public sealed class UpdateIndexDataCommandHandler(
    IRepository repository,
    IMarketDataService marketDataService,
    IUnitOfWork unitOfWork,
    ILogger<UpdateIndexDataCommandHandler> logger
) : IRequestHandler<UpdateIndexDataCommand, Response>
{
    public async Task<Response> Handle(UpdateIndexDataCommand request, CancellationToken cancellationToken)
    {
        DateOnly fromDate = DateTime.Now.AddDays(-1 * request.Days).ToDateOnly();

        List<SimpleIndex> simpleIndices = await repository.ListAsync(new SimpleIndexSpec(), cancellationToken);
        List<Symbol> symbols = await repository.ListAsync(
            new SymbolSpec().WhereProductType(ProductType.Index),
            cancellationToken);

        IndexResponse indices = await marketDataService.GetIndicesAsync(fromDate, cancellationToken);

        foreach (IndexResponseItem index in indices.Result)
        {
            Symbol? indexSymbol = symbols.FirstOrDefault(x => x.Isin == index.Isin);

            if (indexSymbol is null)
            {
                continue;
            }

            if (simpleIndices.Exists(x => x.Isin == index.Isin && x.Date == index.Date.ToDateOnly()))
            {
                Index? existingIndex = await repository.FirstOrDefaultAsync(
                    new IndexSpec().WhereIsin(index.Isin)
                        .WhereDate(index.Date.ToDateOnly()),
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
                    index.Date.ToDateOnly(),
                    index.Open,
                    index.High,
                    index.Low,
                    index.Value,
                    DateTime.Now
                );

                repository.Add(newIndex);
            }
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Response.Successful();
    }
}