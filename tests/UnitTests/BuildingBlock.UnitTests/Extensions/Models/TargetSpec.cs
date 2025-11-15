using Ardalis.Specification;

namespace BuildingBlockUnitTests.Extensions.Models;

public sealed class TargetSpec : Specification<TestEntity, TestEntity>
{
    public TargetSpec()
    {
        Query.Select(e => e);
    }
}