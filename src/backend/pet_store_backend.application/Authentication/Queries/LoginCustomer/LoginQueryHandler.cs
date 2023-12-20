using ErrorOr;
using MediatR;
using pet_store_backend.application.Authentication.Common;
using pet_store_backend.application.Common.Interfaces.Authentication;
using pet_store_backend.application.Common.Interfaces.Persistence;
using pet_store_backend.domain.Common.Errors;

namespace pet_store_backend.application.Authentication.Queries.Login;

public class LoginQueryHandler : IRequestHandler<LoginQuery, ErrorOr<AuthenticationCustomerResult>>
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
    public async Task<ErrorOr<AuthenticationCustomerResult>> Handle(LoginQuery query, CancellationToken cancellationToken)
    {
        // Check if user already exists
        if (await _userRepository.GetCustomerByEmail(query.Email) is not pet_store_backend.domain.Entities.Users.Customer customer)
        {
            return Errors.Authentication.IvalidCredentials;
        }
        if (!customer.Status)
        {
            return Errors.Authentication.ForbidenLogin;
        }
        // Check User Password
        if (!_passwordConfiguration.VerifyPasswordHash(query.Password, customer.PasswordHash, customer.PasswordSalt))
        {
            return Errors.Authentication.IvalidCredentials;
        }
        // Check user have permission
        if (customer.CustomerRoleId is null)
        {
            return Errors.Authentication.ForbidenPermission;
        }
        // Check user verified
        if (customer.VerifiedAt == null)
        {
            return Errors.Authentication.NotVerified;
        }
        //Create JWT Token
        var token = _jwtTokenGenerator.GenerateTokenCustomer(customer);
        return new AuthenticationCustomerResult(
            customer,
            token);
    }
}