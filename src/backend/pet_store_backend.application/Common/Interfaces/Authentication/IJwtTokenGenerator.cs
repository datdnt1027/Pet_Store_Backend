using pet_store_backend.domain.Entities.Users;

namespace pet_store_backend.application.Common.Interfaces.Authentication
{
    public interface IJwtTokenGenerator
    {
        string GenerateTokenCustomer(UserRole userRole);
        string GenerateTokenUser(UserRole userRole, List<UserPermission> permissions);
    }
}
