using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;

namespace IntegrationTests;

public class ExternalServiceMocks
{
    private readonly WireMockServer _server;

    public ExternalServiceMocks(WireMockServer server)
    {
        _server = server;
        ConfigureMocks();
    }

    private void ConfigureMocks()
    {
        ConfigureMdpMocks();
        ConfigureTseTmcMocks();
    }

    private void ConfigureMdpMocks()
    {
        // Mock MDP market data endpoint
        _server
            .Given(Request.Create()
                .WithPath("/api/market-data/*")
                .UsingGet())
            .RespondWith(Response.Create()
                .WithStatusCode(200)
                .WithHeader("Content-Type", "application/json")
                .WithBody(@"{
                    ""symbol"": ""TEST123"",
                    ""price"": 1000.50,
                    ""volume"": 10000,
                    ""timestamp"": ""2024-01-01T10:00:00Z""
                }"));

        // Mock MDP bulk data endpoint
        _server
            .Given(Request.Create()
                .WithPath("/api/bulk-data")
                .UsingPost())
            .RespondWith(Response.Create()
                .WithStatusCode(200)
                .WithHeader("Content-Type", "application/json")
                .WithBody(@"{
                    ""status"": ""success"",
                    ""data"": [
                        {
                            ""symbol"": ""TEST123"",
                            ""price"": 1000.50,
                            ""volume"": 10000
                        },
                        {
                            ""symbol"": ""TEST456"",
                            ""price"": 2000.75,
                            ""volume"": 5000
                        }
                    ]
                }"));
    }

    private void ConfigureTseTmcMocks()
    {
        // Mock TSE_TMC company info endpoint
        _server
            .Given(Request.Create()
                .WithPath("/api/company/*")
                .UsingGet())
            .RespondWith(Response.Create()
                .WithStatusCode(200)
                .WithHeader("Content-Type", "application/json")
                .WithBody(@"{
                    ""companyCode"": ""123456789"",
                    ""companyName"": ""Test Manufacturing Company"",
                    ""industry"": ""Manufacturing"",
                    ""status"": ""Active""
                }"));

        // Mock TSE_TMC financial statements endpoint
        _server
            .Given(Request.Create()
                .WithPath("/api/financial-statements")
                .UsingPost())
            .RespondWith(Response.Create()
                .WithStatusCode(200)
                .WithHeader("Content-Type", "application/json")
                .WithBody(@"{
                    ""statements"": [
                        {
                            ""type"": ""BalanceSheet"",
                            ""period"": ""2023"",
                            ""data"": {
                                ""assets"": 1000000,
                                ""liabilities"": 500000,
                                ""equity"": 500000
                            }
                        }
                    ]
                }"));
    }

    public void SetupBalanceSheetApiResponse(string jsonResponse)
    {
        // Mock CODAL balance sheet API endpoint
        _server
            .Given(Request.Create()
                .WithPath("/api/codal/balance-sheet")
                .UsingPost())
            .RespondWith(Response.Create()
                .WithStatusCode(200)
                .WithHeader("Content-Type", "application/json")
                .WithBody(jsonResponse));
    }
}