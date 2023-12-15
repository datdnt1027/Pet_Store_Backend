using pet_store_backend.domain.Entities.Users.ValueObjects;

namespace pet_store_backend.application.User.Common;

public record UserProfileResult(
    string FirstName,
    string LastName,
    Gender? Sex,
    string Email,
    string Address,
    byte[] Avatar,
    string PhoneNumber
);
public record UserProfileWithStatusResult(
    string FirstName,
    string LastName,
    Gender? Sex,
    string Email,
    string Address,
    byte[] Avatar,
    string PhoneNumber,
    bool Status
);