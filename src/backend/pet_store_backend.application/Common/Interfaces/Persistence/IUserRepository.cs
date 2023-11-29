using pet_store_backend.domain.Entities.User;
using pet_store_backend.domain.Entities.User.ValueObjects;

namespace pet_store_backend.application.Common.Interfaces.Persistence
{
    public interface IUserRepository
    {
        Task<UserRole?> GetUserByEmail(string email);
        Task Add(User user);
        Task Update(User user);
        Task<User?> GetUserByVerificationToken(string verificationToken);
        Task<User?> GetUserByResetPasswordToken(string resetPasswordToken);
        Task<List<UserPermission>> GetUserPermissionsAsync(UserRoleId userRoleId);
        Task<UserRoleId?> GetGuestRoleId();
        Task<UserRoleId?> GetUserRoleId(string userRoleName);
    }
}
