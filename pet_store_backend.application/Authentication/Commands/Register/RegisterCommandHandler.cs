using ErrorOr;
using MediatR;
using pet_store_backend.application.Common.Interfaces.Authentication;
using pet_store_backend.application.Common.Interfaces.Persistence;
using pet_store_backend.application.Services.Authentication;
using pet_store_backend.domain.Common.Errors;
using pet_store_backend.domain.Entities.User;

namespace pet_store_backend.application.Authentication.Commands.Register;

public class RegisterCommadHandler : IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;

    public RegisterCommadHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }
    public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        // Validate User doesn't exist
        if (_userRepository.GetUserByEmail(command.Email) is not null)
        {
            return Errors.User.DuplicateEmail;
        }

        // Create user (generate unique Id) & Persist to DB
        var user = User.Create
        (
            command.FirstName,
            command.LastName,
            command.Email,
            command.Password
        );
        _userRepository.Add(user);

        // Create JWT Token
        var token = _jwtTokenGenerator.GenerateToken(user);
        return new AuthenticationResult(
            user,
            token);
    }
}