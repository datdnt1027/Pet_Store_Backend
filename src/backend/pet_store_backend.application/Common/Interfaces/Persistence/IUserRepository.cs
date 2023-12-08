using pet_store_backend.domain.Entities.Users.ValueObjects;
using pet_store_backend.domain.Entities.Users;

namespace pet_store_backend.application.Common.Interfaces.Persistence
{
    public interface IUserRepository
    {
        Task<UserRole?> GetUserByEmail(string email);
        Task<UserRole?> GetCustomerByEmail(string email);
        Task Add(Customer customer);
        Task Update(Customer customer);
        Task<Customer?> GetCustomerByVerificationToken(string verificationToken);
        Task<Customer?> GetCustomerByResetPasswordToken(string resetPasswordToken);
        Task<List<UserPermission>> GetUserPermissionsAsync(UserRoleId userRoleId);
        Task<UserRoleId?> GetGuestRoleId();
        Task<UserRoleId?> GetUserRoleId(string userRoleName);
    }
}
