using pet_store_backend.domain.Entities.Users;

namespace pet_store_backend.application.Common.Interfaces.Authentication
{
    public interface IJwtTokenGenerator
    {
        string GenerateTokenCustomer(Customer customer);
        string GenerateTokenUser(pet_store_backend.domain.Entities.Users.User user, List<UserPermission> permissions);
    }
}
