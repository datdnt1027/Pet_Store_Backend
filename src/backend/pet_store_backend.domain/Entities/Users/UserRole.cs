using pet_store_backend.domain.Common.Models;
using pet_store_backend.domain.Entities.Users.ValueObjects;

namespace pet_store_backend.domain.Entities.Users;

public class UserRole : AggregateRoot<UserRoleId>
{
    public List<UserPermission> _userPermissions = new();
    public string UserRoleName { get; private set; } = null!;
    public List<User> Users { get; private set; } = null!;
    public List<Customer> Customers { get; private set; } = null!;
    public IReadOnlyList<UserPermission> UserPermissions => _userPermissions.AsReadOnly();
    public bool Status { get; private set; }

    // For User
    private UserRole(
        UserRoleId userRoleId,
        string userRoleName,
        List<User> users,
        List<UserPermission> userPermissions,
        bool status) : base(userRoleId)
    {
        UserRoleName = userRoleName;
        _userPermissions = userPermissions;
        Users = users;
        Status = status;
    }

    // For Customer
    private UserRole(
        UserRoleId userRoleId,
        string userRoleName,
        List<Customer> customers,
        List<UserPermission> userPermissions,
        bool status) : base(userRoleId)
    {
        UserRoleName = userRoleName;
        _userPermissions = userPermissions;
        Customers = customers;
        Status = status;
    }

    public static UserRole Create(
        string userRoleName,
        List<User> users,
        List<UserPermission> _userPermission
    )
    {
        return new(
            UserRoleId.CreatUnique(),
            userRoleName,
            users,
            _userPermission,
            true);
    }

    public void UpdateStatus(bool status)
    {
        Status = status;
    }

#pragma warning disable CS8618
    private UserRole()
    {

    }
#pragma warning restore CS8618
}