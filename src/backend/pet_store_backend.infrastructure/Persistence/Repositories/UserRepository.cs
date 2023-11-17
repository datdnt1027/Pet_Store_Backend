using Microsoft.EntityFrameworkCore;
using pet_store_backend.application.Common.Interfaces.Persistence;
using pet_store_backend.domain.Entities.User;

namespace pet_store_backend.infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _dbContext;

        public UserRepository(DataContext dbcontext)
        {
            _dbContext = dbcontext;
        }

        public async Task Add(User user)
        {
            await _dbContext.AddAsync(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Update(User user)
        {
            _dbContext.Update(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            var user = await _dbContext.Users.SingleOrDefaultAsync(u => u.Email == email);
            return user;
        }

        public async Task<User?> GetUserByVerificationToken(string verificationToken)
        {
            var user = await _dbContext.Users.SingleOrDefaultAsync(u => u.VerificationToken == verificationToken);
            return user;
        }

        public async Task<User?> GetUserByResetPasswordToken(string resetPasswordToken)
        {
            var user = await _dbContext.Users.SingleOrDefaultAsync(u => u.PasswordResetToken == resetPasswordToken);
            return user;
        }
    }
}
