using pet_store_backend.domain.Entities.Users.ValueObjects;
using pet_store_backend.domain.Entities.Users;
using pet_store_backend.application.Customer.Common;

namespace pet_store_backend.application.Common.Interfaces.Persistence
{
    public interface IUserRepository
    {
        Task<pet_store_backend.domain.Entities.Users.User?> GetUserByEmail(string email);
        Task<pet_store_backend.domain.Entities.Users.Customer?> GetCustomerByEmail(string email);
        Task<UserProfileWithStatusResult?> GetCustomerByEmailForAdmin(string email);
        Task<UserProfileWithStatusResult?> GetCustomerByPhoneNumberForAdmin(string phoneNumber);
        Task Add(pet_store_backend.domain.Entities.Users.Customer customer);
        Task Update(pet_store_backend.domain.Entities.Users.Customer customer);
        Task<pet_store_backend.domain.Entities.Users.Customer?> GetCustomerByVerificationToken(string verificationToken);
        Task<pet_store_backend.domain.Entities.Users.Customer?> GetCustomerByResetPasswordToken(string resetPasswordToken);
        Task<List<UserPermission>> GetUserPermissionsAsync(UserRoleId userRoleId);
        Task<UserRoleId?> GetGuestRoleId();
        Task<UserRoleId?> GetUserRoleId(string userRoleName);
        Task<UserProfileResult?> RetrieveUserProfile(Guid userId);
        Task<pet_store_backend.domain.Entities.Users.Customer?> RetrieveUser(Guid userId);
        Task UpdateCustomerStatusAsync(pet_store_backend.domain.Entities.Users.Customer customer);

    }
}
