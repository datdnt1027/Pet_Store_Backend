using ErrorOr;
using MediatR;
using pet_store_backend.application.Common;
using pet_store_backend.application.Common.Interfaces.Authentication;
using pet_store_backend.application.Common.Interfaces.Email;
using pet_store_backend.application.Common.Interfaces.Persistence;
using pet_store_backend.application.Utils;
using pet_store_backend.domain.Common.Errors;
using pet_store_backend.domain.Entities;
using pet_store_backend.domain.Entities.User;

namespace pet_store_backend.application.Authentication.Commands.ForgotPassword;

public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, ErrorOr<MessageResult>>
{
    private readonly IUserRepository _userRepository;
    private readonly IEmailService _emailService;
    private readonly IPasswordConfiguration _passwordConfiguration;
    public ForgotPasswordCommandHandler(IUserRepository userRepository, IEmailService emailService, IPasswordConfiguration passwordConfiguration)
    {
        _userRepository = userRepository;
        _emailService = emailService;
        _passwordConfiguration = passwordConfiguration;
    }
    public async Task<ErrorOr<MessageResult>> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
    {
        // Validate User doesn't exist
        if (await _userRepository.GetUserByEmail(request.Email) is not UserRole user)
        {
            return Errors.User.UserNotExist;
        }
        user.User.CreatePasswordResetToken(_passwordConfiguration.CreateRandomToken(), DateTime.Now.AddMinutes(HttpContextItemKeys.ExpireTokenInMinutes));
        await _userRepository.Update(user.User);
        var message = new Message(new string[] {
            user.User.Email },
            "Pet Store Reset Password Email",
            $"Your Reset Password Link {HttpContextItemKeys.UrlFrontEndForgotPasswordToken}/{user.User.PasswordResetToken}");
        _emailService.SendEmail(message);

        return new MessageResult("Your Reset Token has been sent to your email");

    }
}