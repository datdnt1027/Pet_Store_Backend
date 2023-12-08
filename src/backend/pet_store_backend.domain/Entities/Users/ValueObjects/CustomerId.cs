using pet_store_backend.domain.Common.Models;

namespace pet_store_backend.domain.Entities.Users.ValueObjects;

public sealed class CustomerId : ValueObject
{
    public Guid Value { get; }

    private CustomerId(Guid value)
    {
        Value = value;
    }

    public static CustomerId CreatUnique()
    {
        return new(Guid.NewGuid());
    }

    public static CustomerId Create(Guid value)
    {
        return new CustomerId(value);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}