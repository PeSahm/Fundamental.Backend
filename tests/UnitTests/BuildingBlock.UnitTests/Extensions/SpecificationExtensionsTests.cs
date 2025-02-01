using BuildingBlockUnitTests.Extensions.Models;

namespace BuildingBlockUnitTests.Extensions;

using Ardalis.Specification;
using Fundamental.BuildingBlock.Extensions;

public class SpecificationExtensionsTests
{
    [Fact]
    public void LoadSpecification_CopiesWhereExpressions()
    {
        // Arrange
        ReferenceSpec referenceSpec = new ReferenceSpec();
        referenceSpec.Query.Where(e => e.Id > 5);
        TargetSpec targetSpec = new TargetSpec();

        // Act
        targetSpec.LoadSpecification(referenceSpec);

        // Assert
        Assert.Single(targetSpec.WhereExpressions);
        Assert.Equal(
            referenceSpec.WhereExpressions.ToList()[0].Filter.ToString(),
            targetSpec.WhereExpressions.ToList()[0].Filter.ToString());
    }

    [Fact]
    public void LoadSpecification_CopiesOrderExpressions()
    {
        // Arrange
        ReferenceSpec referenceSpec = new ReferenceSpec();
        referenceSpec.Query.OrderBy(e => e.Name);
        TargetSpec targetSpec = new TargetSpec();

        // Act
        targetSpec.LoadSpecification(referenceSpec);

        // Assert
        Assert.Single(targetSpec.OrderExpressions);
        Assert.Equal(
            referenceSpec.OrderExpressions.ToList()[0].KeySelector.ToString(),
            targetSpec.OrderExpressions.ToList()[0].KeySelector.ToString());
    }

    [Fact]
    public void LoadSpecification_CopiesSearchCriteria()
    {
        // Arrange
        ReferenceSpec referenceSpec = new ReferenceSpec();
        referenceSpec.Query.Search(e => e.Name, "test");
        TargetSpec targetSpec = new TargetSpec();

        // Act
        targetSpec.LoadSpecification(referenceSpec);

        // Assert
        Assert.Single(targetSpec.SearchCriterias);
        Assert.Equal(
            referenceSpec.SearchCriterias.ToList()[0].Selector.ToString(),
            targetSpec.SearchCriterias.ToList()[0].Selector.ToString());
        Assert.Equal("test", targetSpec.SearchCriterias.ToList()[0].SearchTerm);
    }

    [Fact]
    public void LoadSpecification_CopiesTake()
    {
        // Arrange
        ReferenceSpec referenceSpec = new ReferenceSpec();
        referenceSpec.Query.Take(10);
        TargetSpec targetSpec = new TargetSpec();

        // Act
        targetSpec.LoadSpecification(referenceSpec);

        // Assert
        Assert.Equal(10, targetSpec.Take);
    }

    [Fact]
    public void LoadSpecification_CopiesSkip()
    {
        // Arrange
        ReferenceSpec referenceSpec = new ReferenceSpec();
        referenceSpec.Query.Skip(5);
        TargetSpec targetSpec = new TargetSpec();

        // Act
        targetSpec.LoadSpecification(referenceSpec);

        // Assert
        Assert.Equal(5, targetSpec.Skip);
    }

    [Fact]
    public void LoadSpecification_CopiesAllComponents()
    {
        // Arrange
        ReferenceSpec referenceSpec = new ReferenceSpec();
        referenceSpec.Query.Where(e => e.Id > 5);
        referenceSpec.Query.OrderBy(e => e.Name);
        referenceSpec.Query.Search(e => e.Name, "test");
        referenceSpec.Query.Take(10);
        referenceSpec.Query.Skip(5);
        TargetSpec targetSpec = new TargetSpec();

        // Act
        targetSpec.LoadSpecification(referenceSpec);

        // Assert
        Assert.Single(targetSpec.WhereExpressions);
        Assert.Single(targetSpec.OrderExpressions);
        Assert.Single(targetSpec.SearchCriterias);
        Assert.Equal(10, targetSpec.Take);
        Assert.Equal(5, targetSpec.Skip);
    }

