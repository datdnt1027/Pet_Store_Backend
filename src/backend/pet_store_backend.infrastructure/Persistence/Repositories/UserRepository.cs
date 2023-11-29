using Microsoft.EntityFrameworkCore;
using pet_store_backend.application.Common.Interfaces.Persistence;
using pet_store_backend.domain.Entities.User;
using pet_store_backend.domain.Entities.User.ValueObjects;
using pet_store_backend.infrastructure.Persistence.Common;

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

        public async Task<UserRole?> GetUserByEmail(string email)
        {
            var userRole = await _dbContext.UserRoles
                .Where(ur => ur.User.Email == email)
                .Include(ur => ur.User)  // Include to eagerly load the related User
                .FirstOrDefaultAsync();

            return userRole;
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

        public async Task<List<UserPermission>> GetUserPermissionsAsync(UserRoleId userRoleId)
        {
            var userPermissionIds = await _dbContext.UserPermissions
                .Where(up => up.UserRoleId == userRoleId)
                .ToListAsync();

            return userPermissionIds;
        }

        public async Task<UserRoleId?> GetGuestRoleId()
        {
            var userRole = await _dbContext.UserRoles
                .Where(ur => ur.UserRoleName.Equals(UserRoleKey.UserRoleName))
                .FirstOrDefaultAsync();
            return userRole?.Id;
        }

        public async Task<UserRoleId?> GetUserRoleId(string userRoleName)
        {
            var userRole = await _dbContext.UserRoles
                .Where(ur => ur.UserRoleName.Equals(userRoleName))
                .FirstOrDefaultAsync();
            return userRole?.Id;
        }
    }
}
