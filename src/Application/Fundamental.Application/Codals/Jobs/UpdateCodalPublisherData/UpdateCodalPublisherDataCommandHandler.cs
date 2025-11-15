using Fundamental.Application.Codals.Jobs.Specifications;
using Fundamental.Application.Codals.Services;
using Fundamental.Application.Codals.Services.Models.CodelServiceModels;
using Fundamental.Application.Common.Extensions;
using Fundamental.Application.Symbols.Specifications;
using Fundamental.Domain.Codals;
using Fundamental.Domain.Common.Enums;
using Fundamental.Domain.Repositories.Base;
using Fundamental.Domain.Symbols.Entities;
using Fundamental.ErrorHandling;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Fundamental.Application.Codals.Jobs.UpdateCodalPublisherData;

public sealed class UpdateCodalPublisherDataCommandHandler(
    ICodalService codalService,
    ILogger<UpdateCodalPublisherDataCommandHandler> logger,
    IRepository repository,
    IUnitOfWork unitOfWork
) : IRequestHandler<UpdateCodalPublisherDataRequest, Response>
{
    public async Task<Response> Handle(UpdateCodalPublisherDataRequest request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Update Codal Publisher Data Job is starting");
        List<GetPublisherResponse> publishers = await codalService.GetPublishers(cancellationToken);

        // Step 1: Collect all ISINs and Names for batch queries
        List<string> isins = publishers
            .Where(p => !string.IsNullOrWhiteSpace(p.IsinCode))
            .Select(p => p.IsinCode)
            .Distinct()
            .ToList();

        List<string> symbolNames = publishers
            .Select(p => p.DisplayedSymbol)
            .Distinct()
            .ToList();

        List<string> parentNames = publishers
            .Where(p => !string.IsNullOrWhiteSpace(p.Parent))
            .Select(p => p.Parent)
            .Distinct()
            .ToList();

        List<string> codalIds = publishers
            .Select(p => p.Id)
            .Distinct()
            .ToList();

        // Step 2: Batch load all symbols and publishers from database
        logger.LogInformation("Loading symbols by ISIN ({Count} items)", isins.Count);
        List<Symbol> symbolsByIsin = isins.Count > 0
            ? await repository.ListAsync(new SymbolSpec().WhereIsinsIn(isins), cancellationToken)
            : new List<Symbol>();

        logger.LogInformation("Loading symbols by Name ({Count} items)", symbolNames.Count);
        List<Symbol> symbolsByName = symbolNames.Count > 0
            ? await repository.ListAsync(new SymbolSpec().WhereNamesIn(symbolNames), cancellationToken)
            : new List<Symbol>();

        logger.LogInformation("Loading parent symbols ({Count} items)", parentNames.Count);
        List<Symbol> parentSymbols = parentNames.Count > 0
            ? await repository.ListAsync(new SymbolSpec().WhereNamesIn(parentNames), cancellationToken)
            : new List<Symbol>();

        logger.LogInformation("Loading existing publishers ({Count} items)", codalIds.Count);
        List<Publisher> existingPublishers = codalIds.Count > 0
            ? await repository.ListAsync(PublisherSpec.WhereCodalIdsIn(codalIds), cancellationToken)
            : new List<Publisher>();

        // Step 3: Create lookup dictionaries for fast access
        Dictionary<string, Symbol> isinLookup = symbolsByIsin.ToDictionary(s => s.Isin, StringComparer.OrdinalIgnoreCase);
        Dictionary<string, Symbol> nameLookup = symbolsByName
            .Where(s => !string.IsNullOrEmpty(s.Isin) && !isinLookup.ContainsKey(s.Isin))
            .GroupBy(s => s.Name, StringComparer.OrdinalIgnoreCase)
            .ToDictionary(g => g.Key, g => g.First(), StringComparer.OrdinalIgnoreCase);
        Dictionary<string, Symbol> parentLookup = parentSymbols
            .GroupBy(s => s.Name, StringComparer.OrdinalIgnoreCase)
            .ToDictionary(g => g.Key, g => g.First(), StringComparer.OrdinalIgnoreCase);
        Dictionary<string, Publisher> publisherLookup = existingPublishers.ToDictionary(p => p.CodalId, StringComparer.OrdinalIgnoreCase);

        // Step 4: Process publishers in-memory
        List<Symbol> newSymbols = new();
        List<Publisher> newPublishers = new();

        foreach (GetPublisherResponse publisher in publishers)
        {
            Symbol? symbol = null;
            Symbol? parentSymbol = null;

            // Find symbol by ISIN first, then by name
            if (!string.IsNullOrWhiteSpace(publisher.IsinCode) && isinLookup.TryGetValue(publisher.IsinCode, out Symbol? foundByIsin))
            {
                symbol = foundByIsin;
            }
            else if (nameLookup.TryGetValue(publisher.DisplayedSymbol, out Symbol? foundByName))
            {
                symbol = foundByName;
            }

            // Find parent symbol
            if (!string.IsNullOrWhiteSpace(publisher.Parent) && parentLookup.TryGetValue(publisher.Parent, out Symbol? foundParent))
            {
                parentSymbol = foundParent;
            }

            if (symbol is null && parentSymbol is null)
            {
                logger.LogDebug("Symbol and Parent Symbol are null for publisher {@Publisher}", publisher);
                continue;
            }

            // Create new symbol if needed
            if (symbol is null && parentSymbol is not null)
            {
                symbol = Symbol.CreateByParentSymbol(parentSymbol, publisher.DisplayedSymbol, publisher.Name, DateTime.Now);
                newSymbols.Add(symbol);
                nameLookup[symbol.Name] = symbol; // Add to lookup for subsequent publishers
            }

            if (symbol is null)
            {
                continue;
            }

            // Update or create publisher
            if (publisherLookup.TryGetValue(publisher.Id, out Publisher? existingPublisher))
            {
                UpdatePublisherProperties(existingPublisher, publisher);
            }
            else
            {
                Publisher newPublisher = CreatePublisher(publisher, symbol, parentSymbol);
                newPublishers.Add(newPublisher);
            }
        }

        // Step 5: Save all changes in a single transaction
        if (newSymbols.Count > 0)
        {
            logger.LogInformation("Adding {Count} new symbols", newSymbols.Count);
            repository.AddRange(newSymbols);
        }

        if (newPublishers.Count > 0)
        {
            logger.LogInformation("Adding {Count} new publishers", newPublishers.Count);
            repository.AddRange(newPublishers);
        }

        logger.LogInformation("Saving all changes to database");
        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation(
            "Update Codal Publisher Data Job completed. New Symbols: {NewSymbols}, New Publishers: {NewPublishers}, Updated Publishers: {UpdatedPublishers}",
            newSymbols.Count,
            newPublishers.Count,
            existingPublishers.Count);

        return Response.Successful();
    }

    private static void UpdatePublisherProperties(
        Publisher thePublisher,
        GetPublisherResponse publisher
    )
    {
        thePublisher.CodalId = publisher.Id;
        thePublisher.Isic = publisher.Isic;
        thePublisher.ReportingType = (ReportingType)publisher.ReportingType;
        thePublisher.CompanyType = (CompanyType)publisher.CompanyType;
        thePublisher.ExecutiveManager = publisher.ExecutiveManager.Safe();
        thePublisher.Address = publisher.Address.Safe();
        thePublisher.TelNo = publisher.TelNo.Safe();
        thePublisher.FaxNo = publisher.FaxNo.Safe();
        thePublisher.ActivitySubject = publisher.ActivitySubject.Safe();
        thePublisher.OfficeAddress = publisher.OfficeAddress.Safe();
        thePublisher.ShareOfficeAddress = publisher.ShareOfficeAddress.Safe();
        thePublisher.Website = publisher.Website;
        thePublisher.Email = publisher.Email;
        thePublisher.State = (PublisherState)publisher.State;
        thePublisher.Inspector = publisher.Inspector.Safe();
        thePublisher.FinancialManager = publisher.FinancialManager.Safe();
        thePublisher.FactoryTel = publisher.FactoryTel.Safe();
        thePublisher.FactoryFax = publisher.FactoryFax.Safe();
        thePublisher.OfficeTel = publisher.OfficeTel.Safe();
        thePublisher.OfficeFax = publisher.OfficeFax.Safe();
        thePublisher.ShareOfficeTel = publisher.ShareOfficeTel.Safe();
        thePublisher.ShareOfficeFax = publisher.ShareOfficeFax.Safe();
        thePublisher.NationalCode = TruncateNationalCode(publisher.NationalCode);
        thePublisher.FinancialYear = publisher.FinancialYear.Safe();
        thePublisher.ListedCapital = publisher.ListedCapital;
        thePublisher.AuditorName = publisher.AuditorName.Safe();
        thePublisher.IsEnableSubCompany = (EnableSubCompany)publisher.IsEnableSubCompany;
        thePublisher.IsEnabled = publisher.IsEnabled;
        thePublisher.FundType = (PublisherFundType)publisher.FundType;
        thePublisher.SubCompanyType = (PublisherSubCompanyType)publisher.SubCompanyType;
        thePublisher.IsSupplied = publisher.IsSupplied;
        thePublisher.MarketType = (PublisherMarketType)publisher.MarketType;
        thePublisher.UnauthorizedCapital = publisher.UnauthorizedCapital;
        thePublisher.UpdateLog();
    }

    private static Publisher CreatePublisher(GetPublisherResponse publisher, Symbol symbol, Symbol? parentSymbol)
    {
        return new Publisher(Guid.NewGuid(), symbol, DateTime.UtcNow)
        {
            CodalId = publisher.Id,
            Isic = publisher.Isic,
            ReportingType = (ReportingType)publisher.ReportingType,
            CompanyType = (CompanyType)publisher.CompanyType,
            ExecutiveManager = publisher.ExecutiveManager.Safe(),
            Address = publisher.Address.Safe(),
            TelNo = publisher.TelNo.Safe(),
            FaxNo = publisher.FaxNo.Safe(),
            ActivitySubject = publisher.ActivitySubject.Safe(),
            OfficeAddress = publisher.OfficeAddress.Safe(),
            ShareOfficeAddress = publisher.ShareOfficeAddress.Safe(),
            Website = publisher.Website,
            Email = publisher.Email,
            State = (PublisherState)publisher.State,
            Inspector = publisher.Inspector.Safe(),
            FinancialManager = publisher.FinancialManager.Safe(),
            FactoryTel = publisher.FactoryTel.Safe(),
            FactoryFax = publisher.FactoryFax.Safe(),
            OfficeTel = publisher.OfficeTel.Safe(),
            OfficeFax = publisher.OfficeFax.Safe(),
            ShareOfficeTel = publisher.ShareOfficeTel.Safe(),
            ShareOfficeFax = publisher.ShareOfficeFax.Safe(),
            NationalCode = TruncateNationalCode(publisher.NationalCode),
            FinancialYear = publisher.FinancialYear.Safe(),
            ListedCapital = publisher.ListedCapital,
            AuditorName = publisher.AuditorName.Safe(),
            IsEnableSubCompany = (EnableSubCompany)publisher.IsEnableSubCompany,
            IsEnabled = publisher.IsEnabled,
            FundType = (PublisherFundType)publisher.FundType,
            SubCompanyType = (PublisherSubCompanyType)publisher.SubCompanyType,
            IsSupplied = publisher.IsSupplied,
            MarketType = (PublisherMarketType)publisher.MarketType,
            UnauthorizedCapital = publisher.UnauthorizedCapital,
            ParentSymbol = parentSymbol
        };
    }

    private static string TruncateNationalCode(string? nationalCode)
    {
        if (string.IsNullOrEmpty(nationalCode))
        {
            return string.Empty;
        }

        string? safe = nationalCode.Safe();

        if (string.IsNullOrEmpty(safe))
        {
            return string.Empty;
        }

        return safe.Length > 15 ? safe.Substring(0, 15) : safe;
    }
}