using pet_store_backend.domain.Common.Models;

namespace pet_store_backend.domain.Entities.Users.ValueObjects;

public class UserRoleId : ValueObject
{
    public Guid Value { get; }

    private UserRoleId(Guid value)
    {
        Value = value;
    }

    public static UserRoleId CreatUnique()
    {
        return new(Guid.NewGuid());
    }

    public static UserRoleId Create(Guid value)
    {
        return new UserRoleId(value);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}