using ErrorOr;
using MediatR;
using pet_store_backend.application.Common;
using pet_store_backend.application.Common.Interfaces.Authentication;
using pet_store_backend.application.Common.Interfaces.Persistence;
using pet_store_backend.domain.Common.Errors;
using pet_store_backend.domain.Entities.User;

namespace pet_store_backend.application.Authentication.Commands.ResetPassword;

public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, ErrorOr<MessageResult>>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordConfiguration _passwordConfiguration;

    public ResetPasswordCommandHandler(IUserRepository userRepository, IPasswordConfiguration passwordConfiguration)
    {
        _userRepository = userRepository;
        _passwordConfiguration = passwordConfiguration;
    }
    public async Task<ErrorOr<MessageResult>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        if (await _userRepository.GetUserByResetPasswordToken(request.Token) is not User user)
        {
            return Errors.Authentication.InvalidToken;
        }
        if (user.TokenExpires < DateTime.Now)
        {
            return Errors.Authentication.TokenExpire;
        }
        _passwordConfiguration.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
        user.UpdatePassword(passwordHash, passwordSalt);
        user.UpdateVerifiedAt(DateTime.Now);
        await _userRepository.Update(user);

        return new MessageResult("Password successfully changed");

    }
}