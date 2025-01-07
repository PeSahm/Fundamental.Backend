using Fundamental.Domain.Codals.Manufacturing.Entities;
using Fundamental.Domain.Codals.ValueObjects;
using JetBrains.Annotations;

namespace Domain.UnitTests.Codals.Manufacturing.Entities;

[TestSubject(typeof(FinancialStatement))]
public class FinancialStatementTest
{
    [Theory]
    [InlineData(6, 9, 9)]
    [InlineData(1, 12, 1)]
    [InlineData(12, 1, 11)]
    [InlineData(3, 6, 9)]
    [InlineData(11, 2, 9)]
    public void AdjustedMonth_ShouldReturnCorrectMonth(int reportMonth, int yearEndMonth, int expectedAdjustedMonth)
    {
        // Arrange
        StatementMonth month = new StatementMonth(reportMonth);

        StatementMonth adjustedMonth = month.AdjustedMonth(yearEndMonth);

        // Assert
        Assert.Equal(expectedAdjustedMonth, adjustedMonth.Month);
    }
}