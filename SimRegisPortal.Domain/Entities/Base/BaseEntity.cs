namespace SimRegisPortal.Domain.Entities.Base
{
    public abstract class BaseEntity { }

    public abstract class BaseEntity<TKey> : BaseEntity
    {
        public TKey Id { get; protected set; } = default!;
    }
}
