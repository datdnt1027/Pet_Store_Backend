namespace pet_store_backend.contracts.Admin;


public record UpdateAdminProfileRequest(
    string FirstName,
    string LastName,
    int Sex,
    // string? Email,
    string Address,
    byte[]? Avatar,
    string PhoneNumber
);

public record UpdateRoleStatusRequest(
    string UserRoleId,
    bool Status
);