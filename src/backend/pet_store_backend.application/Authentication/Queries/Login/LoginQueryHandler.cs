using ErrorOr;
using MediatR;
using pet_store_backend.application.Authentication.Common;
using pet_store_backend.application.Common.Interfaces.Authentication;
using pet_store_backend.application.Common.Interfaces.Persistence;
using pet_store_backend.domain.Common.Errors;
using pet_store_backend.domain.Entities.User;

namespace pet_store_backend.application.Authentication.Queries.Login;

public class LoginQueryHandler : IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;
    private readonly IPasswordConfiguration _passwordConfiguration;

    public LoginQueryHandler(
        IJwtTokenGenerator jwtTokenGenerator,
        IUserRepository userRepository,
        IPasswordConfiguration passwordConfiguration)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
        _passwordConfiguration = passwordConfiguration;
    }
    public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery query, CancellationToken cancellationToken)
    {
        // Check if user already exists
        if (await _userRepository.GetUserByEmail(query.Email) is not UserRole user)
        {
            return Errors.Authentication.IvalidCredentials;
        }
        // Check User Password
        if (!_passwordConfiguration.VerifyPasswordHash(query.Password, user.User.PasswordHash, user.User.PasswordSalt))
        {
            return Errors.Authentication.IvalidCredentials;
        }
        // Check user have permission
        if (user.User.UserRoleId is null)
        {
            return Errors.Authentication.ForbidenPermission;
        }
        // Check user verified
        if (user.User.VerifiedAt == null)
        {
            return Errors.Authentication.NotVerified;
        }
        // Get permissions for user in JWT
        var permissions = await _userRepository.GetUserPermissionsAsync(user.Id);
        //Create JWT Token
        var token = _jwtTokenGenerator.GenerateToken(user, permissions);
        return new AuthenticationResult(
            user,
            token);
    }
}