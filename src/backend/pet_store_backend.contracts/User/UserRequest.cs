namespace pet_store_backend.contracts.User;

public record UpdateUserProfileRequest(
    string? FirstName,
    string? LastName,
    string? Sex,
    // string? Email,
    string? Address,
    byte[]? Avatar,
    string? PhoneNumber
);