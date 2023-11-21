namespace Fundamental.Domain.Common.BaseTypes;

public abstract class BaseEntity<TId>
{
    public TId Id { get; protected set; }
    public DateTime CreatedAt { get; protected set; }
    public DateTime? UpdatedAt { get; protected set; }
}