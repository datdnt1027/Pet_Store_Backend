using pet_store_backend.domain.Common.Models;

namespace pet_store_backend.domain.Entities.Orders.ValueObjects;

public sealed class OrderId : ValueObject
{
    public Guid Value { get; }

    private OrderId(Guid value)
    {
        Value = value;
    }
    public static OrderId CreateUnique()
    {
        return new(Guid.NewGuid());
    }

    public static OrderId Create(Guid value)
    {
        return new OrderId(value);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}