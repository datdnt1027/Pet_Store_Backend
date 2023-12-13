using pet_store_backend.application.Admin.Common;
using pet_store_backend.domain.Entities.Users;

namespace pet_store_backend.application.Common.Interfaces.Persistence;

public interface IAdminRepository
{
    Task<List<UserRoleResult>> RetriveUserRole();
    Task<AdminProfileResult?> RetrieveAdminProfile(Guid userId);
    Task UpdateAdminProfile(pet_store_backend.domain.Entities.Users.User user);
    Task<pet_store_backend.domain.Entities.Users.User?> RetrieveUser(Guid userId);
    Task UpdateUserRole(UserRole userRole);
    Task CreateUserRole(UserRole userRole);
    Task<UserRole?> GetUserRoleFromId(Guid userRoleId);
}