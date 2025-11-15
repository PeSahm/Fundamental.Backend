using Fundamental.Domain.Common.BaseTypes;

namespace Fundamental.Domain.ExAreas.Entities;

public class Fair : BaseEntity<Guid>
{
    public Fair(string json)
    {
        Json = json;
        Id = Guid.NewGuid();
        CreatedAt = DateTime.Now;
    }

    public string Json { get; set; }
}