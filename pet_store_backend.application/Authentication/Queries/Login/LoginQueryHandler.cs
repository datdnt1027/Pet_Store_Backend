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
        await Task.CompletedTask;
        // Check if user already exists
        if (await _userRepository.GetUserByEmail(query.Email) is not User user)
        {
            return Errors.Authentication.IvalidCredentials;
        }
        // Check User Password
        if (!_passwordConfiguration.VerifyPasswordHash(query.Password, user.PasswordHash, user.PasswordSalt))
        {
            return Errors.Authentication.IvalidCredentials;
        }

        if (user.VerifiedAt == null)
        {
            return Errors.Authentication.NotVerified;
        }

        //Create JWT Token
        var token = _jwtTokenGenerator.GenerateToken(user);
        return new AuthenticationResult(
            user,
            token);
    }
}