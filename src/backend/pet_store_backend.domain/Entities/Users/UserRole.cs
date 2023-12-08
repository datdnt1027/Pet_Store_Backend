using pet_store_backend.domain.Common.Models;
using pet_store_backend.domain.Entities.Users.ValueObjects;

namespace pet_store_backend.domain.Entities.Users;

public class UserRole : AggregateRoot<UserRoleId>
{
    public List<UserPermission> _userPermissions = new();
    public string UserRoleName { get; private set; }
    public User User { get; private set; } = null!;
    public Customer Customer { get; private set; } = null!;
    public IReadOnlyList<UserPermission> UserPermissions => _userPermissions.AsReadOnly();
    public bool Status { get; private set; }

    // For User
    private UserRole(
        UserRoleId userRoleId,
        string userRoleName,
        User user,
        List<UserPermission> userPermissions,
        bool status) : base(userRoleId)
    {
        UserRoleName = userRoleName;
        _userPermissions = userPermissions;
        User = user;
        Status = status;
    }

    // For Customer
    private UserRole(
        UserRoleId userRoleId,
        string userRoleName,
        Customer customer,
        List<UserPermission> userPermissions,
        bool status) : base(userRoleId)
    {
        UserRoleName = userRoleName;
        _userPermissions = userPermissions;
        Customer = customer;
        Status = status;
    }

    public static UserRole Create(
        string userRoleName,
        User user,
        List<UserPermission> _userPermission
    )
    {
        return new(
            UserRoleId.CreatUnique(),
            userRoleName,
            user,
            _userPermission,
            true);
    }

#pragma warning disable CS8618
    private UserRole()
    {

    }
#pragma warning restore CS8618
}