using Fundamental.Infrastructure.Extensions;
using Fundamental.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Npgsql;

IHostBuilder? hostBuilder = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddLogging()
            .AddDbContexts(hostContext.Configuration);
    });

hostBuilder.ConfigureAppConfiguration(b => b
    .AddJsonFile("appsettings.Override.json", true, true)
    .AddEnvironmentVariables()
    .AddUserSecrets<Program>()
    )
    ;

IHost? host = hostBuilder.Build();

if (args.Length > 0 && args[0] == "migrate")
{
    using IServiceScope scope = host.Services.CreateScope();

    FundamentalDbContext appDb = scope.ServiceProvider.GetRequiredService<FundamentalDbContext>();
    appDb.Database.Migrate();

    if (appDb.Database.GetDbConnection() is NpgsqlConnection npgsqlConnection)
    {
        await npgsqlConnection.OpenAsync();

        try
        {
            await npgsqlConnection.ReloadTypesAsync();
        }
        finally
        {
            await npgsqlConnection.CloseAsync();
        }
    }
}
else
{
    Console.WriteLine(@"No arguments provided. Running host...");
    host.Run();
}