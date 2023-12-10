namespace pet_store_backend.contracts.Authentication;

public record LoginRequest(
    string Email,
    string Password);
