using Microsoft.EntityFrameworkCore;
using pet_store_backend.application.Admin.Common;
using pet_store_backend.application.Common.Interfaces.Persistence;
using pet_store_backend.domain.Entities.Users;
using pet_store_backend.domain.Entities.Users.ValueObjects;
using pet_store_backend.infrastructure.Persistence.Common;

namespace pet_store_backend.infrastructure.Persistence.Repositories;

public class AdminRepository : IAdminRepository
{
    private readonly DataContext _dbContext;

    public AdminRepository(DataContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<UserRoleResult>> RetriveUserRole()
    {
        var userRoles = await _dbContext.UserRoles
            .Include(ur => ur.UserPermissions)
            .Where(ur => !ur.UserRoleName.Equals(UserRoleKey.AdminRoleName)
                         && ur.Status == true)
            .ToListAsync();

        return userRoles.Select(
             ur => new UserRoleResult(
                 ur.Id.Value,
                 ur.UserRoleName,
                 ur.UserPermissions.Select(
                     up => new UserPermissionResult(
                         up.Id.Value,
                         up.TableName,
                         up.Read,
                         up.Create,
                         up.Update,
                         up.Deactive
                     )
                 ).ToList(),
                 ur.Status
             )
        ).ToList();
    }

    public async Task<UserRole?> GetUserRoleFromId(Guid userRoleId)
    {
        var userRole = await _dbContext.UserRoles
            .Where(ur => ur.Id == UserRoleId.Create(userRoleId) && !ur.UserRoleName.Equals(UserRoleKey.AdminRoleName))
            .FirstOrDefaultAsync();
        return userRole;
    }

    public async Task<AdminProfileResult?> RetrieveAdminProfile(Guid userId)
    {
        var adminProfile = await _dbContext.Users
            .Where(u => u.Id == UserId.Create(userId)) // Check if UserId.Create is necessary
            .Select(u => new AdminProfileResult(
                u.FirstName,
                u.LastName,
                u.Email,
                u.Address ?? "",
                u.Avatar ?? Array.Empty<byte>(),
                u.PhoneNumber ?? ""
            ))
            .FirstOrDefaultAsync();

        return adminProfile;
    }

    public async Task<User?> RetrieveUser(Guid userId)
    {
        var user = await _dbContext.Users
            .Where(u => u.Id == UserId.Create(userId)) // Check if UserId.Create is necessary
            .FirstOrDefaultAsync();

        return user;
    }

    public async Task UpdateAdminProfile(User user)
    {
        _dbContext.Update(user);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateUserRole(UserRole userRole)
    {
        _dbContext.Update(userRole);
        await _dbContext.SaveChangesAsync();
    }

    public async Task CreateUserRole(UserRole userRole)
    {
        await _dbContext.AddAsync(userRole);
        await _dbContext.SaveChangesAsync();
    }

}