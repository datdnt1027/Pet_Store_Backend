namespace pet_store_backend.contracts.Authentication
{
    // For Customer
    public record AuthenticationResponse(
        Guid UserId,
        string FirstName,
        string LastName,
        string Email,
        string Token);

    // For User like Admin v.v
    public record AdminResponse(
        Guid UserId,
        string FirstName,
        string LastName,
        string Email,
        string Token);
}
