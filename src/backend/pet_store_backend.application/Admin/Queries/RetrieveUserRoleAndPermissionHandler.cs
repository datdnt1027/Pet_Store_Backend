using ErrorOr;
using MediatR;
using pet_store_backend.application.Admin.Common;
using pet_store_backend.application.Common.Interfaces.Persistence;

namespace pet_store_backend.application.Admin.Queries;

public class RetriveUserRole : IRequest<ErrorOr<List<UserRoleResult>>>
{

}

public class RetrieveUserRoleAndPermission : IRequestHandler<RetriveUserRole, ErrorOr<List<UserRoleResult>>>
{
    private readonly IAdminRepository _adminRepository;

    public RetrieveUserRoleAndPermission(IAdminRepository adminRepository)
    {
        _adminRepository = adminRepository;
    }

    public async Task<ErrorOr<List<UserRoleResult>>> Handle(
        RetriveUserRole request,
        CancellationToken cancellationToken)
    {
        var userRole = await _adminRepository.RetriveUserRole();
        return userRole;
    }
}