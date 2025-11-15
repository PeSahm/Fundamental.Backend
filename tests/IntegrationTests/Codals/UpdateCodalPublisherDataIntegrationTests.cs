using FluentAssertions;
using Fundamental.Application.Codals.Jobs.UpdateCodalPublisherData;
using Fundamental.Application.Codals.Services.Models.CodelServiceModels;
using Fundamental.Domain.Codals;
using Fundamental.Domain.Common.Enums;
using Fundamental.Domain.Symbols.Entities;
using Fundamental.Domain.Symbols.Enums;
using Fundamental.ErrorHandling;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace IntegrationTests.Codals;

[Collection("UpdateCodalPublisherData")]
public class UpdateCodalPublisherDataIntegrationTests : IClassFixture<TestFixture>
{
    private readonly TestFixture _fixture;

    public UpdateCodalPublisherDataIntegrationTests(TestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task UpdateCodalPublisherData_ShouldCreateNewPublishersForExistingSymbols()
    {
        // Arrange
        await CleanupTestData();
        Symbol existingSymbol1 = CreateTestSymbol("IRO100000011", "Test Company");
        Symbol existingSymbol2 = CreateTestSymbol("IRO100000012", "New Company");
        _fixture.DbContext.Symbols.AddRange(existingSymbol1, existingSymbol2);
        await _fixture.DbContext.SaveChangesAsync();

        List<GetPublisherResponse> publishers = new()
        {
            CreatePublisherResponse("PUB011", "IRO100000011", "Test Company", null, true),
            CreatePublisherResponse("PUB012", "IRO100000012", "New Company", null, false)
        };

        _fixture.CodalServiceMock
            .Setup(x => x.GetPublishers(It.IsAny<CancellationToken>()))
            .ReturnsAsync(publishers);

        IMediator mediator = _fixture.Services.GetRequiredService<IMediator>();
        UpdateCodalPublisherDataRequest request = new();

        // Act
        Response response = await mediator.Send(request, CancellationToken.None);

        // Assert
        response.Success.Should().BeTrue();

        // Verify new publisher was created for existing symbol
        Publisher? publisher1 = await _fixture.DbContext.Publishers
            .Include(p => p.Symbol)
            .FirstOrDefaultAsync(p => p.CodalId == "PUB011");
        publisher1.Should().NotBeNull();
        publisher1.Symbol.Isin.Should().Be("IRO100000011");
        publisher1.IsEnabled.Should().BeTrue();

        // Verify new publisher with existing symbol was created
        Publisher? publisher2 = await _fixture.DbContext.Publishers
            .Include(p => p.Symbol)
            .FirstOrDefaultAsync(p => p.CodalId == "PUB012");
        publisher2.Should().NotBeNull();
        publisher2.Symbol.Isin.Should().Be("IRO100000012");
        publisher2.IsEnabled.Should().BeFalse();
    }

    [Fact]
    public async Task UpdateCodalPublisherData_ShouldUpdateExistingPublishers()
    {
        // Arrange
        await CleanupTestData();
        Symbol symbol = CreateTestSymbol("IRO100000023", "Update Test");
        _fixture.DbContext.Symbols.Add(symbol);

        Publisher existingPublisher = CreateTestPublisher("PUB023", symbol, "Old Address", "Old Manager");
        _fixture.DbContext.Publishers.Add(existingPublisher);
        await _fixture.DbContext.SaveChangesAsync();

        List<GetPublisherResponse> publishers = new()
        {
            CreatePublisherResponse(
                "PUB023",
                "IRO100000023",
                "Update Test",
                null,
                true,
                "New Address",
                "New Manager")
        };

        _fixture.CodalServiceMock
            .Setup(x => x.GetPublishers(It.IsAny<CancellationToken>()))
            .ReturnsAsync(publishers);

        IMediator mediator = _fixture.Services.GetRequiredService<IMediator>();
        UpdateCodalPublisherDataRequest request = new();

        // Act
        Response response = await mediator.Send(request, CancellationToken.None);

        // Assert
        response.Success.Should().BeTrue();

        Publisher? updatedPublisher = await _fixture.DbContext.Publishers
            .Include(p => p.Symbol)
            .FirstOrDefaultAsync(p => p.CodalId == "PUB023");
        updatedPublisher.Should().NotBeNull();
        updatedPublisher.Address.Should().Be("New Address");
        updatedPublisher.ExecutiveManager.Should().Be("New Manager");
        updatedPublisher.IsEnabled.Should().BeTrue();
    }

    [Fact]
    public async Task UpdateCodalPublisherData_ShouldCreateSymbolFromParentSymbol()
    {
        // Arrange
        await CleanupTestData();
        Symbol parentSymbol = CreateTestSymbol("IRO1000000", "Parent Company");
        _fixture.DbContext.Symbols.Add(parentSymbol);
        await _fixture.DbContext.SaveChangesAsync();

        List<GetPublisherResponse> publishers = new()
        {
            CreatePublisherResponse("PUB034", null, "Parent Company-Sub", "Parent Company", false)
        };

        _fixture.CodalServiceMock
            .Setup(x => x.GetPublishers(It.IsAny<CancellationToken>()))
            .ReturnsAsync(publishers);

        IMediator mediator = _fixture.Services.GetRequiredService<IMediator>();
        UpdateCodalPublisherDataRequest request = new();

        // Act
        Response response = await mediator.Send(request, CancellationToken.None);

        // Assert
        response.Success.Should().BeTrue();

        Publisher? publisher = await _fixture.DbContext.Publishers
            .Include(p => p.Symbol)
            .Include(p => p.ParentSymbol)
            .FirstOrDefaultAsync(p => p.CodalId == "PUB034");
        publisher.Should().NotBeNull();
        publisher.Symbol.Should().NotBeNull();
        publisher.Symbol.Name.Should().Be("Parent Company-Sub");
        publisher.ParentSymbol.Should().NotBeNull();
        publisher.ParentSymbol.Id.Should().Be(parentSymbol.Id);
    }

    [Fact]
    public async Task UpdateCodalPublisherData_ShouldHandleLargeDatasetEfficiently()
    {
        // Arrange
        await CleanupTestData();

        // Create 20 existing symbols
        List<Symbol> existingSymbols = new();
        for (int i = 1; i <= 20; i++)
        {
            Symbol symbol = CreateTestSymbol($"IRO{200 + i:D9}", $"Bulk Company {i}");
            existingSymbols.Add(symbol);
        }

        _fixture.DbContext.Symbols.AddRange(existingSymbols);
        await _fixture.DbContext.SaveChangesAsync();

        // Create 40 publishers (20 for existing symbols, 20 with parent symbols)
        Symbol parentSymbol = CreateTestSymbol("IRO100000", "Bulk Parent");
        _fixture.DbContext.Symbols.Add(parentSymbol);
        await _fixture.DbContext.SaveChangesAsync();

        List<GetPublisherResponse> publishers = new();
        for (int i = 1; i <= 20; i++)
        {
            publishers.Add(CreatePublisherResponse($"BULK{i + 40:D3}", $"IRO{200 + i:D9}", $"Bulk Company {i}", null, true));
        }

        for (int i = 21; i <= 40; i++)
        {
            publishers.Add(CreatePublisherResponse($"BULK{i + 40:D3}", null, $"Bulk ParentSub{i}", "Bulk Parent", false));
        }

        _fixture.CodalServiceMock
            .Setup(x => x.GetPublishers(It.IsAny<CancellationToken>()))
            .ReturnsAsync(publishers);

        IMediator mediator = _fixture.Services.GetRequiredService<IMediator>();
        UpdateCodalPublisherDataRequest request = new();

        // Act
        Response response = await mediator.Send(request, CancellationToken.None);

        // Assert
        response.Success.Should().BeTrue();

        int publisherCount = await _fixture.DbContext.Publishers
            .Where(p => p.CodalId.CompareTo("BULK041") >= 0)
            .CountAsync();
        publisherCount.Should().Be(40);

        // Verify symbols were created for publishers with parent symbols
        int newSymbolCount = await _fixture.DbContext.Symbols
            .Where(s => s.IsUnOfficial && s.Name.Contains("Sub"))
            .CountAsync();
        newSymbolCount.Should().BeGreaterThanOrEqualTo(20);
    }

    [Fact]
    public async Task UpdateCodalPublisherData_ShouldSkipPublishersWithNoSymbolAndNoParent()
    {
        // Arrange
        await CleanupTestData();
        List<GetPublisherResponse> publishers = new()
        {
            CreatePublisherResponse("PUB075", "IRO100000099", "Non-existent", null, false)
        };

        _fixture.CodalServiceMock
            .Setup(x => x.GetPublishers(It.IsAny<CancellationToken>()))
            .ReturnsAsync(publishers);

        IMediator mediator = _fixture.Services.GetRequiredService<IMediator>();
        UpdateCodalPublisherDataRequest request = new();

        // Act
        Response response = await mediator.Send(request, CancellationToken.None);

        // Assert
        response.Success.Should().BeTrue();

        Publisher? publisher = await _fixture.DbContext.Publishers
            .FirstOrDefaultAsync(p => p.CodalId == "PUB075");
        publisher.Should().BeNull();
    }

    private async Task CleanupTestData()
    {
        // Clean up publishers first (due to foreign key) - only test publishers
        await _fixture.DbContext.Publishers
            .Where(p => p.CodalId.StartsWith("PUB") || p.CodalId.StartsWith("BULK"))
            .ExecuteDeleteAsync();

        // Clean up test symbols - be more specific to avoid removing non-test data
        await _fixture.DbContext.Symbols
            .Where(s => s.Isin.StartsWith("IRO100000") || s.Isin.StartsWith("IRO0000001"))
            .ExecuteDeleteAsync();
    }

    private static Symbol CreateTestSymbol(string isin, string name)
    {
        int remainingLength = isin.Length - 3;
        string tseInsCode = isin.Substring(3, Math.Min(9, remainingLength));
        string symbolEnName = isin.Substring(3, Math.Min(6, remainingLength));
        string companyEnCode = isin.Substring(3, Math.Min(6, remainingLength));
        return new Symbol(
            Guid.NewGuid(),
            isin,
            tseInsCode,
            name + " EN",
            symbolEnName,
            name,
            name,
            companyEnCode,
            name + " Persian",
            null,
            1000000000,
            "01",
            "001",
            ProductType.Equity,
            ExchangeType.TSE,
            null,
            DateTime.UtcNow);
    }

    private Publisher CreateTestPublisher(string codalId, Symbol symbol, string address, string manager)
    {
        return new Publisher(Guid.NewGuid(), symbol, DateTime.UtcNow)
        {
            CodalId = codalId,
            Address = address,
            ExecutiveManager = manager,
            NationalCode = "1234567890",
            ReportingType = ReportingType.Production,
            CompanyType = CompanyType.NoneFinancialInstitution,
            State = PublisherState.RegisterInTse,
            IsEnabled = true,
            FundType = PublisherFundType.NotAFund,
            SubCompanyType = PublisherSubCompanyType.Normal,
            MarketType = PublisherMarketType.None,
            IsEnableSubCompany = EnableSubCompany.InActive,
            IsSupplied = false
        };
    }

    private GetPublisherResponse CreatePublisherResponse(
        string id,
        string? isinCode,
        string displayedSymbol,
        string? parent,
        bool isEnabled,
        string? address = null,
        string? executiveManager = null)
    {
        return new GetPublisherResponse
        {
            Id = id,
            IsinCode = isinCode ?? string.Empty,
            DisplayedSymbol = displayedSymbol,
            Name = displayedSymbol + " Full Name",
            Parent = parent ?? string.Empty,
            Isic = "1234",
            ReportingType = 1,
            CompanyType = 1,
            ExecutiveManager = executiveManager ?? "Test Manager",
            Address = address ?? "Test Address",
            TelNo = "021-12345678",
            FaxNo = "021-87654321",
            ActivitySubject = "Test Activity",
            OfficeAddress = "Office Address",
            ShareOfficeAddress = "Share Office Address",
            Website = "https://test.com",
            Email = "test@test.com",
            State = 1,
            Inspector = "Inspector Name",
            FinancialManager = "Financial Manager",
            FactoryTel = "Factory Tel",
            FactoryFax = "Factory Fax",
            OfficeTel = "Office Tel",
            OfficeFax = "Office Fax",
            ShareOfficeTel = "Share Office Tel",
            ShareOfficeFax = "Share Office Fax",
            NationalCode = "123456789",
            FinancialYear = "12/29",
            ListedCapital = 1000000,
            AuditorName = "Auditor Name",
            IsEnableSubCompany = 0,
            IsEnabled = isEnabled,
            FundType = 0,
            SubCompanyType = 0,
            IsSupplied = false,
            MarketType = 0,
            UnauthorizedCapital = 500000
        };
    }
}
