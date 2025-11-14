using FluentAssertions;
using Fundamental.Application.Codals.Jobs.UpdateCodalPublisherData;
using Fundamental.Application.Codals.Services;
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
using Xunit;

namespace IntegrationTests.Codals;

[Collection("Sequential")]
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

        Symbol existingSymbol1 = CreateTestSymbol("IRO100000001", "Test Company");
        Symbol existingSymbol2 = CreateTestSymbol("IRO100000002", "New Company");
        _fixture.DbContext.Symbols.AddRange(existingSymbol1, existingSymbol2);
        await _fixture.DbContext.SaveChangesAsync();

        List<GetPublisherResponse> publishers = new()
        {
            CreatePublisherResponse("PUB001", "IRO100000001", "Test Company", null, true),
            CreatePublisherResponse("PUB002", "IRO100000002", "New Company", null, false)
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
            .FirstOrDefaultAsync(p => p.CodalId == "PUB001");
        publisher1.Should().NotBeNull();
        publisher1!.Symbol.Isin.Should().Be("IRO100000001");
        publisher1.IsEnabled.Should().BeTrue();

        // Verify new publisher with existing symbol was created
        Publisher? publisher2 = await _fixture.DbContext.Publishers
            .Include(p => p.Symbol)
            .FirstOrDefaultAsync(p => p.CodalId == "PUB002");
        publisher2.Should().NotBeNull();
        publisher2!.Symbol.Isin.Should().Be("IRO100000002");
        publisher2.IsEnabled.Should().BeFalse();
    }

    [Fact]
    public async Task UpdateCodalPublisherData_ShouldUpdateExistingPublishers()
    {
        // Arrange
        await CleanupTestData();

        Symbol symbol = CreateTestSymbol("IRO100000003", "Update Test");
        _fixture.DbContext.Symbols.Add(symbol);

        Publisher existingPublisher = CreateTestPublisher("PUB003", symbol, "Old Address", "Old Manager");
        _fixture.DbContext.Publishers.Add(existingPublisher);
        await _fixture.DbContext.SaveChangesAsync();

        List<GetPublisherResponse> publishers = new()
        {
            CreatePublisherResponse(
                "PUB003",
                "IRO100000003",
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
            .FirstOrDefaultAsync(p => p.CodalId == "PUB003");
        updatedPublisher.Should().NotBeNull();
        updatedPublisher!.Address.Should().Be("New Address");
        updatedPublisher.ExecutiveManager.Should().Be("New Manager");
        updatedPublisher.IsEnabled.Should().BeTrue();
    }

    [Fact]
    public async Task UpdateCodalPublisherData_ShouldCreateSymbolFromParentSymbol()
    {
        // Arrange
        await CleanupTestData();

        Symbol parentSymbol = CreateTestSymbol("IRO100000004", "Parent Company");
        _fixture.DbContext.Symbols.Add(parentSymbol);
        await _fixture.DbContext.SaveChangesAsync();

        List<GetPublisherResponse> publishers = new()
        {
            CreatePublisherResponse("PUB004", null, "Parent Company-Sub", "Parent Company", false)
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
            .FirstOrDefaultAsync(p => p.CodalId == "PUB004");
        publisher.Should().NotBeNull();
        publisher!.Symbol.Should().NotBeNull();
        publisher.Symbol.Name.Should().Be("Parent Company-Sub");
        publisher.ParentSymbol.Should().NotBeNull();
        publisher.ParentSymbol!.Id.Should().Be(parentSymbol.Id);
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
            Symbol symbol = CreateTestSymbol($"IRO{100+i:D9}", $"Bulk Company {i}");
            existingSymbols.Add(symbol);
        }

        _fixture.DbContext.Symbols.AddRange(existingSymbols);
        await _fixture.DbContext.SaveChangesAsync();

        // Create 40 publishers (20 for existing symbols, 20 with parent symbols)
        Symbol parentSymbol = CreateTestSymbol("IRO100000006", "Bulk Parent");
        _fixture.DbContext.Symbols.Add(parentSymbol);
        await _fixture.DbContext.SaveChangesAsync();

        List<GetPublisherResponse> publishers = new();
        for (int i = 1; i <= 20; i++)
        {
            publishers.Add(CreatePublisherResponse($"BULK{i:D3}", $"IRO{100+i:D9}", $"Bulk Company {i}", null, true));
        }

        for (int i = 21; i <= 40; i++)
        {
            publishers.Add(CreatePublisherResponse($"BULK{i:D3}", null, $"Bulk Parent-Sub{i}", "Bulk Parent", false));
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

        int publisherCount = await _fixture.DbContext.Publishers.CountAsync();
        publisherCount.Should().Be(40);

        // Verify symbols were created for publishers with parent symbols
        int newSymbolCount = await _fixture.DbContext.Symbols
            .Where(s => s.IsUnOfficial && s.Name.Contains("Sub"))
            .CountAsync();
        newSymbolCount.Should().Be(20);
    }

    [Fact]
    public async Task UpdateCodalPublisherData_ShouldSkipPublishersWithNoSymbolAndNoParent()
    {
        // Arrange
        await CleanupTestData();

        List<GetPublisherResponse> publishers = new()
        {
            CreatePublisherResponse("PUB005", "IRO100000099", "Non-existent", null, false)
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
            .FirstOrDefaultAsync(p => p.CodalId == "PUB005");
        publisher.Should().BeNull();
    }

    [Fact]
    public async Task UpdateCodalPublisherData_ShouldUpdatePublisherSymbolWhenIsinChanges()
    {
        // Arrange
        await CleanupTestData();

        Symbol oldSymbol = CreateTestSymbol("IRO100000005", "Old Symbol");
        Symbol newSymbol = CreateTestSymbol("IRO100000002", "New Symbol");
        _fixture.DbContext.Symbols.AddRange(oldSymbol, newSymbol);

        Publisher publisher = CreateTestPublisher("PUB006", oldSymbol, "Address", "Manager");
        _fixture.DbContext.Publishers.Add(publisher);
        await _fixture.DbContext.SaveChangesAsync();

        List<GetPublisherResponse> publishers = new()
        {
            CreatePublisherResponse("PUB006", "IRO100000002", "New Symbol", null, true)
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
            .FirstOrDefaultAsync(p => p.CodalId == "PUB006");
        updatedPublisher.Should().NotBeNull();
        updatedPublisher!.Symbol.Isin.Should().Be("IRO100000002");
    }

    private async Task CleanupTestData()
    {
        // Clean up publishers first (due to foreign key)
        List<Publisher> publishers = await _fixture.DbContext.Publishers.ToListAsync();
        _fixture.DbContext.Publishers.RemoveRange(publishers);

        // Clean up test symbols
        List<Symbol> symbols = await _fixture.DbContext.Symbols
            .Where(s => s.Isin.StartsWith("IRO10000000") ||
                       s.Isin.StartsWith("IRO10000000") ||
                       s.Isin.StartsWith("IRO10000000") ||
                       s.Isin.StartsWith("IRO10000000") ||
                       s.Isin.StartsWith("IRO10000000") ||
                       s.Isin.StartsWith("IRO1000000"))
            .ToListAsync();
        _fixture.DbContext.Symbols.RemoveRange(symbols);

        await _fixture.DbContext.SaveChangesAsync();
    }

    private Symbol CreateTestSymbol(string isin, string name)
    {
        return new Symbol(
            Guid.NewGuid(),
            isin,
            isin.Substring(3, 9),
            name + " EN",
            isin.Substring(3, 6),
            name,
            name,
            isin.Substring(3, 6),
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
