namespace Fundamental.IntegrationTests.TestData;

/// <summary>
/// Helper class to load test data for Extraordinary Annual Assembly integration tests.
/// </summary>
public static class ExtraAnnualAssemblyTestData
{
    /// <summary>
    /// Gets the V1 test data for Extraordinary Annual Assembly from IRO3RYHZ0001.
    /// </summary>
    /// <summary>
    /// Retrieves the V1 test data JSON for IRO3RYHZ0001 from the test data folder.
    /// </summary>
    /// <returns>The JSON contents of the V1 test data file for IRO3RYHZ0001.</returns>
    /// <exception cref="System.IO.FileNotFoundException">Thrown when the expected test data file does not exist at the constructed path.</exception>
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