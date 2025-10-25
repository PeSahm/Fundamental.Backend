using System.Data.Common;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Configurations;
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
using Microsoft.EntityFrameworkCore.Query;

namespace IntegrationTests;

public class TestFixture : IAsyncLifetime
{
    private PostgreSqlContainer? _postgresContainer;
    private RedisContainer? _redisContainer;
    private WireMockServer? _wireMockServer;
    private IServiceProvider? _serviceProvider;
    // private Respawn.Checkpoint? _checkpoint;

    public IServiceProvider Services => _serviceProvider ?? throw new InvalidOperationException("Test fixture not initialized");
    public FundamentalDbContext DbContext => Services.GetRequiredService<FundamentalDbContext>();
    public string PostgresConnectionString => _postgresContainer?.GetConnectionString() ?? throw new InvalidOperationException("PostgreSQL container not initialized");
    public string RedisConnectionString => _redisContainer?.GetConnectionString() ?? throw new InvalidOperationException("Redis container not initialized");
    public WireMockServer WireMockServer => _wireMockServer ?? throw new InvalidOperationException("WireMock server not initialized");
    public ExternalServiceMocks ExternalMocks { get; private set; }

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
            _redisContainer.StartAsync()
        );

        // Configure services
        var services = new ServiceCollection();
        ConfigureServices(services);
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

    // public async Task ResetDatabaseAsync()
    // {
    //     if (_checkpoint == null)
    //     {
    //         throw new InvalidOperationException("Respawn checkpoint not initialized");
    //     }

    //     await using var connection = new NpgsqlConnection(PostgresConnectionString);
    //     await connection.OpenAsync();
    //     await _checkpoint.ResetAsync(connection);
    // }

    private void ConfigureServices(IServiceCollection services)
    {
        // Configuration
        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["ConnectionStrings:DefaultConnection"] = PostgresConnectionString,
                ["ConnectionStrings:Redis"] = RedisConnectionString,
                ["Mdp:url"] = $"http://localhost:{WireMockServer.Port}",
                ["TseTmc:url"] = $"http://localhost:{WireMockServer.Port}"
            })
            .Build();

        services.AddSingleton<IConfiguration>(configuration);

        // Database context
        services.AddDbContext<FundamentalDbContext>(options =>
            options.UseNpgsql(PostgresConnectionString));

        // Register as IUnitOfWork
        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<FundamentalDbContext>());

        // Add infrastructure services (minimal setup for testing)
        // services.AddInfrastructure(configuration);
    }

    private async Task InitializeDatabaseAsync()
    {
        using var scope = Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<FundamentalDbContext>();

        // Create database schema directly (more reliable for testing than running migrations)
        await context.Database.EnsureCreatedAsync();

        // Ensure database is ready
        await context.Database.CanConnectAsync();
    }


}