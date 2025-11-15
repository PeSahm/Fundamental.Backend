namespace Fundamental.IntegrationTests.TestData;

/// <summary>
/// Helper class to load test data for InterpretativeReportSummaryPage5 integration tests.
/// </summary>
public static class InterpretativeReportSummaryPage5TestData
{
    /// <summary>
    /// Gets the V2 test data for InterpretativeReportSummaryPage5 from IRO1SEPP0001.
    /// </summary>
    /// <returns>JSON string for V2 format.</returns>
    public static string GetV2TestData()
    {
        string filePath = Path.Combine(
            AppDomain.CurrentDomain.BaseDirectory,
            "Data",
            "InterpretativeReportSummaryPage5",
            "V2",
            "IRO1SEPP0001",
            "IRO1SEPP0001.json");

        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($"Test data file not found: {filePath}");
        }

        return File.ReadAllText(filePath);
    }
}
