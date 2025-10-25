# Financial Statements Processing System - Testing Strategy

## Executive Summary

This document outlines a comprehensive testing strategy for the financial statements processing system that addresses the critical challenges of external service dependencies, multiple financial statement versions, and the need for near-realistic testing environments.

## Current System Analysis

### Architecture Overview
- **Framework**: .NET 9.0 with Clean Architecture
- **Database**: PostgreSQL with Entity Framework Core
- **External Services**:
  - MDP (Market Data Provider)
  - TSE_TMC (Tehran Stock Exchange)
- **Message Queue**: CAP (DotNetCore.CAP) with PostgreSQL and Redis
- **Caching**: Redis with hybrid caching
- **Current Testing**: Basic unit tests (xUnit, FluentAssertions, Moq)

### Key Challenges
1. **External Dependencies**: Heavy reliance on MDP and TSE_TMC APIs
2. **Data Complexity**: Multiple financial statement versions and formats
3. **Processing Pipeline**: Parse → Save → Process workflow
4. **State Management**: Database state across processing stages
5. **Realism Gap**: Current tests lack realistic data and environments

## Testing Strategy Overview

### Testing Pyramid
```
┌─────────────────────────────────┐
│   End-to-End Tests (5%)         │
│   - Full pipeline testing       │
│   - Contract verification       │
└─────────────────────────────────┘
┌─────────────────────────────────┐
│   Integration Tests (20%)       │
│   - Component interaction       │
│   - External service mocking    │
└─────────────────────────────────┘
┌─────────────────────────────────┐
│   Unit Tests (75%)              │
│   - Business logic validation   │
│   - Data transformation         │
└─────────────────────────────────┘
```

### Test Environments
- **Unit Tests**: In-memory databases, mocked external services
- **Integration Tests**: Testcontainers (PostgreSQL, Redis)
- **E2E Tests**: Full Testcontainers stack with WireMock
- **Performance Tests**: Scaled Testcontainers with realistic data volumes

## Detailed Implementation Plan

### 1. Test Infrastructure Setup

#### Testcontainers Configuration
```xml
<!-- Add to Directory.Build.props under test packages -->
<PackageReference Include="Testcontainers.PostgreSQL" Version="4.1.0" />
<PackageReference Include="Testcontainers.Redis" Version="4.1.0" />
<PackageReference Include="Testcontainers" Version="4.1.0" />
<PackageReference Include="WireMock.Net" Version="1.6.10" />
<PackageReference Include="Respawn" Version="6.2.1" />
```

#### Base Test Fixture
```csharp
public class TestFixture : IAsyncLifetime
{
    public PostgreSqlContainer PostgreSqlContainer { get; private set; }
    public RedisContainer RedisContainer { get; private set; }
    public WireMockServer WireMockServer { get; private set; }
    public FundamentalDbContext DbContext { get; private set; }
    public IServiceProvider Services { get; private set; }

    public async Task InitializeAsync()
    {
        // Initialize containers
        PostgreSqlContainer = new PostgreSqlBuilder().Build();
        RedisContainer = new RedisBuilder().Build();
        WireMockServer = WireMockServer.Start();

        await Task.WhenAll(
            PostgreSqlContainer.StartAsync(),
            RedisContainer.StartAsync()
        );

        // Configure services with container connections
        var services = ConfigureTestServices();
        Services = services.BuildServiceProvider();
        DbContext = Services.GetRequiredService<FundamentalDbContext>();
    }

    public async Task DisposeAsync()
    {
        await Task.WhenAll(
            PostgreSqlContainer.DisposeAsync(),
            RedisContainer.DisposeAsync()
        );
        WireMockServer.Stop();
    }
}
```

### 2. Test Data Management

#### Realistic Financial Statement Data
```csharp
public static class FinancialStatementTestData
{
    public static IEnumerable<object[]> BalanceSheetVersions =>
        new List<object[]>
        {
            // Version 1.0 - Legacy format
            new object[] {
                "BalanceSheet_V1.json",
                new BalanceSheetData {
                    Assets = new AssetData { /* realistic data */ },
                    Liabilities = new LiabilityData { /* realistic data */ }
                }
            },
            // Version 2.0 - Current format
            new object[] {
                "BalanceSheet_V2.json",
                new BalanceSheetData {
                    Assets = new AssetData { /* updated structure */ },
                    Liabilities = new LiabilityData { /* updated structure */ }
                }
            }
        };

    public static async Task SeedDatabaseAsync(FundamentalDbContext context)
    {
        // Seed with realistic company data
        var companies = GenerateRealisticCompanies(100);
        await context.Companies.AddRangeAsync(companies);

        // Seed with historical financial statements
        var statements = GenerateHistoricalStatements(companies, 5); // 5 years
        await context.FinancialStatements.AddRangeAsync(statements);

        await context.SaveChangesAsync();
    }
}
```

