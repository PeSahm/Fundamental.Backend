namespace Fundamental.IntegrationTests.TestData;

/// <summary>
/// Helper class to load test data for Extraordinary Annual Assembly integration tests.
/// </summary>
public static class ExtraAnnualAssemblyTestData
{
    /// <summary>
    /// Gets the V1 test data for Extraordinary Annual Assembly from IRO3RYHZ0001.
    /// </summary>
    /// <returns>JSON string for V1 format.</returns>
    public static string GetV1TestData()
    {
        string filePath = Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory,
            "Data",
            "ExtraAnnualAssembly",
            "V1",
            "IRO3RYHZ0001",
            "IRO3RYHZ0001.json");

        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($"Test data file not found: {filePath}");
        }

        return File.ReadAllText(filePath);
    }
}
