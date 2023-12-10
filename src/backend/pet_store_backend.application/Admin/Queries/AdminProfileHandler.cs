using System.Security.Claims;
using ErrorOr;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using pet_store_backend.application.Admin.Common;
using pet_store_backend.application.Common.Interfaces.Persistence;
using pet_store_backend.domain.Common.Errors;

namespace pet_store_backend.application.Admin.Queries;

public record AdminProfileQuery
(

) : IRequest<ErrorOr<AdminProfileResult>>;

public class UpdateProductCommandValidator : AbstractValidator<AdminProfileQuery>
{
    public UpdateProductCommandValidator()
    {
    }
}

public class AdminProfileHandler : IRequestHandler<AdminProfileQuery, ErrorOr<AdminProfileResult>>
{
    private readonly IAdminRepository _adminRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AdminProfileHandler(IAdminRepository adminRepository, IHttpContextAccessor httpContextAccessor)
    {
        _adminRepository = adminRepository;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<ErrorOr<AdminProfileResult>> Handle(AdminProfileQuery request, CancellationToken cancellationToken)
    {
        var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null)
        {
            return Errors.User.UserNotSignIn;
        }
        var userProfile = await _adminRepository.RetrieveAdminProfile(Guid.Parse(userId));
        if (userProfile == null)
        {
            return Errors.User.UserNotExist;
        }
        return userProfile;
    }
}