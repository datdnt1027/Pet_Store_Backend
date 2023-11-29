using pet_store_backend.domain.Entities.User;

namespace pet_store_backend.application.Authentication.Common
{
    public record AuthenticationResult(
       UserRole userRole,
        string Token);
}
