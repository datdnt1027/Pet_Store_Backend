using ErrorOr;
using MediatR;
using pet_store_backend.application.Common;
using pet_store_backend.application.Common.Interfaces.Authentication;
using pet_store_backend.application.Common.Interfaces.Persistence;
using pet_store_backend.domain.Common.Errors;
using pet_store_backend.domain.Entities.Users;

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
        if (await _userRepository.GetCustomerByResetPasswordToken(request.Token) is not pet_store_backend.domain.Entities.Users.Customer customer)
        {
            return Errors.Authentication.InvalidToken;
        }
        if (customer.TokenExpires < DateTime.Now)
        {
            return Errors.Authentication.TokenExpire;
        }
        _passwordConfiguration.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
        customer.UpdatePassword(passwordHash, passwordSalt);
        customer.UpdateVerifiedAt(DateTime.Now);
        await _userRepository.Update(customer);

        return new MessageResult("Password successfully changed");

    }
}