#### Data Generation Strategy
- **Synthetic Data**: Generate realistic financial data using statistical models
- **Historical Data**: Include known edge cases and real-world scenarios
- **Version Coverage**: Ensure all supported financial statement versions are tested
- **Volume Testing**: Support both small datasets and large-scale performance tests

### 3. External Service Mocking

#### WireMock Configuration
```csharp
public class ExternalServiceMocks
{
    private readonly WireMockServer _server;

    public ExternalServiceMocks(WireMockServer server)
    {
        _server = server;
        ConfigureMdpMocks();
        ConfigureTseTmcMocks();
    }

    private void ConfigureMdpMocks()
    {
        _server
            .Given(Request.Create().WithPath("/api/market-data/*"))
            .RespondWith(Response.Create()
                .WithStatusCode(200)
                .WithBody(GetMarketDataResponse())
                .WithHeader("Content-Type", "application/json"));
    }

    private void ConfigureTseTmcMocks()
    {
        _server
            .Given(Request.Create().WithPath("/api/tse/*"))
            .RespondWith(Response.Create()
                .WithStatusCode(200)
                .WithBody(GetTseDataResponse())
                .WithHeader("Content-Type", "application/json"));
    }
}
```

#### Contract Testing
```csharp
public class ExternalServiceContractTests
{
    [Theory]
    [MemberData(nameof(MdpContractScenarios))]
    public async Task MdpService_ShouldHonorContract(ContractScenario scenario)
    {
        // Arrange
        var mockServer = WireMockServer.Start();
        ConfigureContractMock(mockServer, scenario);

        // Act
        var result = await _marketDataService.GetDataAsync(scenario.Request);

        // Assert
        result.Should().MatchContract(scenario.ExpectedResponse);
    }
}
```

### 4. Integration Testing Strategy

#### Processing Pipeline Tests
```csharp
public class FinancialStatementProcessingTests : IClassFixture<TestFixture>
{
    private readonly TestFixture _fixture;

    public FinancialStatementProcessingTests(TestFixture fixture)
    {
        _fixture = fixture;
    }

    [Theory]
    [MemberData(nameof(FinancialStatementTestData.BalanceSheetVersions))]
    public async Task ProcessBalanceSheet_ShouldHandleAllVersions(
        string version, BalanceSheetData expectedData)
    {
        // Arrange
        await RespawnDatabaseAsync();
        var rawData = LoadTestData(version);
        var processor = _fixture.Services.GetRequiredService<IBalanceSheetProcessor>();

        // Act
        var result = await processor.ProcessAsync(rawData);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEquivalentTo(expectedData);
    }

    [Fact]
    public async Task FullProcessingPipeline_ShouldMaintainDataIntegrity()
    {
        // Arrange
        await RespawnDatabaseAsync();
        var rawStatements = LoadBulkTestData(1000); // Large dataset

        // Act
        await _fixture.Services.GetRequiredService<IFinancialStatementProcessor>()
            .ProcessBatchAsync(rawStatements);

        // Assert
        var processedCount = await _fixture.DbContext.FinancialStatements.CountAsync();
        processedCount.Should().Be(1000);

        // Verify data integrity
        await VerifyDataIntegrityAsync();
    }
}
```

#### Database State Management
```csharp
public static class DatabaseTestExtensions
{
    public static async Task RespawnDatabaseAsync(this FundamentalDbContext context)
    {
        var checkpoint = Respawn.Checkpoint.Default;
        await checkpoint.Reset(context.Database.GetConnectionString());
    }

    public static async Task VerifyDataIntegrityAsync(this FundamentalDbContext context)
    {
        // Verify referential integrity
        var orphanedRecords = await context.FinancialStatements
            .Where(s => !context.Companies.Any(c => c.Id == s.CompanyId))
            .CountAsync();

        orphanedRecords.Should().Be(0);
    }
}
```

### 5. Performance Testing

