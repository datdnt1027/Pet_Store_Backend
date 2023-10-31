using pet_store_backend.domain.Entities.User;

namespace pet_store_backend.application.Common.Interfaces.Authentication
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user);
    }
}
