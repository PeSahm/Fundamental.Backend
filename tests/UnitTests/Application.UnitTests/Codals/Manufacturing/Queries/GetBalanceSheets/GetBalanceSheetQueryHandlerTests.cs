using FluentAssertions;
using Fundamental.Application.Codals.Manufacturing.Queries.GetBalanceSheets;
using Fundamental.Application.Codals.Manufacturing.Repositories;
using Fundamental.Domain.Common.Dto;
using Fundamental.ErrorHandling;
using Moq;

namespace Application.UnitTests.Codals.Manufacturing.Queries.GetBalanceSheets;

public class GetBalanceSheetQueryHandlerTests
{
    private readonly Mock<IBalanceSheetReadRepository> _balanceSheetReadRepositoryMock;
    private readonly GetBalanceSheetQueryHandler _handler;

    public GetBalanceSheetQueryHandlerTests()
    {
        _balanceSheetReadRepositoryMock = new Mock<IBalanceSheetReadRepository>();
        _handler = new GetBalanceSheetQueryHandler(_balanceSheetReadRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldCallRepositoryWithCorrectRequest()
    {
        // Arrange
        GetBalanceSheetRequest request = new GetBalanceSheetRequest(
            IsinList: new List<string> { "IRO1TEST0001", "IRO1TEST0002" },
            TraceNo: 12345UL,
            FiscalYear: 1402,
            ReportMonth: 6);

        Response<Paginated<GetBalanceSheetResultDto>> expectedResponse = new Paginated<GetBalanceSheetResultDto>(
            Items: new List<GetBalanceSheetResultDto>(),
            Meta: new PaginationMeta(Total: 0, From: 1, To: 10));

        _balanceSheetReadRepositoryMock
            .Setup(x => x.GetBalanceSheet(request, It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResponse.Data!);

        CancellationToken cancellationToken = CancellationToken.None;

        // Act
        Response<Paginated<GetBalanceSheetResultDto>> result = await _handler.Handle(request, cancellationToken);

        // Assert
        result.Should().Be(expectedResponse);
        _balanceSheetReadRepositoryMock.Verify(
            x => x.GetBalanceSheet(request, cancellationToken),
            Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnRepositoryResponse()
    {
        // Arrange
        GetBalanceSheetRequest request = new GetBalanceSheetRequest(
            IsinList: new List<string>(),
            TraceNo: null,
            FiscalYear: null,
            ReportMonth: null);

        List<GetBalanceSheetResultDto> balanceSheetResults = new List<GetBalanceSheetResultDto>
        {
            new GetBalanceSheetResultDto
            {
                Isin = "IRO1TEST0001",
                Symbol = "TEST1",
                TraceNo = 12345UL,
                Uri = "http://test.com",
                FiscalYear = 1402,
                ReportMonth = 6,
                IsAudited = true
            }
        };

        Paginated<GetBalanceSheetResultDto> paginatedResult = new Paginated<GetBalanceSheetResultDto>(
            Items: balanceSheetResults,
            Meta: new PaginationMeta(Total: 1, From: 1, To: 10));

        _balanceSheetReadRepositoryMock
            .Setup(x => x.GetBalanceSheet(request, It.IsAny<CancellationToken>()))
            .ReturnsAsync(paginatedResult);

        // Act
        Response<Paginated<GetBalanceSheetResultDto>> result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        result.Success.Should().BeTrue();
        result.Data.Should().BeEquivalentTo(paginatedResult);
        result.Data.Items.Should().HaveCount(1);
        result.Data.Items[0].Isin.Should().Be("IRO1TEST0001");
        result.Data.Items[0].Symbol.Should().Be("TEST1");
    }

    [Fact]
    public async Task Handle_ShouldPassCancellationTokenToRepository()
    {
        // Arrange
        GetBalanceSheetRequest request = new GetBalanceSheetRequest(
            IsinList: new List<string> { "IRO1TEST0001" },
            TraceNo: null,
            FiscalYear: null,
            ReportMonth: null);

        CancellationToken cancellationToken = new CancellationTokenSource().Token;

        _balanceSheetReadRepositoryMock
            .Setup(x => x.GetBalanceSheet(request, cancellationToken))
            .ReturnsAsync(new Paginated<GetBalanceSheetResultDto>(
                Items: new List<GetBalanceSheetResultDto>(),
                Meta: new PaginationMeta(Total: 0, From: 1, To: 10)));

        // Act
        await _handler.Handle(request, cancellationToken);

        // Assert
        _balanceSheetReadRepositoryMock.Verify(
            x => x.GetBalanceSheet(request, cancellationToken),
            Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldHandleEmptyResult()
    {
        // Arrange
        GetBalanceSheetRequest request = new GetBalanceSheetRequest(
            IsinList: new List<string>(),
            TraceNo: null,
            FiscalYear: null,
            ReportMonth: null);

        Paginated<GetBalanceSheetResultDto> emptyResult = new Paginated<GetBalanceSheetResultDto>(
            Items: new List<GetBalanceSheetResultDto>(),
            Meta: new PaginationMeta(Total: 0, From: 1, To: 10));

        _balanceSheetReadRepositoryMock
            .Setup(x => x.GetBalanceSheet(request, It.IsAny<CancellationToken>()))
            .ReturnsAsync(emptyResult);

        // Act
        Response<Paginated<GetBalanceSheetResultDto>> result = await _handler.Handle(request, CancellationToken.None);

        // Assert
        result.Success.Should().BeTrue();
        result.Data!.Items.Should().BeEmpty();
        result.Data.Meta.Total.Should().Be(0);
    }
}