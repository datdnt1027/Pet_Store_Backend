namespace pet_store_backend.contracts.Admin;


public record UpdateAdminProfileRequest(
    string? FirstName,
    string? LastName,
    string? Email,
    string? Address,
    byte[]? Avatar,
    string? PhoneNumber,
    bool Status
);

public record UpdateRoleStatusRequest(
    string UserRoleId,
    bool Status
);