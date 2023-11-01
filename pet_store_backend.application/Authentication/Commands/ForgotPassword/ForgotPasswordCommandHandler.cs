using System.Security.Cryptography;
using ErrorOr;
using MediatR;
using pet_store_backend.application.Authentication.Common;
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
    public ForgotPasswordCommandHandler(IUserRepository userRepository, IEmailService emailService)
    {
        _userRepository = userRepository;
        _emailService = emailService;
    }
    private string CreateRandomToken()
    {
        return Convert.ToHexString(RandomNumberGenerator.GetBytes(64));
    }
    public async Task<ErrorOr<MessageResult>> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
    {
        // Validate User doesn't exist
        if (await _userRepository.GetUserByEmail(request.Email) is not User user)
        {
            return Errors.User.UserNotExist;
        }
        user.CreatePasswordResetToken(CreateRandomToken());
        await _userRepository.Update(user);
        var message = new Message(new string[] {
            user.Email },
            "Pet Store Reset Password Email",
            $"Your Reset Password Link {HttpContextItemKeys.UrlFrontEndForgotPasswordToken}/{user.PasswordResetToken}");
        _emailService.SendEmail(message);

        return new MessageResult("Your Reset Token has been sent to your email");

    }
}