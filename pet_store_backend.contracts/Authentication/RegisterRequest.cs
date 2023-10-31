namespace pet_store_backend.contracts.Authentication
{
    public record RegisterRequest(
        Guid UserId,
        string FirstName,
        string LastName,
        string Email,
        string Password,
        string ConfirmPassword);
}
