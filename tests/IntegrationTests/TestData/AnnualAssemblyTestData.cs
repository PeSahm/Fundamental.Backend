namespace Fundamental.IntegrationTests.TestData;

/// <summary>
/// Helper class to load test data for Annual Assembly integration tests.
/// </summary>
public static class AnnualAssemblyTestData
{
    /// <summary>
    /// Gets the V1 test data for Annual Assembly from IRO1BAHN0001.
    /// </summary>
    /// <returns>JSON string for V1 format.</returns>
    public static string GetV1TestData()
    {
        string filePath = Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory,
            "Data",
            "AnnualAssembly",
            "V1",
            "IRO1BAHN0001",
            "IRO1BAHN0001.json");

        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($"Test data file not found: {filePath}");
        }

        return File.ReadAllText(filePath);
    }
}
