using pet_store_backend.application.Admin.Common;

namespace pet_store_backend.application.Common.Interfaces.Persistence;

public interface IAdminRepository
{
    Task<List<UserRoleResult>> RetriveUserRole();
}