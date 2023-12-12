using System.Security.Claims;
using ErrorOr;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using pet_store_backend.application.Admin.Common;
using pet_store_backend.application.Common.Interfaces.Persistence;
using pet_store_backend.application.User.Common;
using pet_store_backend.domain.Common.Errors;

namespace pet_store_backend.application.Admin.Queries;

public record UserProfileQuery
(

) : IRequest<ErrorOr<UserProfileResult>>;

public class UserProfileQueryValidator : AbstractValidator<UserProfileQuery>
{
    public UserProfileQueryValidator()
    {
    }
}

public class UserProfileQueryHandler : IRequestHandler<UserProfileQuery, ErrorOr<UserProfileResult>>
{
    private readonly IUserRepository _userRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserProfileQueryHandler(IUserRepository userRepository, IHttpContextAccessor httpContextAccessor)
    {
        _userRepository = userRepository;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<ErrorOr<UserProfileResult>> Handle(UserProfileQuery request, CancellationToken cancellationToken)
    {
        var userId = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null)
        {
            return Errors.User.UserNotSignIn;
        }
        var userProfile = await _userRepository.RetrieveUserProfile(Guid.Parse(userId));
        if (userProfile == null)
        {
            return Errors.User.UserNotExist;
        }
        return userProfile;
    }
}