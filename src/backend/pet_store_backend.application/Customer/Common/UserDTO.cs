using pet_store_backend.domain.Entities.Users.ValueObjects;

namespace pet_store_backend.application.Customer.Common;

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
    Guid CustomerId,
    string FirstName,
    string LastName,
    Gender? Sex,
    string Email,
    string Address,
    byte[] Avatar,
    string PhoneNumber,
    bool Status
);