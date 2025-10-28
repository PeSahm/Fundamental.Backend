using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using Fundamental.Domain.Repositories.Base;
using Fundamental.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using Testcontainers.PostgreSql;
using Testcontainers.Redis;
using WireMock.Server;
using Xunit;
using Moq;
using Fundamental.Application.Codals.Services;
using Fundamental.Application.Codals.Services.Models.CodelServiceModels;
using Fundamental.Application.Codals.Manufacturing.Jobs.UpdateBalanceSheetData;

namespace IntegrationTests;

public class TestFixture : IAsyncLifetime
{
    private PostgreSqlContainer? _postgresContainer;
    private RedisContainer? _redisContainer;
    private WireMockServer? _wireMockServer;
    private IServiceProvider? _serviceProvider;
    private Mock<ICodalService>? _codalServiceMock;

    public IServiceProvider Services => _serviceProvider ?? throw new InvalidOperationException("Test fixture not initialized");
    public FundamentalDbContext DbContext => Services.GetRequiredService<FundamentalDbContext>();
    public string PostgresConnectionString => _postgresContainer?.GetConnectionString() ?? throw new InvalidOperationException("PostgreSQL container not initialized");
    public string RedisConnectionString => _redisContainer?.GetConnectionString() ?? throw new InvalidOperationException("Redis container not initialized");
    public WireMockServer WireMockServer => _wireMockServer ?? throw new InvalidOperationException("WireMock server not initialized");
    public ExternalServiceMocks ExternalMocks { get; private set; }
    public Mock<ICodalService> CodalServiceMock => _codalServiceMock ?? throw new InvalidOperationException("CodalService mock not initialized");

    public async Task InitializeAsync()
    {
        // Initialize PostgreSQL container
        _postgresContainer = new PostgreSqlBuilder()
            .WithImage("postgres:15-alpine")
            .WithDatabase("fundamental_test")
            .WithUsername("postgres")
            .WithPassword("postgres")
            .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(5432))
            .Build();

        // Initialize Redis container
        _redisContainer = new RedisBuilder()
            .WithImage("redis:7-alpine")
            .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(6379))
            .Build();

        // Initialize WireMock server for external API mocking
        _wireMockServer = WireMockServer.Start();

        // Start containers
        await Task.WhenAll(
            _postgresContainer.StartAsync(),
            _redisContainer.StartAsync());

        // Initialize CodalService mock
        _codalServiceMock = new Mock<ICodalService>();

        // Configure services
        IServiceCollection services = new ServiceCollection();
        ConfigureServices(services, _codalServiceMock);
        _serviceProvider = services.BuildServiceProvider();

        // Initialize external service mocks
        ExternalMocks = new ExternalServiceMocks(_wireMockServer);

        // Initialize database
        await InitializeDatabaseAsync();

        // Setup Respawn for database cleanup
        // _checkpoint = new Respawn.Checkpoint();
    }

    public async Task DisposeAsync()
    {
        if (_serviceProvider is IDisposable disposable)
        {
            disposable.Dispose();
        }

        List<Task> disposeTasks = new();

        if (_postgresContainer != null)
        {
            disposeTasks.Add(_postgresContainer.DisposeAsync().AsTask());
        }

        if (_redisContainer != null)
        {
            disposeTasks.Add(_redisContainer.DisposeAsync().AsTask());
        }

        if (_wireMockServer != null)
        {
            _wireMockServer.Stop();
        }

        await Task.WhenAll(disposeTasks);
    }

    private void ConfigureServices(IServiceCollection services, Mock<ICodalService> codalServiceMock)
    {
        // Configuration
        IConfigurationBuilder configurationBuilder = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["ConnectionStrings:DefaultConnection"] = PostgresConnectionString,
                ["ConnectionStrings:Redis"] = RedisConnectionString,
                ["Mdp:url"] = $"http://localhost:{WireMockServer.Port}",
                ["TseTmc:url"] = $"http://localhost:{WireMockServer.Port}"
            });
        IConfiguration configuration = configurationBuilder.Build();

        services.AddSingleton<IConfiguration>(configuration);

        // Add logging
        services.AddLogging();

        // Database context
        services.AddDbContext<FundamentalDbContext>(options =>
            options.UseNpgsql(PostgresConnectionString));

        // Register as IUnitOfWork
        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<FundamentalDbContext>());

        // Register mock CodalService
        services.AddSingleton<ICodalService>(codalServiceMock.Object);

        // Register repositories needed for testing
        services.AddScoped<IRepository, Fundamental.Infrastructure.Persistence.Repositories.Base.FundamentalRepository>();
        services.AddScoped<Fundamental.Application.Codals.Manufacturing.Repositories.IBalanceSheetReadRepository, Fundamental.Infrastructure.Repositories.Codals.Manufacturing.BalanceSheetReadRepository>();

        // Note: MediatR registration removed to avoid licensing issues in tests
        // If needed, handlers can be tested directly

        // Add infrastructure services (minimal setup for testing)
        // services.AddInfrastructure(configuration);
    }

    private async Task InitializeDatabaseAsync()
    {
        using IServiceScope scope = Services.CreateScope();
        FundamentalDbContext context = scope.ServiceProvider.GetRequiredService<FundamentalDbContext>();

        // Create database schema directly (more reliable for testing than running migrations)
        await context.Database.EnsureCreatedAsync();

        // Ensure database is ready
        await context.Database.CanConnectAsync();
    }
}