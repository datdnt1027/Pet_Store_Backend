using pet_store_backend.domain.Entities;

namespace pet_store_backend.application.Services.Authentication
{
    public record AuthenticationResult(
       User user,
        string Token);
}
