using pet_store_backend.domain.Common.Models;
using pet_store_backend.domain.Entities.User.ValueObjects;

namespace pet_store_backend.domain.Entities.User;

public class UserRole : AggregateRoot<UserRoleId>
{
    public List<UserPermission> _userPermissions = new();
    public string UserRoleName { get; private set; }
    public User User { get; private set; }
    public IReadOnlyList<UserPermission> UserPermissions => _userPermissions.AsReadOnly();
    public bool Status { get; private set; }

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