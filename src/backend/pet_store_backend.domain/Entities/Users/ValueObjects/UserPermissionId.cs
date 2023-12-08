using pet_store_backend.domain.Common.Models;

namespace pet_store_backend.domain.Entities.Users.ValueObjects;

public class UserPermissionId : ValueObject
{
    public Guid Value { get; }

    private UserPermissionId(Guid value)
    {
        Value = value;
    }

    public static UserPermissionId CreatUnique()
    {
        return new(Guid.NewGuid());
    }

    public static UserPermissionId Create(Guid value)
    {
        return new UserPermissionId(value);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}