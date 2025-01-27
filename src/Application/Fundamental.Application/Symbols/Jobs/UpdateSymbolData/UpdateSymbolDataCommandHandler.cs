using Fundamental.Application.Codals.Services;
using Fundamental.Application.Codals.Services.Models.MarketDataServiceModels;
using Fundamental.Application.Symbols.Specifications;
using Fundamental.Domain.Repositories.Base;
using Fundamental.Domain.Symbols.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Fundamental.Application.Symbols.Jobs.UpdateSymbolData;

public sealed class UpdateSymbolDataCommandHandler(
    IRepository repository,
    IMarketDataService marketDataService,
    IUnitOfWork unitOfWork,
    ILogger<UpdateSymbolDataCommandHandler> logger
) : IRequestHandler<UpdateSymbolDataCommand>
{
    public async Task Handle(UpdateSymbolDataCommand request, CancellationToken cancellationToken)
    {
        List<SymbolResponse> symbols = await marketDataService.GetSymbolsAsync(cancellationToken);

        foreach (SymbolResponse symbol in symbols)
        {
            if (symbol.SymbolCustomExtension.ProductType is null)
            {
                logger.LogWarning("ProductType is null for symbol {Symbol}", symbol.Isin);
                continue;
            }

            if (await repository.AnyAsync(new SymbolSpec().WhereIsin(symbol.Isin), cancellationToken))
            {
                Symbol? theSymbol = await repository.FirstOrDefaultAsync(new SymbolSpec().WhereIsin(symbol.Isin), cancellationToken);
                theSymbol!.Update(
                    symbol.TseInsCode,
                    symbol.EnName,
                    symbol.SymbolEnName,
                    symbol.Title,
                    symbol.Name,
                    symbol.CompanyEnCode,
                    symbol.CompanyPersianName,
                    symbol.CompanyIsin,
                    symbol.MarketCap,
                    symbol.SectorCode,
                    symbol.SubSectorCode,
                    symbol.SymbolCustomExtension.ProductType.Value,
                    DateTime.Now
                );
                continue;
            }

            Symbol newSymbol = new Symbol(
                Guid.NewGuid(),
                symbol.Isin,
                symbol.TseInsCode,
                symbol.EnName,
                symbol.SymbolEnName,
                symbol.Title,
                symbol.Name,
                symbol.CompanyEnCode,
                symbol.CompanyPersianName,
                symbol.CompanyIsin,
                symbol.MarketCap,
                symbol.SectorCode,
                symbol.SubSectorCode,
                symbol.SymbolCustomExtension.ProductType.Value,
                DateTime.Now
            );
            repository.Add(newSymbol);
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}