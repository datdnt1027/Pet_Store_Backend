using ErrorOr;
using MediatR;
using pet_store_backend.application.Authentication.Common;
using pet_store_backend.application.Authentication.Queries.Login;
using pet_store_backend.application.Common.Interfaces.Authentication;
using pet_store_backend.application.Common.Interfaces.Persistence;
using pet_store_backend.domain.Common.Errors;
using pet_store_backend.domain.Entities.Users;

namespace pet_store_backend.application.Authentication.Queries.LoginUser;

public class LoginQueryUserHandler : IRequestHandler<LoginQueryUser, ErrorOr<AuthenticationUserResult>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;
    private readonly IPasswordConfiguration _passwordConfiguration;

    public LoginQueryUserHandler(
        IJwtTokenGenerator jwtTokenGenerator,
        IUserRepository userRepository,
        IPasswordConfiguration passwordConfiguration)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
        _passwordConfiguration = passwordConfiguration;
    }
    public async Task<ErrorOr<AuthenticationUserResult>> Handle(LoginQueryUser query, CancellationToken cancellationToken)
    {
        // Check if user already exists
        if (await _userRepository.GetUserByEmail(query.Email) is not pet_store_backend.domain.Entities.Users.User user)
        {
            return Errors.Authentication.IvalidCredentials;
        }
        // Check User Password
        if (!_passwordConfiguration.VerifyPasswordHash(query.Password, user.PasswordHash, user.PasswordSalt))
        {
            return Errors.Authentication.IvalidCredentials;
        }
        // Check user have permission
        if (user.UserRoleId is null)
        {
            return Errors.Authentication.ForbidenPermission;
        }
        // Check user verified
        if (user.VerifiedAt == null)
        {
            return Errors.Authentication.NotVerified;
        }
        // Get permissions for user in JWT
        var permissions = await _userRepository.GetUserPermissionsAsync(user.UserRoleId);
        //Create JWT Token
        var token = _jwtTokenGenerator.GenerateTokenUser(user, permissions);
        return new AuthenticationUserResult(
            user,
            token);
    }
}