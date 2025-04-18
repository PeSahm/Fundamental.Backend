using Fundamental.Application.Codals.Services;
using Fundamental.Application.Codals.Services.Models.MarketDataServiceModels;
using Fundamental.Application.Symbols.Specifications;
using Fundamental.Domain.Repositories.Base;
using Fundamental.Domain.Symbols.Entities;
using Fundamental.ErrorHandling;
using MediatR;

namespace Fundamental.Application.Symbols.Jobs.UpdateSymbolData;

public sealed class UpdateSymbolDataCommandHandler(
    IRepository repository,
    IMarketDataService marketDataService,
    IUnitOfWork unitOfWork
) : IRequestHandler<UpdateSymbolDataCommand, Response>
{
    public async Task<Response> Handle(UpdateSymbolDataCommand request, CancellationToken cancellationToken)
    {
        List<SymbolResponse> symbols = await marketDataService.GetSymbolsAsync(cancellationToken);

        foreach (SymbolResponse symbol in symbols)
        {
            if (symbol.SymbolCustomExtension.ProductType is null)
            {
                continue;
            }

            if (await repository.AnyAsync(new SymbolSpec().WhereIsin(symbol.Isin), cancellationToken))
            {
                Symbol? theSymbol = await repository.FirstOrDefaultAsync(new SymbolSpec().WhereIsin(symbol.Isin), cancellationToken);

                theSymbol?.Update(
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
                    symbol.SymbolCustomExtension.CustomExchangeType,
                    symbol.SymbolCustomExtension.EtfType,
                    DateTime.Now
                );

                continue;
            }

            Symbol newSymbol = new(
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
                symbol.SymbolCustomExtension.CustomExchangeType,
                symbol.SymbolCustomExtension.EtfType,
                DateTime.Now
            );
            repository.Add(newSymbol);
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Response.Successful();
    }
}