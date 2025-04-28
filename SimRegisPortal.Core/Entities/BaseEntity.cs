namespace SimRegisPortal.Core.Entities;

public abstract class BaseEntity;

public abstract class BaseEntity<TKey> : BaseEntity
{
    public TKey Id { get; protected set; } = default!;
}
