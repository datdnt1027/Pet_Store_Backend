namespace pet_store_backend.application.Admin.Common;

public record UserRoleResult(
    Guid UserRoleId,
    string UserRoleName,
    List<UserPermissionResult> UserPermissions,
    bool Status
);

public record UserPermissionResult(
    Guid UserPermissionId,
    string TableName,
    bool Read,
    bool Create,
    bool Update,
    bool Deactive
);