#### Large-Scale Processing Tests
```csharp
public class PerformanceTests : IClassFixture<PerformanceTestFixture>
{
    [Fact]
    public async Task ProcessLargeDataset_ShouldCompleteWithinTimeLimit()
    {
        // Arrange
        var largeDataset = GenerateLargeDataset(10000);
        var stopwatch = Stopwatch.StartNew();

        // Act
        await _processor.ProcessBatchAsync(largeDataset);
        stopwatch.Stop();

        // Assert
        stopwatch.Elapsed.Should().BeLessThan(TimeSpan.FromMinutes(5));
    }

    [Fact]
    public async Task MemoryUsage_ShouldRemainStable()
    {
        // Arrange
        var initialMemory = GC.GetTotalMemory(true);
        var dataset = GenerateLargeDataset(5000);

        // Act
        await _processor.ProcessBatchAsync(dataset);
        var finalMemory = GC.GetTotalMemory(true);

        // Assert
        var memoryIncrease = finalMemory - initialMemory;
        memoryIncrease.Should().BeLessThan(100 * 1024 * 1024); // 100MB limit
    }
}
```

### 6. CI/CD Integration

#### GitHub Actions Workflow
```yaml
name: Financial Statements Tests

on: [push, pull_request]

jobs:
  test:
    runs-on: ubuntu-latest
    services:
      postgres:
        image: postgres:15
        env:
          POSTGRES_PASSWORD: postgres
        options: >-
          --health-cmd pg_isready
          --health-interval 10s
          --health-timeout 5s
          --health-retries 5

      redis:
        image: redis:7
        options: >-
          --health-cmd "redis-cli ping"
          --health-interval 10s
          --health-timeout 5s
          --health-retries 5

    steps:
    - uses: actions/checkout@v4

    - name: Setup .NET
      uses: actions/setup-dotnet@v4
      with:
        dotnet-version: 9.0.x

    - name: Run Unit Tests
      run: dotnet test tests/UnitTests --configuration Release --collect:"XPlat Code Coverage"

    - name: Run Integration Tests
      run: dotnet test tests/IntegrationTests --configuration Release
      env:
        DATABASE_URL: postgresql://postgres:postgres@localhost:5432/fundamental_test
        REDIS_URL: redis://localhost:6379

    - name: Run Performance Tests
      run: dotnet test tests/PerformanceTests --configuration Release

    - name: Upload Coverage
      uses: codecov/codecov-action@v4
```

### 7. Monitoring and Metrics

#### Test Metrics Dashboard
- **Coverage Metrics**: Track code coverage trends
- **Performance Benchmarks**: Monitor processing times
- **Failure Analysis**: Track common failure patterns
- **External Service Health**: Monitor mock accuracy vs real services

#### Alerting Rules
- Coverage drops below 85%
- Performance regression > 10%
- External service contract violations
- Test flakiness > 5%

## Implementation Roadmap

### Phase 1: Foundation (2 weeks)
- [ ] Set up Testcontainers infrastructure
- [ ] Create base test fixtures
- [ ] Implement basic external service mocking
- [ ] Establish test data generation framework

### Phase 2: Integration Testing (3 weeks)
- [ ] Build integration test suites
- [ ] Implement contract testing
- [ ] Create realistic test data sets
- [ ] Set up database state management

### Phase 3: Advanced Testing (2 weeks)
- [ ] Performance testing suite
- [ ] End-to-end pipeline tests
- [ ] CI/CD pipeline integration
- [ ] Monitoring and alerting setup

### Phase 4: Optimization (1 week)
- [ ] Test execution optimization
- [ ] Parallel test execution
- [ ] Test data caching
- [ ] Documentation and training

## Success Metrics

### Quality Metrics
- **Code Coverage**: > 85% overall, > 90% for critical paths
- **Test Reliability**: < 2% flaky tests
- **External Service Coverage**: 100% of API endpoints mocked

### Performance Metrics
- **Test Execution Time**: < 10 minutes for full suite
- **Large Dataset Processing**: < 5 minutes for 10k records
- **Memory Usage**: Stable memory consumption during processing

### Business Metrics
- **Defect Detection**: > 95% of production issues caught in testing
- **Deployment Confidence**: Zero production rollbacks due to untested scenarios
- **Time to Market**: Reduced testing cycle time by 60%

## Risk Mitigation

### Technical Risks
- **Container Complexity**: Start with simple Testcontainers, scale complexity gradually
- **Data Realism**: Validate generated data against real samples regularly
- **External API Changes**: Implement contract testing to catch breaking changes early

### Operational Risks
- **Test Maintenance**: Automate test data updates and mock maintenance
- **CI/CD Bottlenecks**: Implement parallel execution and test result caching
- **Team Adoption**: Provide comprehensive documentation and training

## Conclusion

This testing strategy addresses the core challenges of testing a financial statements processing system by providing realistic, isolated test environments that closely mimic production while maintaining fast feedback cycles. The combination of Testcontainers, comprehensive mocking, and realistic data generation ensures high confidence in releases while enabling rapid development cycles.</content>
<parameter name="filePath">c:\Repos\Personal\Fundamental\Fundamental.Backend\TESTING_STRATEGY.md