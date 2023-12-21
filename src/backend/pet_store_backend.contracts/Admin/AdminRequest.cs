namespace pet_store_backend.contracts.Admin;


public record UpdateAdminProfileRequest(
    string? FirstName,
    string? LastName,
    string? Sex,
    // string? Email,
    string? Address,
    byte[]? Avatar,
    string? PhoneNumber
);

public record UpdateRoleStatusRequest(
    string UserRoleId,
    bool Status
);

public record UpdateCustomerStatusRequest(
    string CustomerId,
    string Status
);

public record FindUserRequest(
    string? Email,
    string? PhoneNumber
);

public record UpdateOrderManageRequest(
    string OrderId,
    string OrderStatus,
    string? ExpectedDeliveryStartDate,
    string? ExpectedDeliveryEndDate
);