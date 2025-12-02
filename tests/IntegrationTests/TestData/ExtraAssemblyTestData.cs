namespace Fundamental.IntegrationTests.TestData;

/// <summary>
/// Helper class to load test data for ExtraAssembly integration tests.
/// </summary>
public static class ExtraAssemblyTestData
{
    /// <summary>
    /// Gets the V1 test data for ExtraAssembly from IRO1SSEP0001 (Capital Increase scenario).
    /// </summary>
    /// <returns>JSON string for V1 format.</returns>
    public static string GetV1CapitalIncreaseTestData()
    {
        string filePath = System.IO.Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory,
            "Data",
            "ExtraAssembly",
            "V1",
            "1418249",
            "IRO1SSEP0001.json");

        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($"Test data file not found: {filePath}");
        }

        return File.ReadAllText(filePath);
    }

    /// <summary>
    /// Gets the V1 test data for ExtraAssembly from IRO1SHZG0001 (Financial Year Change scenario).
    /// </summary>
    /// <returns>JSON string for V1 format.</returns>
    public static string GetV1FiscalYearChangeTestData()
    {
        string filePath = System.IO.Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory,
            "Data",
            "ExtraAssembly",
            "V1",
            "1438164",
            "IRO1SHZG0001.json");

        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($"Test data file not found: {filePath}");
        }

        return File.ReadAllText(filePath);
    }
}
