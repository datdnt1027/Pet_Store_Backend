using pet_store_backend.domain.Entities.User;

namespace pet_store_backend.application.Common.Interfaces.Persistence
{
    public interface IUserRepository
    {
        Task<User?> GetUserByEmail(string email);
        Task Add(User user);
        Task Update(User user);
        Task<User?> GetUserByVerificationToken(string verificationToken);
        Task<User?> GetUserByResetPasswordToken(string resetPasswordToken);
    }
}
