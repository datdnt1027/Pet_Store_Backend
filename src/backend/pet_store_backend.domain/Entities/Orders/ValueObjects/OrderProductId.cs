using pet_store_backend.domain.Common.Models;

namespace pet_store_backend.domain.Entities.Orders.ValueObjects;

public sealed class OrderProductId : ValueObject
{
    public Guid Value { get; }

    private OrderProductId(Guid value)
    {
        Value = value;
    }

    public static OrderProductId CreateUnique()
    {
        return new(Guid.NewGuid());
    }

    public static OrderProductId Create(Guid value)
    {
        return new OrderProductId(value);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}