using Fundamental.Infrastructure.Extensions;
using Fundamental.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


IHostBuilder? hostBuilder = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddLogging()
            .AddDbContexts(hostContext.Configuration);
    });

hostBuilder.ConfigureAppConfiguration(b => b
    .AddJsonFile("appsettings.Override.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables());

IHost? host = hostBuilder.Build();

if (args.Length > 0 && args[0] == "migrate")
{
    using IServiceScope scope = host.Services.CreateScope();

    FundamentalDbContext appDb = scope.ServiceProvider.GetRequiredService<FundamentalDbContext>();
    appDb.Database.Migrate();
}
else
{
    Console.WriteLine(@"No arguments provided. Running host...");
    host.Run();
}