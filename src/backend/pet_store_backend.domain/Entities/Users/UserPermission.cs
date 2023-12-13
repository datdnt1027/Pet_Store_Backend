using pet_store_backend.domain.Common.Models;
using pet_store_backend.domain.Entities.Users.ValueObjects;

namespace pet_store_backend.domain.Entities.Users;

public class UserPermission : Entity<UserPermissionId>
{
    public UserRoleId UserRoleId { get; private set; }
    public UserRole? UserRole { get; private set; }
    public string TableName { get; private set; }
    public bool Read { get; private set; }
    public bool Create { get; private set; }
    public bool Update { get; private set; }
    public bool Deactive { get; private set; }

    private UserPermission(
        UserPermissionId userPermissionId, string tableName, bool read, bool create, bool update,
        bool deactive, UserRoleId userRoleId) : base(userPermissionId)
    {
        TableName = tableName;
        Read = read;
        Create = create;
        Update = update;
        Deactive = deactive;
        UserRoleId = userRoleId;
    }

    private UserPermission(
        UserPermissionId userPermissionId, string tableName, bool read, bool create, bool update,
        bool deactive, UserRoleId userRoleId, UserRole userRole) : base(userPermissionId)
    {
        TableName = tableName;
        Read = read;
        Create = create;
        Update = update;
        Deactive = deactive;
        UserRoleId = userRoleId;
        UserRole = userRole;
    }

    public static UserPermission CreatePermission(
        string tableName,
        bool create,
        bool read,
        bool update,
        bool deactive,
        UserRoleId userRoleId)
    {
        return new(
            UserPermissionId.CreatUnique(),
            tableName,
            create,
            read,
            update,
            deactive,
            userRoleId);
    }

    public static UserPermission CreatePermission(
        string tableName,
        bool create,
        bool read,
        bool update,
        bool deactive,
        UserRoleId userRoleId,
        UserRole userRole)
    {
        return new(
            UserPermissionId.CreatUnique(),
            tableName,
            create,
            read,
            update,
            deactive,
            userRoleId,
            userRole);
    }

    public void UpdateUserRoleId(UserRole userRole)
    {
        UserRole = userRole;
    }

#pragma warning disable CS8618
    private UserPermission()
    {

    }
#pragma warning restore CS8618
}