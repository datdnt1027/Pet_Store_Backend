namespace pet_store_backend.domain.Common.Models;

public abstract class AggregateRoot<TId> : Entity<TId>
    where TId : notnull
{
    protected AggregateRoot(TId id) : base(id)
    {
    }
#pragma warning disable C8618
    protected AggregateRoot()
    {

    }
#pragma warning restore CS8618
}