namespace pet_store_backend.contracts.User;

public record UserProfileResponse(
    string FirstName,
    string LastName,
    string Sex,
    string Email,
    string Address,
    string Avatar,
    string PhoneNumber
);