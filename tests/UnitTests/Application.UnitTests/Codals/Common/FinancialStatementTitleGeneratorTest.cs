using FluentAssertions;
using Fundamental.Application.Codals.Common;
using JetBrains.Annotations;

namespace Application.UnitTests.Codals.Common;

[TestSubject(typeof(FinancialStatementTitleGenerator))]
public class FinancialStatementTitleGeneratorTest
{
    [Theory]
    [InlineData(
        "گزارش صورت وضعیت مالی نماد ",
        "TEST",
        6,
        12,
        1402,
        true,
        "گزارش صورت وضعیت مالی نماد TEST ماه 6 دوره 6 ماهه  منتهی به 1402/12/29 حسابرسی شده")]
    [InlineData(
        "گزارش صورت سود و زیان نماد ",
        "XYZ",
        3,
        12,
        1402,
        false,
        "گزارش صورت سود و زیان نماد XYZ ماه 3 دوره 3 ماهه  منتهی به 1402/12/29 حسابرسی نشده")]
    [InlineData(
        "گزارش صورت وضعیت مالی نماد ",
        "ABC",
        12,
        9,
        1402,
        true,
        "گزارش صورت وضعیت مالی نماد ABC ماه 12 دوره 3 ماهه  منتهی به 1402/9/30 حسابرسی شده")]
    public void GenerateTitle_ShouldGenerateCorrectTitle(
        string reportType,
        string symbol,
        ushort reportMonth,
        ushort yearEndMonth,
        ushort fiscalYear,
        bool isAudited,
        string expected)
    {
        // Act
        string result = FinancialStatementTitleGenerator.GenerateTitle(
            reportType,
            symbol,
            reportMonth,
            yearEndMonth,
            fiscalYear,
            isAudited);

        // Assert
        result.Should().Be(expected);
    }
}