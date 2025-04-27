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

        foreach (GetPublisherResponse publisher in publishers)
        {
            Symbol? symbol = null;
            Symbol? parentSymbol = null;

            if (!string.IsNullOrWhiteSpace(publisher.IsinCode))
            {
                symbol = await repository.FirstOrDefaultAsync(new SymbolSpec().WhereIsin(publisher.IsinCode), cancellationToken);
            }

            if (symbol is null)
            {
                symbol = await repository.FirstOrDefaultAsync(
                    new SymbolSpec().WhereName(publisher.DisplayedSymbol),
                    cancellationToken);
            }

            if (!string.IsNullOrWhiteSpace(publisher.Parent))
            {
                parentSymbol =
                    await repository.FirstOrDefaultAsync(new SymbolSpec().WhereName(publisher.Parent), cancellationToken);
            }

            if (symbol is null && parentSymbol is null)
            {
                logger.LogDebug("Symbol and Parent Symbol are null for publisher {@Publisher}", publisher);
                continue;
            }

            if (symbol is null)
            {
                symbol = Symbol.CreateByParentSymbol(parentSymbol!, publisher.DisplayedSymbol, publisher.Name, DateTime.Now);
                repository.Add(symbol);
                await unitOfWork.SaveChangesAsync(cancellationToken);
            }

            if (await repository.AnyAsync(PublisherSpec.WithCodalId(publisher.Id), cancellationToken))
            {
                Publisher? thePublisher =
                    await repository.FirstOrDefaultAsync(PublisherSpec.WithCodalId(publisher.Id), cancellationToken);

                if (thePublisher == null)
                {
                    continue;
                }

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
                thePublisher.NationalCode = publisher.NationalCode.Safe();
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
                thePublisher.ParentSymbol = parentSymbol;
                thePublisher.UpdateLog();

                if (symbol.Isin != thePublisher?.Symbol.Isin)
                {
                    thePublisher!.Update(symbol);
                }
            }
            else
            {
                Publisher entity = new(Guid.NewGuid(), symbol, DateTime.Now)
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
                    NationalCode = publisher.NationalCode.Safe(),
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
                repository.Add(entity);
            }

            await unitOfWork.SaveChangesAsync(cancellationToken);
        }

        return Response.Successful();
    }
}