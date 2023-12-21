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
    string Sex,
    string Email,
    string Address,
    string Avatar,
    string PhoneNumber
);

public record FindCustomerResponse(
    string CustomerId,
    string FirstName,
    string LastName,
    string Sex,
    string Email,
    string Address,
    string Avatar,
    string PhoneNumber,
    string Status
);

public record UserProfileWithStatusResponse(
    string UserId,
    string FirstName,
    string LastName,
    string Sex,
    string Email,
    string Address,
    string Avatar,
    string PhoneNumber,
    string Status
);