    [Fact]
    public void LoadSpecification_CopiesMultipleWhereExpressions()
    {
        // Arrange
        ReferenceSpec referenceSpec = new ReferenceSpec();
        referenceSpec.Query.Where(e => e.Id > 5);
        referenceSpec.Query.Where(e => e.Name != "SOMETHING");
        TargetSpec targetSpec = new TargetSpec();

        // Act
        targetSpec.LoadSpecification(referenceSpec);

        // Assert
        Assert.Equal(2, targetSpec.WhereExpressions.Count());
        Assert.Equal(
            referenceSpec.WhereExpressions.ToList()[0].Filter.ToString(),
            targetSpec.WhereExpressions.ToList()[0].Filter.ToString());
        Assert.Equal(
            referenceSpec.WhereExpressions.ToList()[1].Filter.ToString(),
            targetSpec.WhereExpressions.ToList()[1].Filter.ToString());
    }

    [Fact]
    public void LoadSpecification_CopiesMultipleOrderExpressions()
    {
        // Arrange
        ReferenceSpec referenceSpec = new ReferenceSpec();
        referenceSpec.Query.OrderBy(e => e.Name);
        referenceSpec.Query.OrderBy(e => e.Id);
        TargetSpec targetSpec = new TargetSpec();

        // Act
        targetSpec.LoadSpecification(referenceSpec);

        // Assert
        Assert.Equal(2, targetSpec.OrderExpressions.Count());
        Assert.Equal(
            referenceSpec.OrderExpressions.ToList()[0].KeySelector.ToString(),
            targetSpec.OrderExpressions.ToList()[0].KeySelector.ToString());
        Assert.Equal(
            referenceSpec.OrderExpressions.ToList()[1].KeySelector.ToString(),
            targetSpec.OrderExpressions.ToList()[1].KeySelector.ToString());
    }

    [Fact]
    public void LoadSpecification_EmptyReferenceSpec()
    {
        // Arrange
        ReferenceSpec referenceSpec = new ReferenceSpec();
        TargetSpec targetSpec = new TargetSpec();

        // Act
        targetSpec.LoadSpecification(referenceSpec);

        // Assert
        Assert.Empty(targetSpec.WhereExpressions);
        Assert.Empty(targetSpec.OrderExpressions);
        Assert.Empty(targetSpec.SearchCriterias);
        Assert.Null(targetSpec.Take);
        Assert.Null(targetSpec.Skip);
    }

    [Fact]
    public void LoadSpecification_NullReferenceSpec()
    {
        // Arrange
        ReferenceSpec? referenceSpec = null;
        TargetSpec targetSpec = new TargetSpec();

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => targetSpec.LoadSpecification(referenceSpec!));
    }

    [Fact]
    public void LoadSpecification_ReferenceSpecWithMultipleSearchCriteria()
    {
        // Arrange
        ReferenceSpec referenceSpec = new ReferenceSpec();
        referenceSpec.Query.Search(e => e.Name, "test1");
        referenceSpec.Query.Search(e => e.Id.ToString(), "test2");
        TargetSpec targetSpec = new TargetSpec();

        // Act
        targetSpec.LoadSpecification(referenceSpec);

        // Assert
        Assert.Equal(2, targetSpec.SearchCriterias.Count());
        Assert.Equal(
            referenceSpec.SearchCriterias.ToList()[0].Selector.ToString(),
            targetSpec.SearchCriterias.ToList()[0].Selector.ToString());
        Assert.Equal(
            referenceSpec.SearchCriterias.ToList()[1].Selector.ToString(),
            targetSpec.SearchCriterias.ToList()[1].Selector.ToString());
        Assert.Equal("test1", targetSpec.SearchCriterias.ToList()[0].SearchTerm);
        Assert.Equal("test2", targetSpec.SearchCriterias.ToList()[1].SearchTerm);
    }
}