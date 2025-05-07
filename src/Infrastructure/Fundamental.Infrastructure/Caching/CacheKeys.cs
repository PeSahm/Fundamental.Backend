namespace Fundamental.Infrastructure.Caching;

/// <summary>
/// Provides centralized cache key management for the application.
/// </summary>
public static class CacheKeys
{
    private const string PREFIX = "fundamental";

    public static class MarketData
    {
        private const string SCOPE = "market-data";

        public static string ClosingPriceInfo(string tseInsCode) =>
            $"{PREFIX}:{SCOPE}:closing-price-info:{tseInsCode}";
    }
    public static class MonthlyActivity
    {
        private const string SCOPE = "monthly-activity";
        public static string CodalIdIsinPair() =>
            $"{PREFIX}:{SCOPE}:codal-id-isin-pair";
    }
}