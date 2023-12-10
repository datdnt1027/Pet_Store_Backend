using pet_store_backend.application.Admin.Common;
using pet_store_backend.domain.Entities.Users;

namespace pet_store_backend.application.Common.Interfaces.Persistence;

public interface IAdminRepository
{
    Task<List<UserRoleResult>> RetriveUserRole();
    Task<AdminProfileResult?> RetrieveAdminProfile(Guid userId);
    Task UpdateAdminProfile(User user);
    Task<User?> RetrieveUser(Guid userId);
    Task<bool> UpdateStatusUserRole(Guid userRoleId, bool status);
    Task CreateUserRole(UserRole userRole);
}