namespace pet_store_backend.contracts.Authentication
{
    public record AuthenticationResponse(
        Guid UserId,
        string FirstName,
        string LastName,
        string Email,
        string Token);
}
