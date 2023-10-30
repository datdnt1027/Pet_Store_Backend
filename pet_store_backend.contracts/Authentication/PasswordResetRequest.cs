namespace pet_store_backend.contracts.Authentication;

public record PasswordResetRequest(
    string Token,
    string Password,
    string ConfirmPassword
);