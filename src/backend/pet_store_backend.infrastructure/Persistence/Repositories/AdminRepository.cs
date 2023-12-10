using Microsoft.EntityFrameworkCore;
using pet_store_backend.application.Admin.Common;
using pet_store_backend.application.Common.Interfaces.Persistence;
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
}