using pet_store_backend.domain.Common.Models;

namespace pet_store_backend.domain.Entities.PetProducts.ValueObjects;


public sealed class CategoryId : ValueObject
{
    public Guid Value { get; }

    private CategoryId(Guid value)
    {
        Value = value;
    }

    public static CategoryId CreatUnique()
    {
        return new(Guid.NewGuid());
    }

    public static CategoryId Create(Guid value)
    {
        return new CategoryId(value);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}