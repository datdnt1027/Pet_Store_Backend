namespace pet_store_backend.contracts.Admin;

public record UserRoleResponse(
    Guid UserRoleId,
    string UserRoleName,
    List<UserPermissionResponse> UserPermissions,
    bool Status
);

public record UserPermissionResponse(
    Guid UserPermissionId,
    string TableName,
    bool Read,
    bool Create,
    bool Update,
    bool Deactive
);
public record AdminProfileResponse(
    string FirstName,
    string LastName,
    string Email,
    string Address,
    string Avatar,
    string PhoneNumber
);